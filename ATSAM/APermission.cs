using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Atsam
{
    public abstract class APermission
    {
        public abstract Boolean[] GetPermission(int intWorkGroupCode, int intTableCode);
    }
}
