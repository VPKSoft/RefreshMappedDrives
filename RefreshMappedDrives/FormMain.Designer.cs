namespace RefreshMappedDrives
{
    partial class FormMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            this.lvDrives = new System.Windows.Forms.ListView();
            this.colStateOk = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colDriveLetter = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colUNCPath = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this._colOk = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.imStateIcons = new System.Windows.Forms.ImageList(this.components);
            this.tmRefresh = new System.Windows.Forms.Timer(this.components);
            this.niTray = new System.Windows.Forms.NotifyIcon(this.components);
            this.cmsTray = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cmsTray.SuspendLayout();
            this.SuspendLayout();
            // 
            // lvDrives
            // 
            this.lvDrives.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lvDrives.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colStateOk,
            this.colDriveLetter,
            this.colUNCPath,
            this._colOk});
            this.lvDrives.HideSelection = false;
            this.lvDrives.Location = new System.Drawing.Point(12, 12);
            this.lvDrives.Name = "lvDrives";
            this.lvDrives.Size = new System.Drawing.Size(433, 117);
            this.lvDrives.SmallImageList = this.imStateIcons;
            this.lvDrives.StateImageList = this.imStateIcons;
            this.lvDrives.TabIndex = 1;
            this.lvDrives.UseCompatibleStateImageBehavior = false;
            this.lvDrives.View = System.Windows.Forms.View.Details;
            // 
            // colStateOk
            // 
            this.colStateOk.Text = "Re-Map OK";
            this.colStateOk.Width = 70;
            // 
            // colDriveLetter
            // 
            this.colDriveLetter.Text = "Drive Letter";
            this.colDriveLetter.Width = 89;
            // 
            // colUNCPath
            // 
            this.colUNCPath.Text = "Network Path";
            this.colUNCPath.Width = 247;
            // 
            // _colOk
            // 
            this._colOk.Width = 0;
            // 
            // imStateIcons
            // 
            this.imStateIcons.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imStateIcons.ImageStream")));
            this.imStateIcons.TransparentColor = System.Drawing.Color.Transparent;
            this.imStateIcons.Images.SetKeyName(0, "Error.png");
            this.imStateIcons.Images.SetKeyName(1, "OK.png");
            // 
            // tmRefresh
            // 
            this.tmRefresh.Interval = 500;
            this.tmRefresh.Tick += new System.EventHandler(this.TmRefresh_Tick);
            // 
            // niTray
            // 
            this.niTray.ContextMenuStrip = this.cmsTray;
            this.niTray.Icon = ((System.Drawing.Icon)(resources.GetObject("niTray.Icon")));
            this.niTray.Text = "Refresh Mapped Drives [Copyright";
            this.niTray.Visible = true;
            // 
            // cmsTray
            // 
            this.cmsTray.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exitToolStripMenuItem});
            this.cmsTray.Name = "cmsTray";
            this.cmsTray.Size = new System.Drawing.Size(94, 26);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Image = global::RefreshMappedDrives.Properties.Resources.Exit;
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.ExitToolStripMenuItem_Click);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(457, 141);
            this.Controls.Add(this.lvDrives);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormMain";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Refresh Mapped Drives [Copyright © VPKSoft 2019]";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormMain_FormClosing);
            this.Shown += new System.EventHandler(this.FormMain_Shown);
            this.cmsTray.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.ListView lvDrives;
        private System.Windows.Forms.ColumnHeader colStateOk;
        private System.Windows.Forms.ColumnHeader colDriveLetter;
        private System.Windows.Forms.ColumnHeader colUNCPath;
        private System.Windows.Forms.ImageList imStateIcons;
        private System.Windows.Forms.Timer tmRefresh;
        private System.Windows.Forms.ColumnHeader _colOk;
        private System.Windows.Forms.NotifyIcon niTray;
        private System.Windows.Forms.ContextMenuStrip cmsTray;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
    }
}

