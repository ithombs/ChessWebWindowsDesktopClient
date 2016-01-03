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
        private BasicMoveData moveData;

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
            moveData = new BasicMoveData();
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
               
                MainWindow.oppName.Dispatcher.Invoke((Action)(() =>
                {
                    MainWindow.oppName.Content = opponentName;
                }));
            }
            else if(e.Message.StartsWith("side:"))
            {
                string side = e.Message.Split(':')[1];
                Console.WriteLine(e.Message.Split(':')[1]);

                MainWindow.side.Dispatcher.Invoke((Action)(() =>
                {
                    MainWindow.side.Content = "Side: " + side;
                }));
            }
            else if(e.Message.StartsWith("ml1:"))
            {
                string from = e.Message.Split(':')[1];
                moveData.from = from;
                Console.WriteLine("{0}", from);
            }
            else if(e.Message.StartsWith("ml2:"))
            {
                string to = e.Message.Split(':')[1];
                moveData.to= to;
                Console.WriteLine("{0}", to);
                addMoveToList(moveData);
            }
        }

        private void addMoveToList(BasicMoveData moveData)
        {
            //Console.WriteLine("{0} - {1}", moveData.from, moveData.to);
            moveData.to = MainWindow.tileNames[moveData.to];
            moveData.from = MainWindow.tileNames[moveData.from];

            MainWindow.sMoveList.Dispatcher.Invoke((Action)(() =>
            {
                MainWindow.sMoveList.Items.Add(moveData);
            }));
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
            //move a piece without direct user interaction(uses ChessPieceMoveHelper class to do so. Cause abstraction.)
            moveHelper.MovePiece(msg);

            //add move data to moveList
        }

        public void SendMove(string msg)
        {
            webSocket.Send(msg);
            Console.WriteLine("Sending move: {0}", msg);
            //add move data to moveList
            MainWindow.sMoveList.Dispatcher.Invoke((Action)(() =>
            {
                //BasicMoveData newMove = new BasicMoveData() { };
                //MainWindow.sMoveList.Items.Add
            }));
        }
    }
}
