using MaterialSkin.Controls;
using MaterialSkin.Animations;
using MaterialSkin;
using DBConnector;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PCKLIB;
using System.IO;
using System.Threading;

namespace OddAlert
{
    public partial class MainFrm : MaterialForm
    {
        MaterialSkinManager skinman;

        public DateTime m_cur_date = DateTime.Now;
        bool refresh_in_delay = false;

        System.Windows.Media.MediaPlayer m_media_player = new System.Windows.Media.MediaPlayer();
        public DynOddHelper m_dyn = new DynOddHelper();
        public List<RaceEvent> m_event_list = new List<RaceEvent>();
        public Dictionary<RaceEvent, int> m_event_status = new Dictionary<RaceEvent, int>();

        System.Timers.Timer m_timer_alert = new System.Timers.Timer();
        System.Timers.Timer m_timer_track_result = new System.Timers.Timer();
        System.Timers.Timer m_timer_ui_refresh = new System.Timers.Timer();

        public Dictionary<string, string> m_agency_lookup;
        public string[] m_monitor_bookies = new string[] {"Sportsbet", "BetEasy", "TAB NSW"};

        public Dictionary<string, Notification> m_notes = new Dictionary<string, Notification>();

        public string[] m_note_cols = new string[] {"No","DATE","TIME","TIME TO JUMP","DEGREE","STATE","VENUE","RACE NUMBER","HORSE NUMBER","HORSE NAME","PREVIOUS PRICE","CURRENT PRICE","BOOKIE","SUGGESTED STAKE","MAX PROFIT","TOP PRICE 1","TOP PRICE 2","TOP PRICE 3","TOP PRICE 4", "PRICE TAKEN", "BOOKIE_TAKEN", "BET AMOUNT", "RESULT", "BF SP" };

        string[] m_status = new string[] { "Pending", "Win", "Lose" };

        public API_Con m_api;
        public MainFrm()
        {
            skinman = MaterialSkinManager.Instance;
            skinman.AddFormToManage(this);
            skinman.Theme = MaterialSkinManager.Themes.DARK;
            skinman.ColorScheme = new ColorScheme(Primary.BlueGrey800, Primary.BlueGrey900, Primary.Blue200, Accent.Teal200, TextShade.WHITE);

            InitializeComponent();

            m_media_player.Open(new Uri(Directory.GetCurrentDirectory() + "\\Resources\\alert.wav"));

            m_timer_alert.Elapsed += M_timer_Elapsed;
            m_timer_alert.AutoReset = false;

            m_timer_ui_refresh.Elapsed += M_timer_ui_refresh_Elapsed;
            m_timer_ui_refresh.AutoReset = true;
            m_timer_ui_refresh.Interval = 1000;
            m_timer_ui_refresh.Start();

            m_timer_track_result.Elapsed += M_timer_track_result_Elapsed;
            m_timer_track_result.AutoReset = false;
            m_timer_track_result.Start();

            m_api = MainApp.g_api;
        }

        private async void M_timer_track_result_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            var notes = await m_api.get_tracking_notifications();
            if (notes != null)
            {
                bool updated = false;
                foreach (var note in notes)
                {
                    int id = note.id;
                    string eve_id = note.event_id;
                    int horse_num = note.horse_number;
                    int first = await m_dyn.get_event_result(eve_id);
                    if (first == -1)
                        continue;
                    string result = horse_num == first ? "Win" : "Lose";
                    note.result = result;
                    note.status = 2;
                    await m_api.update_notification(id, note);
                    updated = true;
                }

                if (updated)
                    refresh_grid();
            }
            m_timer_track_result.Start();
        }

