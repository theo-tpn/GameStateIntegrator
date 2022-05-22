using GameStateIntegrator.Entities;
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

        private int _listeningPort;

        private static Dictionary<string, Type> _propertiesTypeMap = new()
        {
            { "provider", typeof(Provider) },
            { "player", typeof(Player) },
            { "bomb", typeof(Bomb) },
            { "round", typeof(Round) },
            { "phase", typeof(PhaseInfo) },
            { "map", typeof(Map) }

        };

        private Subject<Provider> _providerSubject = new();
        private Subject<Player> _playerSubject = new();
        private Subject<Bomb> _bombSubject = new();
        private Subject<Round> _roundSubject = new();
        private Subject<PhaseInfo> _phaseSubject = new();
        private Subject<Map> _mapSubject = new();

        public bool IsRunning { get; private set; }

        public IObservable<Provider> ProviderObserver => _providerSubject.AsObservable();
        public IObservable<Player> PlayerObserver => _playerSubject.AsObservable();
        public IObservable<Bomb> BombObserver => _bombSubject.AsObservable();
        public IObservable<Round> RoundObserver => _roundSubject.AsObservable();
        public IObservable<PhaseInfo> PhaseObserver => _phaseSubject.AsObservable();
        public IObservable<Map> MapObserver => _mapSubject.AsObservable();

        public GsListener(int port)
        {
            _listeningPort = port;
        }

        public bool Start()
        {
            _httpListener = new HttpListener();
            _httpListener.Prefixes.Add($"http://localhost:{_listeningPort}/");

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
                return;
            }
            finally
            {
                _waitForConnection.Set();
            }

            var request = context.Request;
            if (request.HttpMethod != "POST")
                return;

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
            JsonObject? r;
            try
            {
                r = JsonNode.Parse(content)?.AsObject();
                if (r == null)
                    return;
            }
            catch (Exception)
            {
                return;
            }

            foreach (var (propName, propType) in _propertiesTypeMap)
            {
                if (!r.TryGetPropertyValue(propName, out var value))
                    continue;
                if (value == null)
                    continue;
                try
                {
                    var propObj = JsonSerializer.Deserialize(value.ToJsonString(), propType);

                    switch (propObj)
                    {
                        case Provider provider:
                            _providerSubject.OnNext(provider);
                            break;
                        case Player player:
                            _playerSubject.OnNext(player);
                            break;
                        case Bomb bomb:
                            _bombSubject.OnNext(bomb);
                            break;
                        case Round round:
                            _roundSubject.OnNext(round);
                            break;
                        case PhaseInfo phaseInfo:
                            _phaseSubject.OnNext(phaseInfo);
                            break;
                        case Map map:
                            _mapSubject.OnNext(map);
                            break;
                    }
                }
                catch (Exception)
                {
                    continue;
                }

            }
        }
    }
}