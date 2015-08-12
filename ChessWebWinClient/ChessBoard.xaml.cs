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
    /// Interaction logic for ChessBoard.xaml
    /// </summary>
    public partial class ChessBoard : UserControl
    {
        List<HitTestResult> hitResultsList;

        public ChessBoard()
        {
            InitializeComponent();
            hitResultsList = new List<HitTestResult>();
            
        }
        //TODO: make events that can send the moves and such
        public delegate void PieceMovedEventHandler(int id, string tile);
        public event PieceMovedEventHandler PieceMoved;

        public void RaisePieceMoved(int id, string tile)
        {
            if(PieceMoved != null)
            {
                PieceMoved(id, tile);
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

        private void testPiece1_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            hitResultsList.Clear();

            ThumbChessPiece piece = (ThumbChessPiece)sender;
            Console.WriteLine("Top:{0} | Left: {1}", Canvas.GetTop(piece), Canvas.GetLeft(piece));

            VisualTreeHelper.HitTest(chessCanvas, null, 
                new HitTestResultCallback(MyHitTestResults), 
                new PointHitTestParameters(e.GetPosition(chessCanvas)));

            Rectangle r = (Rectangle)hitResultsList.Last().VisualHit;
            Console.WriteLine("From Mouse Down: " + r.Name);
            Console.WriteLine("Top:{0} | Left: {1}", Canvas.GetTop(r), Canvas.GetLeft(r));
            
            //Snap the piece to the square it was dropped in (where the mouse was released)
            Canvas.SetTop(piece, Canvas.GetTop(r));
            Canvas.SetLeft(piece, Canvas.GetLeft(r));

            RaisePieceMoved(piece.pieceID, r.Name);
        }

        public HitTestResultBehavior MyHitTestResults(HitTestResult result)
        {
            hitResultsList.Add(result);

            return HitTestResultBehavior.Continue;
        }

    }
}
