using MaterialSkin.Controls;
using MaterialSkin.Animations;
using MaterialSkin;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OddAlert
{
    public partial class TakeForm : MaterialForm
    {
        public Notification note;
        public bool add = true;
        MaterialSkinManager skinman;
        public TakeForm(Notification _note = null, bool _add = true)
        {
            skinman = MaterialSkinManager.Instance;
            skinman.AddFormToManage(this);
            skinman.Theme = MaterialSkinManager.Themes.DARK;
            skinman.ColorScheme = new ColorScheme(Primary.BlueGrey800, Primary.BlueGrey900, Primary.Blue200, Accent.Teal200, TextShade.WHITE);

            note = _note;
            add = _add;
            InitializeComponent();
        }

        private void btn_ok_Click(object sender, EventArgs e)
        {
            decimal price_taken, bet_amount;
            if(decimal.TryParse(txt_price.Text, out price_taken) == false ||
                decimal.TryParse(txt_bet_amount.Text, out bet_amount) == false )
            {
                MessageBox.Show("Please input valid numbers");
                return;
            }
            note.account = txt_bookie.Text;
            note.price_taken = price_taken;
            note.bet_amount = bet_amount;
            note.BF_SP = decimal.Parse(txt_bf_sp.Text);
            if (rad_pending.Checked)
                note.status = 0;
            else if (rad_win.Checked)
                note.status = 1;
            else if (rad_lose.Checked)
                note.status = 2;
            note.max_profit = (note.price_taken - 1) * note.bet_amount;
            DialogResult = DialogResult.OK;
            Close();
        }

        private void btn_cancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void TakeForm_Load(object sender, EventArgs e)
        {
            if (add == false)
            {
                txt_bookie.Text = note.account;
                txt_price.Text = String.Format("{0:#0.##}", note.price_taken);
                txt_bet_amount.Text = String.Format("{0:#0.##}", note.bet_amount);
                txt_bf_sp.Text = String.Format("{0:#0.##}", note.BF_SP);
                if (note.status == 0)
                    rad_pending.Checked = true;
                else if (note.status == 1)
                    rad_win.Checked = true;
                else if (note.status == 2)
                    rad_lose.Checked = true;
            }
        }
    }
}
