using Atsam;
using System;
using Microsoft.Win32;

namespace ServerCommonLayer
{
    // Server Administrator
    public class SA : ISA
    {
        private static string strConnectionString = string.Empty;
        private static int intPortNumber = -1;
        private static string strUserID = string.Empty;
        private static string strPassword = string.Empty;
        private static ErrorCode ecErrorCode = ErrorCode.ecNotInitialized;

        public SA()
        {
            if (!ecErrorCode.Equals(ErrorCode.ecNone))
                throw new NotImplementedException();
        }

        public SA(string strKey)
        {
            try
            {
                RegistryKey rkRegistry = Registry.LocalMachine.OpenSubKey("Software\\" + strKey.Trim(), RegistryKeyPermissionCheck.ReadSubTree);
                strConnectionString = rkRegistry.GetValue("ConnectionString").ToString().Trim().Substring(("Provider=SQLOLEDB.1;").Length).Trim();
                intPortNumber = Convert.ToInt32(rkRegistry.GetValue("PortNumber").ToString().Trim());
                ecErrorCode = ErrorCode.ecNone;
            }
            catch
            {
                ecErrorCode = ErrorCode.ecDBConnection;
            }
        }

        public string getConnectionString()
        {
            return (strConnectionString);
        }

        public int getPortNumber()
        {
            return (intPortNumber);
        }

        public string getUserID()
        {
            string[] strArray = strConnectionString.Split(';');
            for (int Index = 0; (Index < strArray.Length); Index++)
                if (strArray[Index].Contains("User ID="))
                {
                    strUserID = strArray[Index].Substring(("User ID=").Length);
                    break;
                }
            return (strUserID);
        }

        public string getPassword()
        {
            string[] strArray = strConnectionString.Split(';');
            for (int Index = 0; (Index < strArray.Length); Index++)
                if (strArray[Index].Contains("Password"))
                {
                    strPassword = strArray[Index].Substring(("Password=").Length);
                    break;
                }
            return (strPassword);
        }

        public ErrorCode getStatus()
        {
            return (ecErrorCode);
        }
    }
}
