using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shangrid
{
    class DummyModule
    {
        public event CommandChangeFunc Changed;
        public event CommandSelectFunc Selected;
        public event CommandSetupFunc Setuped;

        private List<object> m_commandList;

        public DummyModule()
        {
            m_commandList = DummyCommand.GetTestCommand();
        }

        public async void Start()
        {
            await Task.Run(()=> {
                System.Threading.Thread.Sleep(5000);
                foreach(var command in m_commandList)
                {
                    if(command is Command.CommandChange)
                    {
                        ExcecuteChange((Command.CommandChange)command);
                    }
                    if (command is Command.CommandSelect)
                    {
                        ExcecuteSelect((Command.CommandSelect)command);
                    }
                    if (command is Command.CommandSetup)
                    {
                        ExcecuteSetup((Command.CommandSetup)command);
                    }
                    System.Threading.Thread.Sleep(2000);
                }
            });
        }

        private void ExcecuteChange(Command.CommandChange command)
        {
            Changed?.Invoke(command);
        }
        private void ExcecuteSelect(Command.CommandSelect command)
        {
            Selected?.Invoke(command);
        }
        private void ExcecuteSetup(Command.CommandSetup command)
        {
            Setuped?.Invoke(command);
        }
    }
}
