using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shangrid
{
    public class ConnectionEventArgs:EventArgs
    {
        public ConnectionState State { get; }
        public ConnectionEventArgs(ConnectionState state)
        {
            State = state;
        }
    }
}
