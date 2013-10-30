using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace ServiceHost
{
    public partial class MainService : ServiceBase
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private Timer secondTimer;
        private Timer minuteTimer;
        private int maxFailure;

        public MainService() {
            InitializeComponent();
            InitTimers();
        }

        public void DebugStart(string[] args) {
            log.Debug("Start service in debug model");
            OnStart(args);
        }

        public void DebugStop() {
            OnStop();
        }

        protected override void OnStart(string[] args) {
            StartTimers();
            log.Info("Service started");
        }

        protected override void OnStop() {
            StopTimers();
            log.WarnFormat("Service stopped{0}", Environment.NewLine);
        }

        private void secondTimer_Elapsed(object sender, ElapsedEventArgs e) {
            secondTimer.Enabled = false;
            try {
                ServiceLogic.SyncDataAtSecond();
            }
            catch (Exception ex) {
                log.Error("Failed with exception.", ex);
                if (--maxFailure <= 0) {
                    log.Warn("The maximum number of failures is reached.");
                    Stop();
                    return;
                }
            }
            secondTimer.Enabled = true;
        }

        private void minuteTimer_Elapsed(object sender, ElapsedEventArgs e) {
            StopTimers(); //For stability, minute-timer events will stop second-timer as well.
            try {
                ServiceLogic.SyncDataAtMinute();
            }
            catch (Exception ex) {
                log.Error("Failed with exception.", ex);
                if (--maxFailure <= 0) {
                    log.Warn("The maximum number of failures is reached.");
                    OnStop();
                }
            }
            StartTimers();
        }

        private void InitTimers() {
            int interval;

            // second timer
            if (!int.TryParse(ConfigurationManager.AppSettings["secondTimerInterval"], out interval)) {
                interval = 5;
            }
            secondTimer = new Timer(1000 * interval);
            secondTimer.Elapsed += secondTimer_Elapsed;

            // minute timer
            if (!int.TryParse(ConfigurationManager.AppSettings["minuteTimerInterval"], out interval)) {
                interval = 60;
            }
            minuteTimer = new Timer(1000 * 60 * interval);
            minuteTimer.Elapsed += minuteTimer_Elapsed;

            // Max failures
            if (!int.TryParse(ConfigurationManager.AppSettings["maxFailure"], out maxFailure)) {
                maxFailure = 10;
            }
        }

        private void StartTimers() {
            secondTimer.Enabled = true;
            minuteTimer.Enabled = true;
        }

        private void StopTimers() {
            secondTimer.Enabled = false;
            minuteTimer.Enabled = false;
        }
    }
}
