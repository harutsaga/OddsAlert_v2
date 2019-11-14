namespace OddAlert
{
    partial class MainFrm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainFrm));
            this.panel_main = new System.Windows.Forms.Panel();
            this.tabc_main = new MaterialSkin.Controls.MaterialTabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.grid_note = new ADGV.AdvancedDataGridView();
            this.lab_status = new System.Windows.Forms.Label();
            this.tabs_main = new MaterialSkin.Controls.MaterialTabSelector();
            this.panel_wait = new System.Windows.Forms.Panel();
            this.progress = new Bunifu.Framework.UI.BunifuCircleProgressbar();
            this.materialLabel2 = new MaterialSkin.Controls.MaterialLabel();
            this.drag = new Bunifu.Framework.UI.BunifuDragControl(this.components);
            this.lab_caption = new System.Windows.Forms.Label();
            this.img_status = new System.Windows.Forms.PictureBox();
            this.bind_main = new System.Windows.Forms.BindingSource(this.components);
            this.panel_main.SuspendLayout();
            this.tabc_main.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grid_note)).BeginInit();
            this.panel_wait.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.img_status)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bind_main)).BeginInit();
            this.SuspendLayout();
            // 
            // panel_main
            // 
            this.panel_main.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.panel_main.Controls.Add(this.tabc_main);
            this.panel_main.Controls.Add(this.lab_status);
            this.panel_main.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_main.Location = new System.Drawing.Point(2, 64);
            this.panel_main.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.panel_main.Name = "panel_main";
            this.panel_main.Size = new System.Drawing.Size(1156, 728);
            this.panel_main.TabIndex = 0;
            // 
            // tabc_main
            // 
            this.tabc_main.Controls.Add(this.tabPage1);
            this.tabc_main.Depth = 0;
            this.tabc_main.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabc_main.Font = new System.Drawing.Font("Roboto", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabc_main.Location = new System.Drawing.Point(0, 0);
            this.tabc_main.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.tabc_main.MouseState = MaterialSkin.MouseState.HOVER;
            this.tabc_main.Name = "tabc_main";
            this.tabc_main.SelectedIndex = 0;
            this.tabc_main.Size = new System.Drawing.Size(1156, 707);
            this.tabc_main.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.tabPage1.Controls.Add(this.grid_note);
            this.tabPage1.Location = new System.Drawing.Point(4, 26);
            this.tabPage1.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.tabPage1.Size = new System.Drawing.Size(1148, 677);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Notifications";
            // 
            // grid_note
            // 
            this.grid_note.AllowUserToAddRows = false;
            this.grid_note.AllowUserToDeleteRows = false;
            this.grid_note.AutoGenerateContextFilters = true;
            this.grid_note.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(61)))), ((int)(((byte)(61)))), ((int)(((byte)(61)))));
            this.grid_note.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grid_note.DateWithTime = false;
            this.grid_note.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grid_note.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.grid_note.Location = new System.Drawing.Point(2, 3);
            this.grid_note.Name = "grid_note";
            this.grid_note.ReadOnly = true;
            this.grid_note.RowHeadersVisible = false;
            this.grid_note.RowTemplate.Height = 40;
            this.grid_note.Size = new System.Drawing.Size(1144, 671);
            this.grid_note.TabIndex = 1;
            this.grid_note.TimeFilter = false;
            // 
            // lab_status
            // 
            this.lab_status.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lab_status.Font = new System.Drawing.Font("Roboto", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lab_status.ForeColor = System.Drawing.SystemColors.Info;
            this.lab_status.Location = new System.Drawing.Point(0, 707);
            this.lab_status.Name = "lab_status";
            this.lab_status.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.lab_status.Size = new System.Drawing.Size(1156, 21);
            this.lab_status.TabIndex = 2;
            this.lab_status.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tabs_main
            // 
            this.tabs_main.BaseTabControl = this.tabc_main;
            this.tabs_main.Depth = 0;
            this.tabs_main.Dock = System.Windows.Forms.DockStyle.Top;
            this.tabs_main.Location = new System.Drawing.Point(2, 36);
            this.tabs_main.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.tabs_main.MouseState = MaterialSkin.MouseState.HOVER;
            this.tabs_main.Name = "tabs_main";
            this.tabs_main.Size = new System.Drawing.Size(1156, 28);
            this.tabs_main.TabIndex = 0;
            this.tabs_main.Click += new System.EventHandler(this.tabs_main_Click);
            // 
            // panel_wait
            // 
            this.panel_wait.Controls.Add(this.progress);
            this.panel_wait.Controls.Add(this.materialLabel2);
            this.panel_wait.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_wait.Location = new System.Drawing.Point(2, 36);
            this.panel_wait.Name = "panel_wait";
            this.panel_wait.Size = new System.Drawing.Size(1156, 756);
            this.panel_wait.TabIndex = 2;
            // 
            // progress
            // 
            this.progress.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.progress.animated = true;
            this.progress.animationIterval = 5;
            this.progress.animationSpeed = 5;
            this.progress.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.progress.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("progress.BackgroundImage")));
            this.progress.Font = new System.Drawing.Font("Microsoft Sans Serif", 26.25F);
            this.progress.ForeColor = System.Drawing.Color.SeaGreen;
            this.progress.LabelVisible = false;
            this.progress.LineProgressThickness = 8;
            this.progress.LineThickness = 6;
            this.progress.Location = new System.Drawing.Point(553, 257);
            this.progress.Margin = new System.Windows.Forms.Padding(10, 9, 10, 9);
            this.progress.MaxValue = 100;
            this.progress.Name = "progress";
            this.progress.ProgressBackColor = System.Drawing.Color.Gainsboro;
            this.progress.ProgressColor = System.Drawing.Color.SeaGreen;
            this.progress.Size = new System.Drawing.Size(80, 80);
            this.progress.TabIndex = 0;
            this.progress.Value = 30;
            // 
            // materialLabel2
            // 
            this.materialLabel2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.materialLabel2.AutoSize = true;
            this.materialLabel2.Depth = 0;
            this.materialLabel2.Font = new System.Drawing.Font("Roboto", 11F);
            this.materialLabel2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.materialLabel2.Location = new System.Drawing.Point(544, 346);
            this.materialLabel2.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialLabel2.Name = "materialLabel2";
            this.materialLabel2.Size = new System.Drawing.Size(97, 18);
            this.materialLabel2.TabIndex = 1;
            this.materialLabel2.Text = "Please wait...";
            // 
            // drag
            // 
            this.drag.Fixed = true;
            this.drag.Horizontal = true;
            this.drag.TargetControl = this.tabs_main;
            this.drag.Vertical = true;
            // 
            // lab_caption
            // 
            this.lab_caption.AutoSize = true;
            this.lab_caption.BackColor = System.Drawing.Color.Transparent;
            this.lab_caption.Font = new System.Drawing.Font("Roboto", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lab_caption.ForeColor = System.Drawing.Color.Wheat;
            this.lab_caption.Location = new System.Drawing.Point(500, 3);
            this.lab_caption.Name = "lab_caption";
            this.lab_caption.Size = new System.Drawing.Size(185, 19);
            this.lab_caption.TabIndex = 3;
            this.lab_caption.Text = "Dynamic Odds Trigger (v2)";
            // 
            // img_status
            // 
            this.img_status.BackColor = System.Drawing.Color.Transparent;
            this.img_status.BackgroundImage = global::OddAlert.Properties.Resources.Circle_ON;
            this.img_status.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.img_status.Location = new System.Drawing.Point(0, 0);
            this.img_status.Name = "img_status";
            this.img_status.Size = new System.Drawing.Size(26, 22);
            this.img_status.TabIndex = 4;
            this.img_status.TabStop = false;
            // 
            // MainFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.ClientSize = new System.Drawing.Size(1160, 795);
            this.Controls.Add(this.img_status);
            this.Controls.Add(this.lab_caption);
            this.Controls.Add(this.panel_main);
            this.Controls.Add(this.tabs_main);
            this.Controls.Add(this.panel_wait);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.Name = "MainFrm";
            this.Padding = new System.Windows.Forms.Padding(2, 36, 2, 3);
            this.Text = "Odd Alerter";
            this.Load += new System.EventHandler(this.MainFrm_Load);
            this.Shown += new System.EventHandler(this.MainFrm_Shown);
            this.Resize += new System.EventHandler(this.MainFrm_Resize);
            this.panel_main.ResumeLayout(false);
            this.tabc_main.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grid_note)).EndInit();
            this.panel_wait.ResumeLayout(false);
            this.panel_wait.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.img_status)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bind_main)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel_main;
        private MaterialSkin.Controls.MaterialTabSelector tabs_main;
        private MaterialSkin.Controls.MaterialTabControl tabc_main;
        private System.Windows.Forms.TabPage tabPage1;
        private ADGV.AdvancedDataGridView grid_note;
        private System.Windows.Forms.Panel panel_wait;
        private Bunifu.Framework.UI.BunifuCircleProgressbar progress;
        private Bunifu.Framework.UI.BunifuDragControl drag;
        private MaterialSkin.Controls.MaterialLabel materialLabel2;
        private System.Windows.Forms.Label lab_caption;
        private System.Windows.Forms.Label lab_status;
        private System.Windows.Forms.PictureBox img_status;
        private System.Windows.Forms.BindingSource bind_main;
    }
}

