namespace AtsamServer
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
            this.splServiceProcessInstaller = new System.ServiceProcess.ServiceProcessInstaller();
            this.slServiceInstaller = new System.ServiceProcess.ServiceInstaller();
            // 
            // splServiceProcessInstaller
            // 
            this.splServiceProcessInstaller.Account = System.ServiceProcess.ServiceAccount.LocalSystem;
            this.splServiceProcessInstaller.Password = null;
            this.splServiceProcessInstaller.Username = null;
            // 
            // slServiceInstaller
            // 
            this.slServiceInstaller.Description = "ATSAM Service for Clients";
            this.slServiceInstaller.ServiceName = "ATSAM_SERVER";
            this.slServiceInstaller.StartType = System.ServiceProcess.ServiceStartMode.Automatic;
            // 
            // ProjectInstaller
            // 
            this.Installers.AddRange(new System.Configuration.Install.Installer[] {
            this.splServiceProcessInstaller,
            this.slServiceInstaller});

        }

        #endregion

        private System.ServiceProcess.ServiceProcessInstaller splServiceProcessInstaller;
        private System.ServiceProcess.ServiceInstaller slServiceInstaller;
    }
}