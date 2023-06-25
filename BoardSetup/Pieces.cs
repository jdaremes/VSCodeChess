using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoardSetup
{
    class Pawn : Piece
    {
        private int pos;
        private int pieceInfo;

        public override bool canMove(Board board)
        {
            throw new NotImplementedException();
        }

        public override ArrayList possibleMoves(Board board)
        {
            return generatePossibleMoves(new Direction[1] { Direction.N }, 
                                            board, pos, pieceInfo);
        }
    }

    class Knight : Piece
    {
        private int pos;
        private int pieceInfo;

        public override bool canMove(Board board)
        {
            throw new NotImplementedException();
        }

        public override ArrayList possibleMoves(Board board)
        {
            return generatePossibleMoves(new Direction[0] {}, 
                                            board, pos, pieceInfo);
        }
    }

    class Bishop : Piece
    {
        private int pos;
        private int pieceInfo;

        public override bool canMove(Board board)
        {
            throw new NotImplementedException();
        }

        public override ArrayList possibleMoves(Board board)
        {
            return generatePossibleMoves(new Direction[4] {Direction.NE, Direction.NW, Direction.SE, Direction.SW},
                                            board, pos, pieceInfo);
        }
    }

    class Rook : Piece
    {
        private int pos;
        private int pieceInfo;

        public override bool canMove(Board board)
        {
            throw new NotImplementedException();
        }

        public override ArrayList possibleMoves(Board board)
        {
            return generatePossibleMoves(new Direction[4] {Direction.N, Direction.E, Direction.W, Direction.S},
                                            board, pos, pieceInfo);
        }
    }

    class Queen : Piece
    {
        private int pos;
        private int pieceInfo;

        public override bool canMove(Board board)
        {
            throw new NotImplementedException();
        }

        public override ArrayList possibleMoves(Board board)
        {
            return generatePossibleMoves(new Direction[8] {Direction.N, Direction.NE, Direction.E, Direction.SE,
                                                           Direction.S, Direction.SW, Direction.W, Direction.NW},
                                            board, pos, pieceInfo);
        }
    }

    class King : Piece
    {
        private int pos;
        private int pieceInfo;

        public override bool canMove(Board board)
        {
            throw new NotImplementedException();
        }

        public override ArrayList possibleMoves(Board board)
        {
            return generatePossibleMoves(new Direction[2] {Direction.N, Direction.S},
                                            board, pos, pieceInfo);
        }
    }
}
