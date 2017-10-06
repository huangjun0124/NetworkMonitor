using System;
using System.Timers;
using System.Collections;
using System.Diagnostics;

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

        public void GetSpeedMonitored(out string upSpeed, out string downSpeed)
        {
            long up = 0, down = 0;
            foreach (NetworkAdapter adapter in this.monitoredAdapters)
            {
                up += adapter.UploadSpeed;
                down += adapter.DownloadSpeed;
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
        }
    }
}