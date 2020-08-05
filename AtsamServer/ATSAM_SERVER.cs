using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.ServiceProcess;
using System.Text;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Http;

using BusinessFacadeLayer;
using ServerCommonLayer;
using Atsam;

namespace AtsamServer
{
    partial class ATSAM_SERVER : ServiceBase
    {
        public ATSAM_SERVER()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            string strURI = "ATSAM";
            SA pSA = new SA(strURI);
            try
            {
                if (pSA.getStatus() == ErrorCode.ecNone)
                {
                    HttpChannel hc = new HttpChannel(pSA.getPortNumber());
                    ChannelServices.RegisterChannel(hc, false);
                    RemotingConfiguration.RegisterWellKnownServiceType(typeof(BFL), strURI + "URI", WellKnownObjectMode.Singleton);
                }
            }
            catch (Exception e)
            {
                System.Diagnostics.EventLog elEventLog = new System.Diagnostics.EventLog();
                elEventLog.WriteEntry(e.Message, EventLogEntryType.Error);
            }
        }

        protected override void OnStop()
        {
            // TODO: Add code here to perform any tear-down necessary to stop your service.
        }
    }
}
