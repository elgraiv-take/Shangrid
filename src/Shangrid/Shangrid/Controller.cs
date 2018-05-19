using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shangrid
{
    public class Controller:BindableBase,IDisposable
    {

        private ConnectionCore m_core= new ConnectionCore();
        public ConnectionCore Core { get { return m_core; } }

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

        #region IDisposable Support
        private bool disposedValue = false; // 重複する呼び出しを検出するには

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    m_core.Dispose();
                }
                
                disposedValue = true;
            }
        }
        

        // このコードは、破棄可能なパターンを正しく実装できるように追加されました。
        public void Dispose()
        {
            // このコードを変更しないでください。クリーンアップ コードを上の Dispose(bool disposing) に記述します。
            Dispose(true);
        }
        #endregion
    }
}
