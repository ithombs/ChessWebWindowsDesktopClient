using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls.Primitives;

namespace ChessWebWinClient
{
    class ThumbChessPiece : Thumb
    {
        public string position { get; set; }
        public int pieceID { get; set; }
    }
}
