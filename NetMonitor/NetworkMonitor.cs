using System;
using System.Timers;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows.Forms;
using Timer = System.Timers.Timer;

namespace NetMonitor
{
    /// <summary>  
    /// The NetworkMonitor class monitors network speed for each network adapter on the computer,  
    /// using classes for Performance counter in .NET library.  
    /// </summary>  
    public class NetworkMonitor
    {
        private Timer timer;                // The timer event executes every second to refresh the values in adapters.  
        private ArrayList adapters;         // The list of adapters on the computer.  
        private ArrayList monitoredAdapters;// The list of currently monitored adapters.  

        public NetworkMonitor()
        {
            this.adapters = new ArrayList();
            this.monitoredAdapters = new ArrayList();
            EnumerateNetworkAdapters();

            timer = new Timer(1000);
            timer.Elapsed += new ElapsedEventHandler(timer_Elapsed);
        }
        /// <summary>  
        /// Enumerates network adapters installed on the computer.  
        /// </summary>  
        private void EnumerateNetworkAdapters()
        {
            string err;
            if (!TryEnumerateNA(out err))
            {
                string cmd = "LODCTR /R";
                // output : 信息: 成功地从系统备份存储中重建性能计数器设置
                InvokeCmd(cmd);
                if (!TryEnumerateNA(out err))
                {
                    MessageBox.Show(err);
                }
            }
        }

        private string InvokeCmd(string cmd)
        {
            System.Diagnostics.Process p = new System.Diagnostics.Process();
            p.StartInfo.FileName = "cmd.exe";
            p.StartInfo.UseShellExecute = false; //是否使用操作系统shell启动
            p.StartInfo.RedirectStandardInput = true; //接受来自调用程序的输入信息
            p.StartInfo.RedirectStandardOutput = true; //由调用程序获取输出信息
            p.StartInfo.RedirectStandardError = true; //重定向标准错误输出
            p.StartInfo.CreateNoWindow = true; //不显示程序窗口
            p.StartInfo.Verb = "RunAs"; // 确保以管理员身份运行
            p.Start(); //启动程序

            //向cmd窗口发送输入信息
            p.StandardInput.WriteLine( cmd + "&exit"); //+ "&exit"

            p.StandardInput.AutoFlush = true;
            //p.StandardInput.WriteLine("exit");
            //向标准输入写入要执行的命令。这里使用&是批处理命令的符号，表示前面一个命令不管是否执行成功都执行后面(exit)命令，如果不执行exit命令，后面调用ReadToEnd()方法会假死
            //同类的符号还有&&和||前者表示必须前一个命令执行成功才会执行后面的命令，后者表示必须前一个命令执行失败才会执行后面的命令

            //获取cmd窗口的输出信息
            string outputLog;
            outputLog = p.StandardOutput.ReadToEnd();
            
            p.WaitForExit(); //等待程序执行完退出进程
            p.Close();

            return outputLog;
        }

        private bool TryEnumerateNA(out string errorMsg)
        {
            errorMsg = "";
            bool ret = true;
            try
            {
                PerformanceCounterCategory category = new PerformanceCounterCategory("Network Interface");

                foreach (string name in category.GetInstanceNames())
                {
                    // This one exists on every computer.  
                    if (name == "MS TCP Loopback interface")
                        continue;
                    // Create an instance of NetworkAdapter class, and create performance counters for it.  
                    NetworkAdapter adapter =
                        new NetworkAdapter(name)
                        {
                            dlCounter = new PerformanceCounter("Network Interface", "Bytes Received/sec", name),
                            ulCounter = new PerformanceCounter("Network Interface", "Bytes Sent/sec", name)
                        };
                    this.adapters.Add(adapter); // Add it to ArrayList adapter  
                }
            }
            catch (Exception e)
            {
                errorMsg = e.Message;
                ret = false;
            }

            return ret;
        }

        private void timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            foreach (NetworkAdapter adapter in this.monitoredAdapters)
                adapter.Refresh();
        }
        /// <summary>  
        /// Get instances of NetworkAdapter for installed adapters on this computer.  
        /// </summary>  
        public NetworkAdapter[] Adapters => (NetworkAdapter[])this.adapters.ToArray(typeof(NetworkAdapter));

        /// <summary>  
        /// Disable the timer, and clear the monitoredAdapters list.  
        /// </summary>  
        public void StopMonitoring()
        {
            this.monitoredAdapters.Clear();
            timer.Enabled = false;
        }
        /// <summary>  
        /// Remove the specified adapter from the monitoredAdapters list, and   
        /// disable the timer if the monitoredAdapters list is empty.  
        /// </summary>  
        public void StartMonitoring(NetworkAdapter[] adapters = null)
        {
            if (adapters == null) adapters = Adapters;
            if (adapters.Length > 0)
            {
                foreach (NetworkAdapter adapter in adapters)
                    if (!this.monitoredAdapters.Contains(adapter))
                    {
                        this.monitoredAdapters.Add(adapter);
                        adapter.Init();
                    }
                timer.Enabled = true;
            }
        }

        public void GetSpeedMonitored(out string upSpeed, out string downSpeed, out string downTotal)
        {
            long up = 0, down = 0, downT = 0;
            foreach (NetworkAdapter adapter in this.monitoredAdapters)
            {
                up += adapter.UploadSpeed;
                down += adapter.DownloadSpeed;
                downT += adapter.DownloadTotal;
            }
            if (up > 1024 * 1024)
            {
                upSpeed = $"{(double) up / 1024 / 1024:n}MB/s";
            }
            else
            {
                upSpeed = $"{(double) up / 1024:n}KB/s";
            }
            if (down > 1024 * 1024)
            {
                downSpeed = $"{(double)down / 1024 / 1024:n}MB/s";
            }
            else
            {
                downSpeed = $"{(double)down / 1024:n}KB/s";
            }
            
            if (downT > 1024 * 1024 * 1024)
            {
                downTotal = $"{(double)downT / 1024 / 1024 / 1024:n}GB";
            }
            else if (downT > 1024 * 1024)
            {
                downTotal = $"{(double)downT / 1024 / 1024:n}MB";
            }
            else
            {
                downTotal = $"{(double)downT / 1024:n}KB";
            }
        }
    }
}