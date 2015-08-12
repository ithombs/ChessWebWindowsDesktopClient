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

        public ChessPieceMoveHelper(System.Windows.Controls.Canvas chessBoard)
        {
            this.chessBoard = chessBoard;
        }

        public void MovePiece(string msg)
        {
            
            

            string tileX, tileY, pieceID;

            pieceID = msg.Split('|')[0];
            tileX = msg.Split('|')[1];
            tileY = msg.Split('|')[2];
            

            Console.WriteLine(msg);

            //------------Do the actual piece moving here--------------
            ThumbChessPiece chessPiece;
            System.Windows.Shapes.Rectangle tile = null;

            //Get the tile that the move piece is moving to from the canvas
            //NOTE: Dispatcher is required in order to access the UIElement from the UI thread. Program crashes without use of the Dispatcher
            chessBoard.Dispatcher.Invoke((Action)(() =>
            {
                foreach (System.Windows.UIElement uie in chessBoard.Children)
                {
                    tile = uie as System.Windows.Shapes.Rectangle;

                    if (tile != null && tile.Name.Equals("board" + tileX + "_" + tileY))
                    {
                        Console.WriteLine("Found the tile!");
                        break;
                    }
                }

                //Get the chess piece from the canvas and move it
                foreach (System.Windows.UIElement uie in chessBoard.Children)
                {
                    chessPiece = uie as ThumbChessPiece;

                    if (chessPiece != null && chessPiece.pieceID == Int32.Parse(pieceID))
                    {
                        System.Windows.Controls.Canvas.SetTop(chessPiece, System.Windows.Controls.Canvas.GetTop(tile));
                        System.Windows.Controls.Canvas.SetLeft(chessPiece, System.Windows.Controls.Canvas.GetLeft(tile));
                        break;
                    }
                }
            }));

            

            
            
            
        }
    }
}
