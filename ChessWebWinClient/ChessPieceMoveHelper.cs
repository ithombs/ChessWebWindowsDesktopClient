using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
            string tile, pieceID;

            pieceID = msg.Split('|')[0];
            tile = msg.Split('|')[1] + msg.Split('|')[2];

            Console.WriteLine(msg);
            //Do the actual piece moving here
        }
    }
}
