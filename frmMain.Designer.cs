namespace INV2019
{
    partial class frmMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.panel1 = new System.Windows.Forms.Panel();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.inout_ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.login_StripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.logout_StripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.urser_ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tranInfo_ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.managerInOut_StripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.AutoSize = true;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 28);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1237, 656);
            this.panel1.TabIndex = 0;
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.toolsToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1237, 28);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.inout_ToolStripMenuItem,
            this.toolStripSeparator,
            this.login_StripMenuItem1,
            this.logout_StripMenuItem1,
            this.exitToolStripMenuItem,
            this.toolStripSeparator1});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(44, 24);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // inout_ToolStripMenuItem
            // 
            this.inout_ToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("inout_ToolStripMenuItem.Image")));
            this.inout_ToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.inout_ToolStripMenuItem.Name = "inout_ToolStripMenuItem";
            this.inout_ToolStripMenuItem.Size = new System.Drawing.Size(160, 26);
            this.inout_ToolStripMenuItem.Text = "&Giờ Ra Vào";
            this.inout_ToolStripMenuItem.Click += new System.EventHandler(this.inout_ToolStripMenuItem_Click);
            // 
            // toolStripSeparator
            // 
            this.toolStripSeparator.Name = "toolStripSeparator";
            this.toolStripSeparator.Size = new System.Drawing.Size(157, 6);
            // 
            // login_StripMenuItem1
            // 
            this.login_StripMenuItem1.Name = "login_StripMenuItem1";
            this.login_StripMenuItem1.Size = new System.Drawing.Size(160, 26);
            this.login_StripMenuItem1.Text = "Đăng Nhập";
            this.login_StripMenuItem1.Click += new System.EventHandler(this.login_StripMenuItem1_Click);
            // 
            // logout_StripMenuItem1
            // 
            this.logout_StripMenuItem1.Name = "logout_StripMenuItem1";
            this.logout_StripMenuItem1.Size = new System.Drawing.Size(160, 26);
            this.logout_StripMenuItem1.Text = "Đăng Xuất";
            this.logout_StripMenuItem1.Click += new System.EventHandler(this.logout_StripMenuItem1_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(160, 26);
            this.exitToolStripMenuItem.Text = "E&xit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(157, 6);
            // 
            // toolsToolStripMenuItem
            // 
            this.toolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.urser_ToolStripMenuItem,
            this.tranInfo_ToolStripMenuItem,
            this.managerInOut_StripMenuItem});
            this.toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
            this.toolsToolStripMenuItem.Size = new System.Drawing.Size(56, 24);
            this.toolsToolStripMenuItem.Text = "&Tools";
            // 
            // urser_ToolStripMenuItem
            // 
            this.urser_ToolStripMenuItem.Name = "urser_ToolStripMenuItem";
            this.urser_ToolStripMenuItem.Size = new System.Drawing.Size(213, 26);
            this.urser_ToolStripMenuItem.Text = "Thông Tin &User";
            this.urser_ToolStripMenuItem.Click += new System.EventHandler(this.urser_ToolStripMenuItem_Click);
            // 
            // tranInfo_ToolStripMenuItem
            // 
            this.tranInfo_ToolStripMenuItem.Name = "tranInfo_ToolStripMenuItem";
            this.tranInfo_ToolStripMenuItem.Size = new System.Drawing.Size(213, 26);
            this.tranInfo_ToolStripMenuItem.Text = "&Thông Tin Xe";
            this.tranInfo_ToolStripMenuItem.Click += new System.EventHandler(this.tranInfo_ToolStripMenuItem_Click);
            // 
            // managerInOut_StripMenuItem
            // 
            this.managerInOut_StripMenuItem.Name = "managerInOut_StripMenuItem";
            this.managerInOut_StripMenuItem.Size = new System.Drawing.Size(213, 26);
            this.managerInOut_StripMenuItem.Text = "&Quản Lý Giờ Ra Vào";
            this.managerInOut_StripMenuItem.Click += new System.EventHandler(this.managerInOut_StripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(53, 24);
            this.helpToolStripMenuItem.Text = "&Help";
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(134, 26);
            this.aboutToolStripMenuItem.Text = "&About...";
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1237, 684);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "frmMain";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.Text = "Quản Lý Giở Ra Vào";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem inout_ToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tranInfo_ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem urser_ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem managerInOut_StripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem login_StripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem logout_StripMenuItem1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
    }
}