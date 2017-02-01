using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessWebWinClient
{
    class ChessPieceMoveHelper
    {
        private System.Windows.Controls.Canvas chessBoard;
        private List<System.Windows.Shapes.Rectangle> tiles;
        private List<ThumbChessPiece> chessPieces;

        public ChessPieceMoveHelper(System.Windows.Controls.Canvas chessBoard)
        {
            this.chessBoard = chessBoard;
            tiles = new List<System.Windows.Shapes.Rectangle>();
            chessPieces = new List<ThumbChessPiece>();

            foreach (System.Windows.UIElement uie in chessBoard.Children)
            {
                if(uie is System.Windows.Shapes.Rectangle)
                {
                    tiles.Add(uie as System.Windows.Shapes.Rectangle);
                } else if(uie is ThumbChessPiece)
                {
                    chessPieces.Add(uie as ThumbChessPiece);
                }
            }
        }

        public void MovePiece(string msg)
        {
            string tileX, tileY, pieceID;
            string from, to;

            pieceID = msg.Split('|')[0];
            tileX = msg.Split('|')[1];
            tileY = msg.Split('|')[2];
            
            Console.WriteLine(msg);

            //------------Do the actual piece moving here--------------
            ThumbChessPiece chessPiece;
            System.Windows.Shapes.Rectangle tile = null;

            //Get the tile that the moved piece is moving to from the canvas
            //NOTE: Dispatcher is required in order to access the UIElement from the UI thread. Program crashes without use of the Dispatcher
            chessBoard.Dispatcher.Invoke((Action)(() =>
            {
                tile = tiles.Single(t => t.Name.Equals("board" + tileX + "_" + tileY));

                //Get the chess piece from the canvas and move it
                chessPiece = chessPieces.Single(piece => piece.pieceID == Int32.Parse(pieceID));

                System.Windows.Controls.Canvas.SetTop(chessPiece, System.Windows.Controls.Canvas.GetTop(tile));
                System.Windows.Controls.Canvas.SetLeft(chessPiece, System.Windows.Controls.Canvas.GetLeft(tile));

            }));
        }

        public void ResetChessBoard()
        {
            chessBoard.Dispatcher.Invoke((Action)(() => 
            {
                for(int i = 0; i < 16; i++)
                {
                    ThumbChessPiece chessPiece = chessPieces[i];
                    System.Windows.Shapes.Rectangle tile = tiles[i];

                    System.Windows.Controls.Canvas.SetTop(chessPiece, System.Windows.Controls.Canvas.GetTop(tile));
                    System.Windows.Controls.Canvas.SetLeft(chessPiece, System.Windows.Controls.Canvas.GetLeft(tile));
                }

                int p = 31;
                for (int i = 63; i > 63-16; i--)
                {
                    
                    ThumbChessPiece chessPiece = chessPieces[p];
                    System.Windows.Shapes.Rectangle tile = tiles[i];

                    System.Windows.Controls.Canvas.SetTop(chessPiece, System.Windows.Controls.Canvas.GetTop(tile));
                    System.Windows.Controls.Canvas.SetLeft(chessPiece, System.Windows.Controls.Canvas.GetLeft(tile));

                    p--;
                }
            }));
        }
    }
}
