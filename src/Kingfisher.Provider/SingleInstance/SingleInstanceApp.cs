using System;
using System.Diagnostics;
using System.IO;
using System.IO.Pipes;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Kingfisher.Provider.SingleInstance
{
    public static class SingleInstanceApp
    {
        private static readonly CancellationTokenSource _tokenSource = new CancellationTokenSource();

        public static bool IsSingleInstance(string name)
        {
            name += "." + Environment.UserName; // make it independent from the current user.

            var sendOk = TrySendArgumentsToServer(name);
            if (sendOk)
                // if we could reach the first application, we are obviously not the first one.
                return false;

            StartServer(name);

            return true;
        }

        public static event EventHandler<SingleInstanceEventArgs> SecondInstanceStarted;

        public static void Cleanup()
        {
            _tokenSource.Cancel();
        }

        private static void StartServer(string name)
        {
            Task.Factory.StartNew(async () =>
            {
                try
                {
                    await using var pipe = new NamedPipeServerStream(name, PipeDirection.In, 1, PipeTransmissionMode.Message, PipeOptions.Asynchronous);
                    while (true)
                    {
                        await pipe.WaitForConnectionAsync(_tokenSource.Token);

                        using (var reader = new StreamReader(pipe, Encoding.UTF8, true, -1, true))
                        {
                            var line = await reader.ReadToEndAsync();
                            var args = JsonConvert.DeserializeObject<string[]>(line);
                            SecondInstanceStarted?.Invoke(null, new SingleInstanceEventArgs(args));
                        }

                        pipe.Disconnect();
                    }
                }
                catch (TaskCanceledException)
                {
                    Debug.WriteLine("Name Pipe Server was canceled.");
                }
            }, _tokenSource.Token, TaskCreationOptions.LongRunning, TaskScheduler.Current);
        }

        private static bool TrySendArgumentsToServer(string name)
        {
            try
            {
                using (var pipe = new NamedPipeClientStream(".", name, PipeDirection.Out))
                {
                    pipe.Connect(100);

                    var args = Environment.GetCommandLineArgs();
                    var json = JsonConvert.SerializeObject(args);

                    using (var writer = new StreamWriter(pipe, Encoding.UTF8, -1, true))
                    {
                        writer.Write(json);
                    }

                    return true;
                }
            }
            catch (TimeoutException)
            {
                // we cant connect? -> no first instance of application is running -> we are the first
                return false;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Could not write arguments to client {name}.");
                Debug.WriteLine(ex);
                return false;
            }
        }
    }
}