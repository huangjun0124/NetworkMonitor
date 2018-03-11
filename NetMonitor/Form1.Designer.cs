namespace NetMonitor
{
    partial class NetMonitor
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NetMonitor));
            this.lblUp = new System.Windows.Forms.Label();
            this.lblDown = new System.Windows.Forms.Label();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.selectAdapterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.All = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.totalDownLoadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblUp
            // 
            this.lblUp.AutoSize = true;
            this.lblUp.BackColor = System.Drawing.Color.Transparent;
            this.lblUp.Font = new System.Drawing.Font("Microsoft YaHei", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblUp.Location = new System.Drawing.Point(46, 7);
            this.lblUp.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblUp.Name = "lblUp";
            this.lblUp.Size = new System.Drawing.Size(90, 20);
            this.lblUp.TabIndex = 0;
            this.lblUp.Text = "999.99KB/s";
            this.lblUp.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblUp.MouseDown += new System.Windows.Forms.MouseEventHandler(this.NetMonitor_MouseDown);
            this.lblUp.MouseUp += new System.Windows.Forms.MouseEventHandler(this.NetMonitor_MouseUp);
            // 
            // lblDown
            // 
            this.lblDown.AutoSize = true;
            this.lblDown.BackColor = System.Drawing.Color.Transparent;
            this.lblDown.Font = new System.Drawing.Font("Microsoft YaHei", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblDown.Location = new System.Drawing.Point(141, 7);
            this.lblDown.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblDown.Name = "lblDown";
            this.lblDown.Size = new System.Drawing.Size(90, 20);
            this.lblDown.TabIndex = 0;
            this.lblDown.Text = "999.99KB/s";
            this.lblDown.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblDown.MouseDown += new System.Windows.Forms.MouseEventHandler(this.NetMonitor_MouseDown);
            this.lblDown.MouseUp += new System.Windows.Forms.MouseEventHandler(this.NetMonitor_MouseUp);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.selectAdapterToolStripMenuItem,
            this.totalDownLoadToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(172, 92);
            // 
            // selectAdapterToolStripMenuItem
            // 
            this.selectAdapterToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.All});
            this.selectAdapterToolStripMenuItem.Name = "selectAdapterToolStripMenuItem";
            this.selectAdapterToolStripMenuItem.Size = new System.Drawing.Size(157, 22);
            this.selectAdapterToolStripMenuItem.Text = "SelectAdapter";
            // 
            // All
            // 
            this.All.Name = "All";
            this.All.Size = new System.Drawing.Size(98, 22);
            this.All.Text = "√All";
            this.All.Click += new System.EventHandler(this.adapterToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(157, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // totalDownLoadToolStripMenuItem
            // 
            this.totalDownLoadToolStripMenuItem.Name = "totalDownLoadToolStripMenuItem";
            this.totalDownLoadToolStripMenuItem.Size = new System.Drawing.Size(171, 22);
            this.totalDownLoadToolStripMenuItem.Text = "Total DownLoad";
            // 
            // NetMonitor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::NetMonitor.Properties.Resources.pics1;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(229, 33);
            this.Controls.Add(this.lblDown);
            this.Controls.Add(this.lblUp);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "NetMonitor";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.TopMost = true;
            this.Shown += new System.EventHandler(this.NetMonitor_Shown);
            this.Leave += new System.EventHandler(this.NetMonitor_Leave);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.NetMonitor_MouseDown);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.NetMonitor_MouseUp);
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblUp;
        private System.Windows.Forms.Label lblDown;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem selectAdapterToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem All;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.ToolStripMenuItem totalDownLoadToolStripMenuItem;
    }
}

