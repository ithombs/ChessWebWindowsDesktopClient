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
            
            socketComm = new ChessWebWebSocketComm("ws://localhost:8080/WebSocks/test1");
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            socketComm.Close();
        }

        private void btnEnterQueue_Click(object sender, RoutedEventArgs e)
        {
            socketComm.EnterQueue();
        }

        /*
        private void ThumbChessPiece_DragDelta(object sender, System.Windows.Controls.Primitives.DragDeltaEventArgs e)
        {
            //Console.WriteLine("thumbChessPiece drag");
            ThumbChessPiece thumb = e.Source as ThumbChessPiece;
            //Console.WriteLine(thumb.ToString());

            Canvas.SetLeft(thumb, Canvas.GetLeft(thumb) + e.HorizontalChange);
            Canvas.SetTop(thumb, Canvas.GetTop(thumb) + e.VerticalChange);
        }
        */
    }
}
