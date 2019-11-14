/*
 * DynamicOdds API Helper
 * David Piao 2019.
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace OddAlert
{
    public class DynOddHelper
    {
        string[] au_states = new string[] { "NSW", "QLD", "SA", "TAS", "VIC", "WA", "ACT", "NT" };
        public string session_id;
        private string ep = "http://dynamicraceodds.com/xml/data/";
        public DynOddHelper()
        {

        }

        public bool Ready()
        {
            return session_id != "";
        }
        public async System.Threading.Tasks.Task<bool> login()
        {
            try
            {
                string user = MainApp.g_setting.dyn_odd_credential.user;
                string pwd = MainApp.g_setting.dyn_odd_credential.pwd;
                string res = await WRequest.get_response($"{ep}Login.asp?UserName={user}&Password={pwd}");
                XDocument doc = XDocument.Parse(res);
                var _sid = doc.Element("Login").Element("SessionID");
                if (_sid == null)
                {
                    return false;
                }
                session_id = _sid.Value;
                MainApp.g_working = true;
                return true;
            }
            catch (Exception ex)
            {
                MainApp.g_working = false;
                MainApp.log_error("DynHelperError: " + ex.Message + "\n" + ex.StackTrace);
                return false;
            }
        }

        public async System.Threading.Tasks.Task<RaceEvent> get_event(RaceEvent ref_eve, string eve_id)
        {
            try
            {
                RaceEvent eve = new RaceEvent();
                string endpoint = $"{ep}GetData.asp?SessionID={session_id}&Method=GetEvent&EventID={eve_id}&Runners=true";
                string res = await WRequest.get_response(endpoint);
                XDocument doc = XDocument.Parse(res);
                if (check_session(doc) == false)
                {
                    if (res.Contains("Request Limit Exceeded"))
                    {
                        await Task.Delay(1000);
                        return await get_event(ref_eve, eve_id);
                    }
                    MainApp.g_working = false;
                    return null;
                }

                MainApp.g_working = true;
                XElement elem = null;
                try
                {
                    elem = doc.Element("Data").Element("GetEvent").Element("Event");
                }
                catch (Exception e)
                {
                    return null;
                }
                if (elem == null)
                    return null;
                eve.ID = elem.Attribute("ID").Value;
                eve.EventNo = elem.Element("EventNo").Value;
                eve.Name = elem.Element("Name").Value;
                eve.Distance = elem.Element("Distance").Value;
                eve.Track = elem.Element("Track").Value;
                eve.TrackRtg = elem.Element("TrackRtg").Value;
                eve.Starters = elem.Element("Starters").Value;
                eve.StartTime_Au = elem.Element("StartTime").Value;
                eve.Class = elem.Element("Class").Value;
                eve.Prizemoney = elem.Element("Prizemoney").Value;
                eve.Status = elem.Element("Status").Value;
                if (eve.Status.ToUpper() == "FINAL")
                    eve.Status = "PAYING";
                eve.WeatherTC = elem.Element("WeatherTC").Value;
                WeatherConditions con = new WeatherConditions();
                con.TempC = elem.Element("WeatherCondtions").Element("TempC").Value;
                con.Humidity = elem.Element("WeatherCondtions").Element("Humidity").Value;
                con.WindSpeed = elem.Element("WeatherCondtions").Element("WindSpeed").Value;
                con.WindDir = elem.Element("WeatherCondtions").Element("WindDir").Value;
                eve.WeatherCon = con;
                eve.Rail = elem.Element("Rail").Value;
                eve.CodeAAP = elem.Element("CodeAAP").Value;
                eve.CodeBF = elem.Element("CodeBF").Value;
                eve.Runners = new List<Runner>();
                foreach (var node in elem.Elements("Runner"))
                {
                    Runner runner = new Runner();
                    runner.Name = node.Element("Name").Value;
                    runner.Jockey = node.Element("Jockey").Value;
                    runner.Trainer = node.Element("Trainer").Value;
                    runner.Bar = node.Element("Bar").Value;
                    runner.Hcp = node.Element("Hcp").Value;
                    runner.Scr = node.Element("Scr").Value;
                    runner.Emergency = node.Element("Emergency").Value;
                    eve.Runners.Add(runner);
                }

                eve.Venue = ref_eve.Venue;
                eve.EventCode = ref_eve.EventCode;
                eve.Type = ref_eve.Type;
                eve.State = ref_eve.State;
                eve.StartTime = ref_eve.StartTime;
                eve.Date = ref_eve.Date;
                MainApp.g_working = true;
                return eve;
            }
            catch (Exception ex)
            {
                MainApp.log_error("DynHelperError: " + ex.Message + "\n" + ex.StackTrace);
                MainApp.g_working = false;
                return null;
            }
        }

        public async Task<OddOverview> get_runner_odds(string event_id)
        {
            XDocument doc;
            string res;
            try
            {
                OddOverview overview = new OddOverview();
                overview.EveID = event_id;

                string endpoint = $"{ep}GetData.asp?SessionID={session_id}&Method=GetRunnerOdds&EventID={event_id}";
                res = await WRequest.get_response(endpoint);
                doc = XDocument.Parse(res);
                if (check_session(doc) == false)
                {
                    if(res.Contains("Request Limit Exceeded"))
                    {
                        await Task.Delay(1000);
                        return await get_runner_odds(event_id);
                    }
                    MainApp.log_error("Error getting running odds");
                    MainApp.log_error(res);
                    MainApp.g_working = false;
                    return null;
                }

                IEnumerable<XElement> elems;
                try
                {
                    elems = doc.Element("Data").Element("RunnerOdds").Elements();
                }
                catch (Exception ee)
                {
                    MainApp.log_error("DynHelperError: " + ee.Message + "\n" + ee.StackTrace);
                    MainApp.g_working = false;
                    return null;
                }
                overview.Odds = new List<OddOneBookie>();
                foreach (var elem in elems)
                {
                    string name = elem.Name.ToString();
                    string val = elem.Value.ToString();
                    if (name == "RNo")
                    {
                        overview.RNo = new List<string>(val.Split(','));
                    }
                    else if (name == "RName")
                    {
                        overview.RName = new List<string>(val.Split(','));
                    }
                    else
                    {
                        string bookie;
                        if (OddBookieMap.map.TryGetValue(name, out bookie))
                        {
                            OddOneBookie odd_one = new OddOneBookie();
                            odd_one.OddName = name;
                            odd_one.Bookie = bookie;
                            odd_one.Odds = new List<decimal>(val.Split(',').Select(x => x.ToString() == "" ? 0 : decimal.Parse(x)));
                            overview.Odds.Add(odd_one);
                        }
                    }
                }
                MainApp.g_working = true;
                return overview;
            }
            catch (Exception ex)
            {
                MainApp.log_error("DynHelperError: " + ex.Message + "\n" + ex.StackTrace);
                MainApp.g_working = false;
                return null;
            }
        }

        public async Task<int> get_event_result(string event_id)
        {
            XDocument doc;
            string res;
            try
            {
                string endpoint = $"{ep}GetData.asp?SessionID={session_id}&Method=GetEventResults&EventID={event_id}";
                res = await WRequest.get_response(endpoint);
                doc = XDocument.Parse(res);
                if (check_session(doc) == false)
                {
                    if (res.Contains("Request Limit Exceeded"))
                    {
                        await Task.Delay(1000);
                        return await get_event_result(event_id);
                    }
                    MainApp.log_error("Error getting event results");
                    MainApp.log_error(res);
                    MainApp.g_working = false;
                    return -1;

                }

                IEnumerable<XElement> elems;
                try
                {
                    elems = doc.Element("Data").Element("EventResults").Elements("Placings").Elements("Result").Elements("No");
                }
                catch (Exception ex)
                {
                    MainApp.log_error("Error parsing event results");
                    MainApp.log_error("DynHelperError: " + ex.Message + "\n" + ex.StackTrace);
                    MainApp.g_working = false;
                    return -1;
                }
                var elem = elems.FirstOrDefault();
                if (elem == null)
                    return -1;
                MainApp.g_working = true;
                return int.Parse(elem.Value.ToString());
            }
            catch (Exception ex)
            {
                MainApp.log_error("DynHelperError: " + ex.Message + "\n" + ex.StackTrace);
                MainApp.g_working = false;
                return -1;
            }
        }
        public async System.Threading.Tasks.Task<List<RaceEvent>> get_event_schedule(DateTime date)
        {
            try
            {
                DateTime au_now = date.Subtract(MainApp.g_time_diff); 
                List<RaceEvent> schedule = new List<RaceEvent>();
                string date_str = date.ToString("yyyy-MM-dd");
                string endpoint = $"{ep}GetData.asp?SessionID={session_id}&Method=GetEventSchedule&Date={date_str}&Types=R,H,G&Limit=999";
                string res = await WRequest.get_response(endpoint);
                XDocument doc = XDocument.Parse(res);
                if (check_session(doc) == false)
                {
                    if (res.Contains("Request Limit Exceeded"))
                    {
                        await Task.Delay(1000);
                        return await get_event_schedule(date);
                    }
                    MainApp.log_error("Error getting event schedule");
                    MainApp.log_error(res);
                    MainApp.g_working = false;
                    return null;
                }

                var all_events = doc.Descendants("Event");
                foreach(var node in all_events)
                {
                    RaceEvent eve = new RaceEvent();
                    eve.ID = node.Attribute("ID").Value;
                    eve.Date = date;
                    eve.EventCode = node.Attribute("EventCode").Value;
                    eve.StartTime_Au = node.Attribute("StartTime").Value;
                    eve.Type = node.Attribute("Type").Value;
                    if (eve.Type == "T")
                        eve.Type = "H";
                    eve.Venue = node.Attribute("Venue").Value;
                    eve.EventNo = node.Attribute("EventNo").Value;
                    eve.Status = node.Attribute("EventStatus").Value;
                    if (eve.Status.ToUpper() == "FINAL")
                        eve.Status = "PAYING";
                    eve.State = node.Attribute("State").Value;
                    
                    if (au_states.Contains(eve.State) == false ||
                        eve.Status.ToUpper() != "OPEN" ||
                        eve.Type != "R" )
                        continue;

                    if (!MainApp.DEBUG && eve.StartTime_Au.CompareTo(au_now.ToString("HH:mm:ss")) < 0)
                        continue;

                    string[] fields = eve.StartTime_Au.Split(':');
                    eve.StartTime = new DateTime(au_now.Year, au_now.Month, au_now.Day, int.Parse(fields[0]), int.Parse(fields[1]), int.Parse(fields[2]));

                    //MainApp.log_info(eve.ToString());
                    schedule.Add(eve);
                }
                MainApp.g_working = true;
                return schedule;
            }
            catch (Exception ex)
            {
                MainApp.log_error("DynHelperError: " + ex.Message + "\n" + ex.StackTrace);
                MainApp.g_working = false;
                return null;
            }
        }

        public void calculate_localtime(List<RaceEvent> schedule)
        {
            try
            {
                int now = -1;
                for (int i = 0; i < schedule.Count; i++)
                {
                    if (schedule[i].Status.ToUpper() == "OPEN")
                    {
                        now = i;
                        break;
                    }
                }

                if (now == -1)
                    return;
                if (now == 0)
                    now = schedule.Count - 1;

                DateTime aust = DateTime.Now.Subtract(MainApp.g_time_diff);
                schedule[now].StartTime = DateTime.Parse(aust.ToString("yyyy-MM-dd") + " " + schedule[now].StartTime_Au);
                schedule[now].StartTime = schedule[now].StartTime.Add(MainApp.g_time_diff);

                for (int i = 0; i < now; i++)
                {
                    if (string.Compare(schedule[i].StartTime_Au, schedule[now].StartTime_Au) > 0)
                    {
                        schedule[i].StartTime = DateTime.Parse(DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd") + " " + schedule[i].StartTime_Au);
                    }
                    else
                    {
                        schedule[i].StartTime = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd") + " " + schedule[i].StartTime_Au);
                    }
                    schedule[i].StartTime = schedule[i].StartTime.Add(MainApp.g_time_diff);
                }

                for (int i = now + 1; i < schedule.Count; i++)
                {
                    if (string.Compare(schedule[i].StartTime_Au, schedule[now].StartTime_Au) < 0)
                    {
                        schedule[i].StartTime = DateTime.Parse(DateTime.Now.AddDays(1).ToString("yyyy-MM-dd") + " " + schedule[i].StartTime_Au);
                    }
                    else
                    {
                        schedule[i].StartTime = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd") + " " + schedule[i].StartTime_Au);
                    }
                    schedule[i].StartTime = schedule[i].StartTime.Add(MainApp.g_time_diff);
                }

            }
            catch (Exception ex)
            {
                MainApp.log_error("DynHelperError: " + ex.Message + "\n" + ex.StackTrace);
            }
        }
        public async System.Threading.Tasks.Task<Dictionary<string,string>> get_agencies()
        {
            try
            {
                Dictionary<string, string> agency = new Dictionary<string, string>();
                string endpoint = $"{ep}GetData.asp?SessionID={session_id}&Method=GetBettingAgencies";
                string res = await WRequest.get_response(endpoint);
                XDocument doc = XDocument.Parse(res);
                if (check_session(doc) == false)
                {
                    if (res.Contains("Request Limit Exceeded"))
                    {
                        await Task.Delay(1000);
                        return await get_agencies();
                    }
                    MainApp.log_error("Error getting agencies");
                    MainApp.log_error(res);
                    MainApp.g_working = false;
                    return null;
                }

                foreach (var node in doc.Descendants("BA"))
                {
                    agency.Add(node.Attribute("ID").Value, node.Element("Name").Value);
                }
                MainApp.g_working = true;
                return agency;
            }
            catch (Exception ex)
            {
                MainApp.log_error("DynHelperError: " + ex.Message + "\n" + ex.StackTrace);
                MainApp.g_working = false;
                return null;
            }
        }

        bool check_session(XDocument doc)
        {
            try
            {
                var error_node = doc.Descendants("Error").Single();
                if (error_node == null)
                    return false;
                if(error_node.Element("ErrorTxt").Value.Trim() != "")
                {
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                MainApp.log_error("DynHelperError: " + ex.Message + "\n" + ex.StackTrace);
                MainApp.g_working = false;
                return false;
            }
        }
       
    }
}