        private void M_timer_ui_refresh_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            if(refresh_in_delay)
            {
                refresh_in_delay = false;
                refresh_grid();
            }
        }

        private async void MainFrm_Load(object sender, EventArgs e)
        {
            grid_note.DataSource = bind_main;
            grid_note.DoubleBuffered(true);
            
            MainApp.log_info("Program started.");
            MainApp.g_working = true;
        }

        public void update_status(string log)
        {
            this.InvokeOnUiThreadIfRequired(() =>
            {
                lab_status.Text = log;
            });
        }

        void init_dynamic()
        {
            panel_wait.BringToFront();
            new System.Threading.Tasks.Task(async () =>
            {
                try
                {
                    bool _login = await m_dyn.login();
                    if (_login == false)
                    {
                        MainApp.log_info("Dynamic Odds API connection failed.");
                        if (MessageBox.Show("Failed to log in to Dynamic Odds XML feed service.\nDo you want to continue without live alert?", "OddAlert", MessageBoxButtons.YesNo) == DialogResult.No)
                        {
                            Close();
                            return;
                        }
                    }

                    play_sound();
                    MainApp.log_info("Dynamic Odds API connected.");
                    m_agency_lookup = await m_dyn.get_agencies();

                    load_events();

                    this.InvokeOnUiThreadIfRequired(() =>
                    {
                        panel_wait.SendToBack();
                    });

                    m_timer_alert.Start();
                }
                catch (Exception ex)
                {
                    MainApp.log_error("Exception in init_dynamic\n" + ex.Message + "\n" + ex.StackTrace);
                }
            }).Start();
        }

        async void load_events()
        {
            m_cur_date = DateTime.Now;
            m_event_list = await m_dyn.get_event_schedule(DateTime.Now.AddDays(MainApp.DEBUG ? 1 : 0));
            if (m_event_list.Count == 0)
                MainApp.log_info("No events are left today. No notification will be triggered this session.");
            else
                MainApp.log_info($"{m_event_list.Count} events loaded from Dynamic Odds API.");
            m_event_status.Clear();
            foreach (var eve in m_event_list)
                m_event_status.Add(eve, 0);
        }

        private async void M_timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            try
            {
                {

                }
                if (m_dyn.Ready())
                {
                    if (m_cur_date.ToString("yyyy-MM-dd") != DateTime.Now.ToString("yyyy-MM-dd"))
                        load_events();

                    foreach (var eve in m_event_list)
                    {
                        if (m_event_status[eve] == 0)
                        {
                            var au_now = MainApp.AuNow();
                            if (eve.StartTime < au_now)
                            {
                                m_event_status[eve] = 2; // ended
                                continue;
                            }

                            m_event_status[eve] = 1; // busy
                            new Thread(async () =>
                            {
                                var odd_view = await m_dyn.get_runner_odds(eve.ID);
                                // Check delta and raise the alert
                                if (eve.OddView != null && odd_view != null)
                                {
                                    MainApp.log_info($"Odds for {eve.State} - {eve.Venue} - {eve.EventNo} updated.");
                                    foreach (var runner in odd_view.RName)
                                    {
                                        foreach (var bookie in m_monitor_bookies)
                                        {
                                            decimal pre_price = eve.OddView.get_odd_for_bookie(bookie, runner);
                                            decimal cur_price = odd_view.get_odd_for_bookie(bookie, runner);

                                            if (pre_price < 2M || cur_price == 0)
                                                continue;

                                            int pre_point = PriceTable.get_point(pre_price);
                                            int cur_point = PriceTable.get_point(cur_price);
                                            int delta = cur_point - pre_point;
                                            if (delta <= -MainApp.g_setting.alert_level && -delta <= 13)
                                            {
                                                play_sound();
                                                MainApp.log_info("Notification triggered.");

                                                int idx = odd_view.RName.IndexOf(runner);
                                                // get top prices
                                                List<KeyValuePair<string, decimal>> odd_dict = new List<KeyValuePair<string, decimal>>();
                                                foreach (var one_bookie in odd_view.Odds)
                                                {
                                                    odd_dict.Add(new KeyValuePair<string, decimal>(one_bookie.Bookie, one_bookie.Odds[idx]));
                                                }
                                                odd_dict.Sort((pair1, pair2) => -pair1.Value.CompareTo(pair2.Value));

                                                // new notification
                                                Notification note = new Notification();
                                                note.event_id = eve.ID;
                                                note.datetime = au_now.ToString("yyyy-MM-dd hh:mm:ss");
                                                note.time_to_jump = eve.StartTime.Subtract(au_now).ToString("h'h 'm'm 's's'");
                                                note.degree = delta;
                                                note.state = eve.State;
                                                note.venue = eve.Venue;
                                                note.race_number = int.Parse(eve.EventNo);
                                                note.horse_number = int.Parse(odd_view.RNo[idx]);
                                                note.horse_name = runner;
                                                note.previous_price = pre_price;
                                                note.current_price = cur_price;
                                                note.bookie = bookie;
                                                note.suggested_stake = suggest_stake(pre_price, delta);
                                                note.max_profit = note.suggested_stake * (pre_price - 1);
                                                note.top_price_1 = $"${odd_dict[0].Value} {odd_dict[0].Key}";
                                                note.top_price_2 = $"${odd_dict[1].Value} {odd_dict[1].Key}";
                                                note.top_price_3 = $"${odd_dict[2].Value} {odd_dict[2].Key}";
                                                note.top_price_4 = $"${odd_dict[3].Value} {odd_dict[3].Key}";
                                                note.status = 0; // pending
                                                note.result = "-";
                                                add_note_to_db(note);
                                                refresh_in_delay = true;
                                                //refresh_grid();
                                            }
                                        }
                                    }
                                }

                                eve.OddView = odd_view;
                                m_event_status[eve] = 0; // idle
                            }).Start();
                        }
                    }

                    foreach(var pair in m_event_status)
                    {
                        if (pair.Value == 2)
                            m_event_list.Remove(pair.Key);
                    }

                    if(m_event_list.Count == 0)
                    {
                        MainApp.log_info("No active events are left today. " + m_cur_date.ToString("yyyy-MM-dd"));
                    }
                }

                if(MainApp.g_working)
                {
                    img_status.BackgroundImage = Properties.Resources.Circle_ON;
                }
                else
                {
                    img_status.BackgroundImage = Properties.Resources.Circle_OFF;
                }
            }
            catch (Exception ex)
            {
                MainApp.log_error("Exception in timer\n" + ex.Message + "\n" + ex.StackTrace);
            }
            m_timer_alert.Start();
        }
        private decimal suggest_stake(decimal prev_price, int point)
        {
            // Removed incidentally
            return 0
        }
        async void add_note_to_db(Notification note)
        {
            await m_api.create_notification(note);
        }

        async void refresh_grid()
        {
            int h_off = 0, v_off = 0;
            new System.Threading.Tasks.Task(async () =>
            {
                this.InvokeOnUiThreadIfRequired(() =>
                {
                    h_off = grid_note.FirstDisplayedScrollingColumnIndex;
                    v_off = grid_note.FirstDisplayedScrollingRowIndex;
                });
                var notes = await m_api.get_notifications();
                if (notes == null)
                    return;
                var view = notes.ToDataTable().DefaultView;
                view.Sort = "datetime desc";
                var dt = view.ToTable();
                this.InvokeOnUiThreadIfRequired(() =>
                {
                    bind_main.DataSource = dt;
                    if( h_off >= 0 && h_off < grid_note.Columns.Count)
                        grid_note.FirstDisplayedScrollingColumnIndex = h_off;
                    if (v_off >= 0 && v_off < grid_note.Rows.Count)
                        grid_note.FirstDisplayedScrollingRowIndex = v_off;
                });
            }).Start();
        }

        private void MainFrm_Resize(object sender, EventArgs e)
        {
            lab_caption.Left = this.Width / 2 - lab_caption.Width / 2;
        }

        public void play_sound()
        {
            this.InvokeOnUiThreadIfRequired(() =>
            {
                try
                {
                    //m_media_player.Play();
                    using (var soundPlayer = new System.Media.SoundPlayer(Directory.GetCurrentDirectory() + "\\Resources\\alert.wav"))
                    {
                        soundPlayer.Play(); // can also use soundPlayer.PlaySync()
                    }
                }
                catch (Exception ex)
                {
                    MainApp.log_error(ex.Message);
                }
            });
        }

        private void tabs_main_Click(object sender, EventArgs e)
        {

        }

        private void MainFrm_Shown(object sender, EventArgs e)
        {
            init_dynamic();
            refresh_grid();
        }
    }
}
