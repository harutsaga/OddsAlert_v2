namespace OddAlert
{
    partial class TakeForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.materialLabel5 = new MaterialSkin.Controls.MaterialLabel();
            this.materialLabel4 = new MaterialSkin.Controls.MaterialLabel();
            this.materialLabel3 = new MaterialSkin.Controls.MaterialLabel();
            this.txt_price = new MaterialSkin.Controls.MaterialSingleLineTextField();
            this.materialLabel2 = new MaterialSkin.Controls.MaterialLabel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.btn_cancel = new MaterialSkin.Controls.MaterialRaisedButton();
            this.btn_ok = new MaterialSkin.Controls.MaterialRaisedButton();
            this.materialLabel1 = new MaterialSkin.Controls.MaterialLabel();
            this.txt_bookie = new MaterialSkin.Controls.MaterialSingleLineTextField();
            this.txt_bet_amount = new MaterialSkin.Controls.MaterialSingleLineTextField();
            this.txt_bf_sp = new MaterialSkin.Controls.MaterialSingleLineTextField();
            this.panel1 = new System.Windows.Forms.Panel();
            this.rad_lose = new MaterialSkin.Controls.MaterialRadioButton();
            this.rad_win = new MaterialSkin.Controls.MaterialRadioButton();
            this.rad_pending = new MaterialSkin.Controls.MaterialRadioButton();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 110F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.materialLabel5, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.materialLabel4, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.materialLabel3, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.txt_price, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.materialLabel2, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 1, 5);
            this.tableLayoutPanel1.Controls.Add(this.materialLabel1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.txt_bookie, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.txt_bet_amount, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.txt_bf_sp, 1, 4);
            this.tableLayoutPanel1.Controls.Add(this.panel1, 1, 3);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 24);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 6;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(377, 243);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // materialLabel5
            // 
            this.materialLabel5.AutoSize = true;
            this.materialLabel5.Depth = 0;
            this.materialLabel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.materialLabel5.Font = new System.Drawing.Font("Roboto", 11F);
            this.materialLabel5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.materialLabel5.Location = new System.Drawing.Point(3, 160);
            this.materialLabel5.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialLabel5.Name = "materialLabel5";
            this.materialLabel5.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.materialLabel5.Size = new System.Drawing.Size(104, 40);
            this.materialLabel5.TabIndex = 7;
            this.materialLabel5.Text = "BF SP";
            this.materialLabel5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // materialLabel4
            // 
            this.materialLabel4.AutoSize = true;
            this.materialLabel4.Depth = 0;
            this.materialLabel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.materialLabel4.Font = new System.Drawing.Font("Roboto", 11F);
            this.materialLabel4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.materialLabel4.Location = new System.Drawing.Point(3, 120);
            this.materialLabel4.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialLabel4.Name = "materialLabel4";
            this.materialLabel4.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.materialLabel4.Size = new System.Drawing.Size(104, 40);
            this.materialLabel4.TabIndex = 6;
            this.materialLabel4.Text = "Result";
            this.materialLabel4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // materialLabel3
            // 
            this.materialLabel3.AutoSize = true;
            this.materialLabel3.Depth = 0;
            this.materialLabel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.materialLabel3.Font = new System.Drawing.Font("Roboto", 11F);
            this.materialLabel3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.materialLabel3.Location = new System.Drawing.Point(3, 80);
            this.materialLabel3.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialLabel3.Name = "materialLabel3";
            this.materialLabel3.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.materialLabel3.Size = new System.Drawing.Size(104, 40);
            this.materialLabel3.TabIndex = 5;
            this.materialLabel3.Text = "Bet Amount";
            this.materialLabel3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txt_price
            // 
            this.txt_price.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txt_price.Depth = 0;
            this.txt_price.Hint = "";
            this.txt_price.Location = new System.Drawing.Point(113, 48);
            this.txt_price.MaxLength = 32767;
            this.txt_price.MouseState = MaterialSkin.MouseState.HOVER;
            this.txt_price.Name = "txt_price";
            this.txt_price.PasswordChar = '\0';
            this.txt_price.SelectedText = "";
            this.txt_price.SelectionLength = 0;
            this.txt_price.SelectionStart = 0;
            this.txt_price.Size = new System.Drawing.Size(261, 23);
            this.txt_price.TabIndex = 4;
            this.txt_price.TabStop = false;
            this.txt_price.UseSystemPasswordChar = false;
            // 
            // materialLabel2
            // 
            this.materialLabel2.AutoSize = true;
            this.materialLabel2.Depth = 0;
            this.materialLabel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.materialLabel2.Font = new System.Drawing.Font("Roboto", 11F);
            this.materialLabel2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.materialLabel2.Location = new System.Drawing.Point(3, 40);
            this.materialLabel2.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialLabel2.Name = "materialLabel2";
            this.materialLabel2.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.materialLabel2.Size = new System.Drawing.Size(104, 40);
            this.materialLabel2.TabIndex = 2;
            this.materialLabel2.Text = "Price Taken";
            this.materialLabel2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 3;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel2.Controls.Add(this.btn_cancel, 2, 0);
            this.tableLayoutPanel2.Controls.Add(this.btn_ok, 1, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(113, 203);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(261, 37);
            this.tableLayoutPanel2.TabIndex = 0;
            // 
            // btn_cancel
            // 
            this.btn_cancel.AutoSize = true;
            this.btn_cancel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btn_cancel.Depth = 0;
            this.btn_cancel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn_cancel.Icon = null;
            this.btn_cancel.Location = new System.Drawing.Point(164, 3);
            this.btn_cancel.MouseState = MaterialSkin.MouseState.HOVER;
            this.btn_cancel.Name = "btn_cancel";
            this.btn_cancel.Primary = true;
            this.btn_cancel.Size = new System.Drawing.Size(94, 31);
            this.btn_cancel.TabIndex = 1;
            this.btn_cancel.Text = "cancel";
            this.btn_cancel.UseVisualStyleBackColor = true;
            this.btn_cancel.Click += new System.EventHandler(this.btn_cancel_Click);
            // 
            // btn_ok
            // 
            this.btn_ok.AutoSize = true;
            this.btn_ok.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btn_ok.Depth = 0;
            this.btn_ok.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn_ok.Icon = null;
            this.btn_ok.Location = new System.Drawing.Point(64, 3);
            this.btn_ok.MouseState = MaterialSkin.MouseState.HOVER;
            this.btn_ok.Name = "btn_ok";
            this.btn_ok.Primary = true;
            this.btn_ok.Size = new System.Drawing.Size(94, 31);
            this.btn_ok.TabIndex = 0;
            this.btn_ok.Text = "OK";
            this.btn_ok.UseVisualStyleBackColor = true;
            this.btn_ok.Click += new System.EventHandler(this.btn_ok_Click);
            // 
            // materialLabel1
            // 
            this.materialLabel1.AutoSize = true;
            this.materialLabel1.Depth = 0;
            this.materialLabel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.materialLabel1.Font = new System.Drawing.Font("Roboto", 11F);
            this.materialLabel1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.materialLabel1.Location = new System.Drawing.Point(3, 0);
            this.materialLabel1.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialLabel1.Name = "materialLabel1";
            this.materialLabel1.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.materialLabel1.Size = new System.Drawing.Size(104, 40);
            this.materialLabel1.TabIndex = 1;
            this.materialLabel1.Text = "Bookie";
            this.materialLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txt_bookie
            // 
            this.txt_bookie.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txt_bookie.Depth = 0;
            this.txt_bookie.Hint = "";
            this.txt_bookie.Location = new System.Drawing.Point(113, 8);
            this.txt_bookie.MaxLength = 32767;
            this.txt_bookie.MouseState = MaterialSkin.MouseState.HOVER;
            this.txt_bookie.Name = "txt_bookie";
            this.txt_bookie.PasswordChar = '\0';
            this.txt_bookie.SelectedText = "";
            this.txt_bookie.SelectionLength = 0;
            this.txt_bookie.SelectionStart = 0;
            this.txt_bookie.Size = new System.Drawing.Size(261, 23);
            this.txt_bookie.TabIndex = 3;
            this.txt_bookie.TabStop = false;
            this.txt_bookie.UseSystemPasswordChar = false;
            // 
            // txt_bet_amount
            // 
            this.txt_bet_amount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txt_bet_amount.Depth = 0;
            this.txt_bet_amount.Hint = "";
            this.txt_bet_amount.Location = new System.Drawing.Point(113, 88);
            this.txt_bet_amount.MaxLength = 32767;
            this.txt_bet_amount.MouseState = MaterialSkin.MouseState.HOVER;
            this.txt_bet_amount.Name = "txt_bet_amount";
            this.txt_bet_amount.PasswordChar = '\0';
            this.txt_bet_amount.SelectedText = "";
            this.txt_bet_amount.SelectionLength = 0;
            this.txt_bet_amount.SelectionStart = 0;
            this.txt_bet_amount.Size = new System.Drawing.Size(261, 23);
            this.txt_bet_amount.TabIndex = 8;
            this.txt_bet_amount.TabStop = false;
            this.txt_bet_amount.UseSystemPasswordChar = false;
            // 
            // txt_bf_sp
            // 
            this.txt_bf_sp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txt_bf_sp.Depth = 0;
            this.txt_bf_sp.Hint = "";
            this.txt_bf_sp.Location = new System.Drawing.Point(113, 168);
            this.txt_bf_sp.MaxLength = 32767;
            this.txt_bf_sp.MouseState = MaterialSkin.MouseState.HOVER;
            this.txt_bf_sp.Name = "txt_bf_sp";
            this.txt_bf_sp.PasswordChar = '\0';
            this.txt_bf_sp.SelectedText = "";
            this.txt_bf_sp.SelectionLength = 0;
            this.txt_bf_sp.SelectionStart = 0;
            this.txt_bf_sp.Size = new System.Drawing.Size(261, 23);
            this.txt_bf_sp.TabIndex = 8;
            this.txt_bf_sp.TabStop = false;
            this.txt_bf_sp.UseSystemPasswordChar = false;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.rad_lose);
            this.panel1.Controls.Add(this.rad_win);
            this.panel1.Controls.Add(this.rad_pending);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(113, 123);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(261, 34);
            this.panel1.TabIndex = 9;
            // 
            // rad_lose
            // 
            this.rad_lose.AutoSize = true;
            this.rad_lose.Depth = 0;
            this.rad_lose.Font = new System.Drawing.Font("Roboto", 10F);
            this.rad_lose.Location = new System.Drawing.Point(185, 5);
            this.rad_lose.Margin = new System.Windows.Forms.Padding(0);
            this.rad_lose.MouseLocation = new System.Drawing.Point(-1, -1);
            this.rad_lose.MouseState = MaterialSkin.MouseState.HOVER;
            this.rad_lose.Name = "rad_lose";
            this.rad_lose.Ripple = true;
            this.rad_lose.Size = new System.Drawing.Size(59, 30);
            this.rad_lose.TabIndex = 2;
            this.rad_lose.Text = "Lose";
            this.rad_lose.UseVisualStyleBackColor = true;
            // 
            // rad_win
            // 
            this.rad_win.AutoSize = true;
            this.rad_win.Depth = 0;
            this.rad_win.Font = new System.Drawing.Font("Roboto", 10F);
            this.rad_win.Location = new System.Drawing.Point(98, 5);
            this.rad_win.Margin = new System.Windows.Forms.Padding(0);
            this.rad_win.MouseLocation = new System.Drawing.Point(-1, -1);
            this.rad_win.MouseState = MaterialSkin.MouseState.HOVER;
            this.rad_win.Name = "rad_win";
            this.rad_win.Ripple = true;
            this.rad_win.Size = new System.Drawing.Size(52, 30);
            this.rad_win.TabIndex = 1;
            this.rad_win.Text = "Win";
            this.rad_win.UseVisualStyleBackColor = true;
            // 
            // rad_pending
            // 
            this.rad_pending.AutoSize = true;
            this.rad_pending.Checked = true;
            this.rad_pending.Depth = 0;
            this.rad_pending.Font = new System.Drawing.Font("Roboto", 10F);
            this.rad_pending.Location = new System.Drawing.Point(0, 5);
            this.rad_pending.Margin = new System.Windows.Forms.Padding(0);
            this.rad_pending.MouseLocation = new System.Drawing.Point(-1, -1);
            this.rad_pending.MouseState = MaterialSkin.MouseState.HOVER;
            this.rad_pending.Name = "rad_pending";
            this.rad_pending.Ripple = true;
            this.rad_pending.Size = new System.Drawing.Size(79, 30);
            this.rad_pending.TabIndex = 0;
            this.rad_pending.Text = "Pending";
            this.rad_pending.UseVisualStyleBackColor = true;
            // 
            // TakeForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(377, 267);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "TakeForm";
            this.Padding = new System.Windows.Forms.Padding(0, 24, 0, 0);
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Please input the following fields";
            this.Load += new System.EventHandler(this.TakeForm_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private MaterialSkin.Controls.MaterialRaisedButton btn_ok;
        private MaterialSkin.Controls.MaterialRaisedButton btn_cancel;
        private MaterialSkin.Controls.MaterialLabel materialLabel2;
        private MaterialSkin.Controls.MaterialLabel materialLabel1;
        private MaterialSkin.Controls.MaterialSingleLineTextField txt_price;
        private MaterialSkin.Controls.MaterialSingleLineTextField txt_bookie;
        private MaterialSkin.Controls.MaterialLabel materialLabel5;
        private MaterialSkin.Controls.MaterialLabel materialLabel4;
        private MaterialSkin.Controls.MaterialLabel materialLabel3;
        private MaterialSkin.Controls.MaterialSingleLineTextField txt_bet_amount;
        private MaterialSkin.Controls.MaterialSingleLineTextField txt_bf_sp;
        private System.Windows.Forms.Panel panel1;
        private MaterialSkin.Controls.MaterialRadioButton rad_lose;
        private MaterialSkin.Controls.MaterialRadioButton rad_win;
        private MaterialSkin.Controls.MaterialRadioButton rad_pending;
    }
}