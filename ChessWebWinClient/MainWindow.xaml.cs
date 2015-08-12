using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WebSocket4Net;

namespace ChessWebWinClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ChessWebWebSocketComm socketComm;

        public MainWindow()
        {
            InitializeComponent();
            
            socketComm = new ChessWebWebSocketComm("ws://localhost:8080/WebSocks/test1", chessBoard.chessCanvas);

            
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            socketComm.Close();
        }

        private void btnEnterQueue_Click(object sender, RoutedEventArgs e)
        {
            socketComm.EnterQueue();
        }

        //Send the move data to the server
        private void chessBoard_PieceMoved(int id, string tile)
        {
            lblTesting.Content = id + tile;
            string moveProtocol = id + "|" + tile.Substring(5, 1) + "|" + tile.Substring(7, 1);
            Console.WriteLine(moveProtocol);

            socketComm.SendMove(moveProtocol);
        }

        private void btnTestMove_Click(object sender, RoutedEventArgs e)
        {
            ThumbChessPiece p;
            foreach(UIElement uie in chessBoard.chessCanvas.Children)
            {
                p = uie as ThumbChessPiece;

                if(p != null && p.pieceID == 0)
                {
                    Canvas.SetTop(p, 50);
                    Canvas.SetLeft(p, 50);
                }
            }
        }
    }
}
