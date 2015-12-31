using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebSocket4Net;


namespace ChessWebWinClient
{
    class ChessWebWebSocketComm
    {
        private WebSocket webSocket;
        private string uri;
        private ChessPieceMoveHelper moveHelper;

        public ChessWebWebSocketComm(string uri)
        {
            this.uri = uri;

            webSocket = new WebSocket(uri);
            webSocket.Opened += WebSocket_Opened;
            webSocket.MessageReceived += WebSocket_MessageReceived;
            webSocket.Closed += WebSocket_Closed;

            webSocket.Open();
        }

        public ChessWebWebSocketComm(string uri, System.Windows.Controls.Canvas canvas)
        {
            this.uri = uri;

            webSocket = new WebSocket(uri);
            webSocket.Opened += WebSocket_Opened;
            webSocket.MessageReceived += WebSocket_MessageReceived;
            webSocket.Closed += WebSocket_Closed;

            moveHelper = new ChessPieceMoveHelper(canvas);
            webSocket.Open();
        }

        private void WebSocket_Opened(object sender, EventArgs e)
        {
            Console.WriteLine("Connection established to - {0}", uri);
        }

        private void WebSocket_MessageReceived(object sender, MessageReceivedEventArgs e)
        {
            Console.WriteLine("Message received: {0}", e.Message);

            if(e.Message.StartsWith("move:"))
            {
                MovePiece(e.Message.Split(':')[1]);
            }
            else if(e.Message.StartsWith("opponent:"))
            {
                string opponentName = e.Message.Split(':')[1];
                Console.WriteLine(opponentName);
               
                MainWindow.testingLabel.Dispatcher.Invoke((Action)(() =>
                {
                    MainWindow.testingLabel.Content = opponentName;
                }));
            }
            else if(e.Message.StartsWith("side:"))
            {
                Console.WriteLine(e.Message.Split(':')[1]);
            }
            else if(e.Message.StartsWith("ml1:"))
            {

            }
            else if(e.Message.StartsWith("ml2:"))
            {

            }
        }

        private void WebSocket_Closed(object sender, EventArgs e)
        {
            Console.WriteLine("Connection ended with - {0}", uri);
        }

        public void Close()
        {
            webSocket.Close();
        }

        /*
            To enter queue for a PvP game, the connection string is as follows: "connect:" + userName
            A username of "guest" is used for non-logged in users.
        */
        public void EnterQueue(string userName = "guest")
        {
            
            string connectionString = "connect:";

            webSocket.Send(connectionString + userName);
        }

        /*
        The below methods deal with incoming messages from the websocket
        */
        public void MovePiece(string msg)
        {
            //move a piece without direct user interaction(uses ChessPieceMoveHelper class to do so, cause abstraction)
            moveHelper.MovePiece(msg);
        }

        public void SendMove(string msg)
        {
            webSocket.Send(msg);
        }
    }
}
