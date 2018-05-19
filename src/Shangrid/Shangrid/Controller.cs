using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shangrid
{
    public class Controller:BindableBase
    {

        public ConnectionCore Core { get; } = new ConnectionCore();

        public DelegateCommand StartConnection { get; } 
        public DelegateCommand StopConnection { get; } = new DelegateCommand();

        public string _connectionState;
        public string ConnectionState {
            get
            {
                return _connectionState;
            }
            set
            {
                SetProperty(ref _connectionState, value);
            }
        }

        public Controller()
        {
            StartConnection = new DelegateCommand() { Func = (o) => Start() };
            StopConnection = new DelegateCommand() { Func = (o) => Stop() };
            Core.ConnectionStateChanged += Core_ConnectionStateChanged;
            ConnectionState = Core.State.StateToString();
        }

        private void Core_ConnectionStateChanged(object sender, ConnectionEventArgs e)
        {
            ConnectionState = e.State.StateToString();
        }

        public void Start()
        {
            Core.StartAsync();
        }
        public void Stop()
        {
            Core.Stop();
        }
    }
}
