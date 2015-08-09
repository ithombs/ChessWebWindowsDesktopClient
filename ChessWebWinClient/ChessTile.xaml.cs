using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ChessWebWinClient
{
    /// <summary>
    /// Interaction logic for ChessTile.xaml
    /// </summary>
    public partial class ChessTile : UserControl
    {
        public String position { get; set; }
        public int pieceID { get; set; }
        public ImageSource pieceImage { get; set; }

        public ChessTile()
        {
            InitializeComponent();
        }

        private void Image_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Console.WriteLine("From tile - " + position);
        }
    }
}
