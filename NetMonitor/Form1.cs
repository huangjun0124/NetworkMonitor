using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NetMonitor
{
    public partial class NetMonitor : Form
    {
        [DllImport("User32.DLL")]
        public static extern int SendMessage(IntPtr hWnd, uint Msg, int wParam, int lParam);
        [DllImport("User32.DLL")]
        public static extern bool ReleaseCapture();
        public const uint WM_SYSCOMMAND = 0x0112;
        public const int SC_MOVE = 61456;
        public const int HTCAPTION = 2;

        private NetworkMonitor Monitor;
        public NetMonitor()
        {
            InitializeComponent();
            contextMenuStrip1.Visible = false;
            InitAdapterItems();
        }

        private void InitAdapterItems()
        {
            Monitor = new NetworkMonitor();
            int i = 0;
            foreach (var adapter in Monitor.Adapters)
            {
                ToolStripMenuItem adapterMenuItem = new ToolStripMenuItem(adapter.Name);
                adapterMenuItem.Name = adapter.Name;
                adapterMenuItem.Tag = i++;
                adapterMenuItem.Click += new System.EventHandler(this.adapterToolStripMenuItem_Click);
                selectAdapterToolStripMenuItem.DropDownItems.Add(adapterMenuItem);
            }
            Monitor.StartMonitoring();
        }

        private void adapterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Monitor.StopMonitoring();
            var item = sender as ToolStripMenuItem;
            if (item.Tag == null)
            {
                Monitor.StartMonitoring();
            }
            else
            {
                Monitor.StartMonitoring(new NetworkAdapter[]{ Monitor.Adapters[(int) item.Tag]});
            }
            foreach (ToolStripMenuItem toolItem in selectAdapterToolStripMenuItem.DropDownItems)
            {
                if (toolItem == item)
                {
                    toolItem.Text = "√" + toolItem.Name;
                }
                else
                {
                    toolItem.Text = toolItem.Name;
                }
            }
        }

        private void NetMonitor_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                contextMenuStrip1.Show(Cursor.Position);
                contextMenuStrip1.Visible = true;
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void NetMonitor_Leave(object sender, EventArgs e)
        {
            contextMenuStrip1.Visible = false;
        }

        private void NetMonitor_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(Handle, WM_SYSCOMMAND, SC_MOVE | HTCAPTION, 0);
        }

        private void NetMonitor_Shown(object sender, EventArgs e)
        {
            this.Size = new Size(230, 33);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            string up, down, downT;
            Monitor.GetSpeedMonitored(out up, out down, out downT);
            this.lblDown.Text = down;
            this.lblUp.Text = up;
            totalDownLoadToolStripMenuItem.Text = "↓ " + downT;
        }
    }
}
