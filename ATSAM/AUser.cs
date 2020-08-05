using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atsam
{
    public abstract class AUser
    {
        public static Boolean IsPasswordChanged = false;
        protected static string _UserID = string.Empty;
        protected static string _Password = string.Empty;
        protected static int intUserCode = -1;
        protected static string strUserName = string.Empty;
        protected static int intWorkGroupCode = -1;
        protected static string strWorkGroup = string.Empty;
        protected static int intWorkStationCode = -1;
        protected static string strWorkStation = string.Empty;
        protected static Boolean isUserChanged = false;
        protected static UserType utUserType = UserType.utNone;
        protected static ErrorCode ecErrorCode = ErrorCode.ecUserNotDefined;
        protected static string _IP = string.Empty;
        protected static APermission pPermission;

        public abstract string GetLocalIP();

        public static int UserCode
        {
            get
            {
                return (intUserCode);
            }
        }

        public static string UserName
        {
            get
            {
                return (strUserName);
            }
        }

        public static int WorkGroupCode
        {
            get
            {
                return (intWorkGroupCode);
            }
        }

        public static string WorkGroup
        {
            get
            {
                return (strWorkGroup);
            }
        }

        public static int WorkStationCode
        {
            get
            {
                return (intWorkStationCode);
            }
        }

        public static string WorkStation
        {
            get
            {
                return (strWorkStation);
            }
        }

        public static Boolean IsUserChanged
        {
            get
            {
                return (isUserChanged);
            }
        }

        public static UserType CurrentUserType
        {
            get
            {
                return (utUserType);
            }
        }

        public static ErrorCode Status
        {
            get
            {
                return (ecErrorCode);
            }
        }

        public static string IP
        {
            get
            {
                return (_IP);
            }
        }
        public static APermission _Permission
        {
            get
            {
                return (pPermission);
            }
        }
    }
}
