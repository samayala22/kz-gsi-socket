using CSGSI;
using Newtonsoft.Json.Linq;
using SuperSocket.WebSocket;

namespace GSISocket.Socket
{
    public class GSISocketServer
    {
        public int SocketPort { get; }
        public bool UsingWS { get; set; }
        public GameState Cache { get; set; }
        public GameStateListener Listener { get; }
        public WebSocketServer SocketServer { get; set; }

        private readonly Dictionary<string, string> sessions = new Dictionary<string, string>();

        public GSISocketServer(int socketPort = 4001)
        {
            this.SocketPort = socketPort;
            this.Cache = new GameState("{}");
            this.Listener = new GameStateListener(socketPort - 1);
            this.Listener.NewGameState += new NewGameStateHandler(this.OnGameState);
        }

        public void Start() => this.Listener.Start();

        public void SetupSockets()
        {
            this.SocketServer = new WebSocketServer();
            this.SocketServer.Setup(this.SocketPort);
            this.SocketServer.NewMessageReceived += this.OnRequest_WS;

            this.UsingWS = this.SocketServer.Start();
        }

        private void OnGameState(GameState state)
        {
            if (state.JSON != "{}")
            {
                this.Cache = state;

                if (this.UsingWS)
                {
                    foreach(var session in this.SocketServer.GetAllSessions())
                    {
                        this.sessions.TryGetValue(session.SessionID, out var propString);
                        try
                        {
                           session.Send(this.GetValueOfPropertyString(propString));
                        }
                        catch
                        {

                        }
                    }
                }
            }
        }

        private void OnRequest_WS(WebSocketSession session, string message)
        {
            this.sessions.Add(session.SessionID, message);
            session.Send(this.GetValueOfPropertyString(message));
        }

        private string GetValueOfPropertyString(string propString)
        {
            var currentJson = this.Cache.JSON;

            if (propString == "" || propString == "json")
            {
                return currentJson;
            }

            var returnValue = "Property does not exist";
            var requestProps = propString.Split('.');

            for (var i = 0; i < requestProps.Length; i++)
            {
                var obj = JObject.Parse(currentJson);

                if (i == requestProps.Length - 1)
                {
                    if (obj.ContainsKey(requestProps[i]))
                    {
                        returnValue = obj.GetValue(requestProps[i]).ToString();
                    }
                }
                else
                {
                    currentJson = obj.GetValue(requestProps[i]).ToString();
                }
            }

            return returnValue;
        }
    }
}