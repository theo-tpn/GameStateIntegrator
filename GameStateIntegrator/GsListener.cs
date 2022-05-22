﻿using GameStateIntegrator.Entities;
using System.Net;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace GameStateIntegrator
{
    public class GsListener
    {
        private HttpListener? _httpListener;
        private AutoResetEvent _waitForConnection = new(false);

        private static Dictionary<string, Type> _propertiesTypeMap = new()
        {
            { "provider", typeof(Provider) },
            { "player", typeof(Player) },
            { "bomb", typeof(Bomb) },
            { "round", typeof (Round) },
            { "phase", typeof(PhaseInfo) },
            { "map", typeof(Map) }

        };

        public bool IsRunning { get; private set; }

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
            while (IsRunning)
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
            var r = JsonNode.Parse(content)?.AsObject();
            if (r == null)
                return;

            foreach(var (propName, propType) in _propertiesTypeMap)
            {
                if(!r.TryGetPropertyValue(propName, out var value))
                    continue;
                if (value == null)
                    continue;
                try
                {
                    var propObj = JsonSerializer.Deserialize(value.ToJsonString(), propType);
                }
                catch (Exception)
                {
                    continue;
                }

            }

        }
    }
}