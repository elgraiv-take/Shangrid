using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.Serialization.Json;

namespace Shangrid
{
    public class ConnectionCore:IDisposable
    {
        public event CommandChangeFunc ChangeEvent;
        public event CommandSelectFunc SelectEvent;
        public event CommandSetupFunc SetupEvent;
        private NetworkStream m_stream;
        private TcpListener m_tcpServer;
        private CancellationTokenSource m_cancel;
        private Task m_task;

        public event EventHandler<ConnectionEventArgs> ConnectionStateChanged;

        private ConnectionState m_state;
        public ConnectionState State
        {
            get
            {
                return m_state;
            }
            private set
            {
                if (m_state != value)
                {
                    m_state = value;
                    ConnectionStateChanged?.Invoke(this, new ConnectionEventArgs(m_state));
                }
            }
        }

        public ConnectionCore()
        {
            m_state = ConnectionState.Closed;
        }

        public async void StartAsync()
        {
            m_tcpServer = new TcpListener(IPAddress.Any,10502);
            m_tcpServer.Start();
            State = ConnectionState.Listening;
            try
            {
                var client = await m_tcpServer.AcceptTcpClientAsync();
                m_stream=client.GetStream();
            }
            catch (Exception)
            {
                State = ConnectionState.Closed;
                return;
            }
            finally
            {
                m_tcpServer.Stop();
            }

            State = ConnectionState.Connected;
            Polling();
        }
        public void Stop()
        {

            m_tcpServer?.Stop();
            m_cancel?.Cancel();
            m_stream?.Close();
            m_task?.Wait(1000 * 5);

        }

        private void Polling()
        {

            m_cancel = new CancellationTokenSource();
            var token = m_cancel.Token;
            m_task = Task.Factory.StartNew(() => { pollingFunc(token); }, token, TaskCreationOptions.LongRunning, TaskScheduler.Default);
            
        }
        private void pollingFunc(CancellationToken token)
        {
            using (var reader = new StreamReader(m_stream))
            {
                while (true)
                {

                    try
                    {
#if true
                        var task = reader.ReadLineAsync();
                        task.Wait(token);
                        if (task.IsCanceled)
                        {
                            break;
                        }
                        if (task.Result == null)
                        {
                            break;
                        }
                        analyzeCommand(task.Result);
#else
                        var line = reader.ReadLine();
                        if (line != null)
                        {
                            analyzeCommand(line);
                        }
#endif
                    }
                    catch
                    {
                        //タイムアウト
                    }
                    if (token.IsCancellationRequested)
                    {
                        break;
                    }
                }


            }
            State = ConnectionState.Closed;
        }
        private void analyzeCommand(string message)
        {
            
            using (var stream = new MemoryStream(Encoding.UTF8.GetBytes(message)))
            {
                var setting = new DataContractJsonSerializerSettings();
                var serializer = new DataContractJsonSerializer(typeof(Command.CommandHeader<object>), setting);
                var command = serializer.ReadObject(stream) as Command.CommandHeader<object>;

                stream.Seek(0, SeekOrigin.Begin);

                var commandType = command.CommandBodyType;
                switch (commandType)
                {
                    case Command.CommandType.Setup:
                        serializer = new DataContractJsonSerializer(typeof(Command.CommandHeader<Command.CommandSetup>), setting);
                        break;
                    case Command.CommandType.Change:
                        serializer = new DataContractJsonSerializer(typeof(Command.CommandHeader<Command.CommandChange>), setting);
                        break;
                    default:
                        break;
                }
                var data = serializer.ReadObject(stream);


                if (data is Command.CommandHeader<Command.CommandChange>)
                {
                    ExcecuteChange(((Command.CommandHeader<Command.CommandChange>)data).CommandBody);
                }
                if (data is Command.CommandHeader<Command.CommandSetup>)
                {
                    ExcecuteSetup(((Command.CommandHeader<Command.CommandSetup>)data).CommandBody);
                }
            }
        }

        public void SendChangeCommand(Command.CommandChange command)
        {
            var setting = new DataContractJsonSerializerSettings();
            var serializer = new DataContractJsonSerializer(typeof(Command.CommandHeader<Command.CommandChange>), setting);
            serializer.WriteObject(m_stream, new Command.CommandHeader<Command.CommandChange>() {
                CommandBodyType = Command.CommandType.Change,
                CommandBody = command,
            });
            var endl = "\n";
            var endlb = Encoding.UTF8.GetBytes(endl);
            m_stream.Write(endlb,0,endlb.Count());
        }

        private void ExcecuteChange(Command.CommandChange command)
        {
            ChangeEvent?.Invoke(command);
        }
        private void ExcecuteSelect(Command.CommandSelect command)
        {
            SelectEvent?.Invoke(command);
        }
        private void ExcecuteSetup(Command.CommandSetup command)
        {
            SetupEvent?.Invoke(command);
        }

#region IDisposable Support
        private bool disposedValue = false; // 重複する呼び出しを検出するには

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    m_cancel.Dispose();
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
