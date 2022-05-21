using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System.Net;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace GameStateIntegrator
{
    public class GsListener
    {
        public bool IsRunning { get; private set; }

        private AutoResetEvent _waitForConnection = new(false);
        private HttpListener? _httpListener;


        public GsListener()
        {
        }

        public bool Start()
        {
            _httpListener = new HttpListener();
            _httpListener.Prefixes.Add("http://localhost:3000/");

            _httpListener.Start();

            var listeningThread = new Thread(new ThreadStart(Run));

            try
            {
                _httpListener.Start();
            }
            catch (Exception)
            {

                return false;
            }

            IsRunning = true;
            listeningThread.Start();

            return true;
        }

        public void Stop()
        {
            IsRunning = false;
            _httpListener?.Close();
            (_httpListener! as IDisposable).Dispose();
        }

        public void Run()
        {
            while(IsRunning)
            {
                _httpListener?.BeginGetContext(ReceiveGameState, _httpListener);
                _waitForConnection.WaitOne();
                _waitForConnection.Reset();
            }
            try
            {
                _httpListener?.Stop();
            }
            catch (ObjectDisposedException)
            { /* _listener was already disposed, do nothing */ }
        }

        private void ReceiveGameState(IAsyncResult result)
        {
            HttpListenerContext context;
            try
            {
                context = _httpListener?.EndGetContext(result)!;
            }
            catch (ObjectDisposedException)
            {
                // Listener was Closed due to call of Stop();
                return;
            }
            finally
            {
                _waitForConnection.Set();
            }

            var request = context.Request;
            string jsonContent;

            using (var inputStream = request.InputStream)
            {
                using StreamReader sr = new(inputStream);
                jsonContent = sr.ReadToEnd();
            }

            using HttpListenerResponse response = context.Response;
            response.StatusCode = (int)HttpStatusCode.OK;
            response.Close();

            ProcessIncomingContent(jsonContent);
        }

        private void ProcessIncomingContent(string content)
        {
            dynamic r = JsonNode.Parse(content);
            var o = JsonSerializer.Deserialize<dynamic>(content);

            Console.WriteLine(r.provider.name);
        }
    }
}