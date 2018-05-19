using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shangrid
{
    public delegate void CommandSelectFunc(Command.CommandSelect command);
    public delegate void CommandChangeFunc(Command.CommandChange command);
    public delegate void CommandSetupFunc(Command.CommandSetup command);

}
