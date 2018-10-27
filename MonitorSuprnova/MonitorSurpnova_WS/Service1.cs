using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using DataAccess;

namespace MonitorSurpnova_WS
{
    public partial class Service1 : ServiceBase
    {
        public Service1()
        {
            InitializeComponent();
        }
        static bool AllowRunning = false;
        protected async override void OnStart(string[] args)
        {
            AllowRunning = true;
            DataAccess.WorkerModel.OnStart();
            while (AllowRunning)
            {
                await DataAccess.WorkerModel.ProcessData();
                await Task.Delay(TimeSpan.FromMinutes(DataAccess.WorkerModel.WaitMinutes));
            }
        }

        protected override void OnStop()
        {
            AllowRunning = false;
        }

        protected override void OnPause()
        {
            AllowRunning = false;
        }

        protected async override void OnContinue()
        {
            DataAccess.WorkerModel.OnStart();
            while (AllowRunning)
            {
                await DataAccess.WorkerModel.ProcessData();
                await Task.Delay(TimeSpan.FromMinutes(DataAccess.WorkerModel.WaitMinutes));
            }
        }
    }
}
