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
        WebSocket ws;

        public MainWindow()
        {
            InitializeComponent();
            ws = new WebSocket("ws://localhost:8080/WebSocks/test1");
            ws.Opened += Ws_Opened;
            ws.MessageReceived += Ws_MessageReceived;
            ws.Closed += Ws_Closed;
            ws.Open();
            
        }

        private void Ws_Closed(object sender, EventArgs e)
        {
            Console.WriteLine("Connection closed: " + e.ToString());
        }

        private void Ws_MessageReceived(object sender, MessageReceivedEventArgs e)
        {
            Console.WriteLine("Message Recieved: " + e.Message);
        }

        private void Ws_Opened(object sender, EventArgs e)
        {
            Console.WriteLine("Connection opened...");
            ws.Send("Connecting from ChessWebWinClient");
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            ws.Close();
        }


        private void board0_0_MouseDown(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show("0|0");
            
        }

        private void id0_MouseDown(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show("id-0");
        }

        private void id8_MouseMove(object sender, MouseEventArgs e)
        {
            Image i = sender as Image;
            if(i != null && e.LeftButton == MouseButtonState.Pressed)
            {
                DragDrop.DoDragDrop(i, i.Source, DragDropEffects.Copy);
            }
        }

        private void ThumbChessPiece_DragDelta(object sender, System.Windows.Controls.Primitives.DragDeltaEventArgs e)
        {
            //Console.WriteLine("thumbChessPiece drag");
            ThumbChessPiece thumb = e.Source as ThumbChessPiece;
            //Console.WriteLine(thumb.ToString());

            Canvas.SetLeft(thumb, Canvas.GetLeft(thumb) + e.HorizontalChange);
            Canvas.SetTop(thumb, Canvas.GetTop(thumb) + e.VerticalChange);
        }
    }
}
