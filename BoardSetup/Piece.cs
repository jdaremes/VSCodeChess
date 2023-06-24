using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoardSetup
{
    public enum Direction 
    { 
                    N,
              NW,        NE,
          W,                  E,
              SW,        SE,
                    S
    }

    public abstract class Piece
    {
        public static readonly int       White = 8;
        public static readonly int       Black = 7;

        public static readonly int       Empty = 0;
        public static readonly int        King = 1;
        public static readonly int        Pawn = 2;
        public static readonly int      Knight = 3;
        public static readonly int      Bishop = 4;
        public static readonly int        Rook = 5;
        public static readonly int       Queen = 6;

        private int position;
        private int color;
        private int pieceInfo;

        public abstract Boolean canMove(Board board);

        public abstract HashSet<int> possibleMoves(Board board);

        public HashSet<int> generateAllPossibleMoves(Board board) { return null; }

        public ArrayList generatePossibleMoves(Direction[] arr, Board board, int pos, int pieceInfo)
        {
            ArrayList legalMoves = new ArrayList();

            // An empty array will signify a knight
            if ( arr.Length == 0 )
            {
                Move.KnightMoveLogic(board, pos, pieceInfo, legalMoves);
            } else if (arr.Length == 1)
            {
                Move.PawnMoveLogic(board, pos, pieceInfo, legalMoves);
            } else if (arr.Length == 2)
            {
                // KingMoveLogic
            } else
            {
                Move.PossibleMovesByDirection(arr, board, pos, pieceInfo, legalMoves);
            }

            return legalMoves;
        }

    }
}