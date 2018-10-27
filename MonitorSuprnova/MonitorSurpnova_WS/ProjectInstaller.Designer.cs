namespace MonitorSurpnova_WS
{
    partial class ProjectInstaller
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

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.MonitorSuprnova_WS = new System.ServiceProcess.ServiceProcessInstaller();
            this.MonitorSuprnovaInstaller = new System.ServiceProcess.ServiceInstaller();
            // 
            // MonitorSuprnova_WS
            // 
            this.MonitorSuprnova_WS.Account = System.ServiceProcess.ServiceAccount.LocalService;
            this.MonitorSuprnova_WS.Password = null;
            this.MonitorSuprnova_WS.Username = null;
            // 
            // MonitorSuprnovaInstaller
            // 
            this.MonitorSuprnovaInstaller.ServiceName = "MonitorSuprnova_Service";
            this.MonitorSuprnovaInstaller.StartType = System.ServiceProcess.ServiceStartMode.Automatic;
            // 
            // ProjectInstaller
            // 
            this.Installers.AddRange(new System.Configuration.Install.Installer[] {
            this.MonitorSuprnova_WS,
            this.MonitorSuprnovaInstaller});

        }

        #endregion

        private System.ServiceProcess.ServiceProcessInstaller MonitorSuprnova_WS;
        private System.ServiceProcess.ServiceInstaller MonitorSuprnovaInstaller;
    }
}