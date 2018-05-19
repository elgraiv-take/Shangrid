using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shangrid
{
    public enum ConnectionState
    {
        Closed,
        Listening,
        Connected,
    }
    public static class ConnectionStateUtil
    {
        public static string StateToString(this ConnectionState e)
        {
            return e.ToString();
        }
    }
}
