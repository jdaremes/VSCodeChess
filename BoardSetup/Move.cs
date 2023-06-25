using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoardSetup
{
    internal class Move
    {
        // TODO: Put the basic piece logic in a helper method

        /* 
         * String squareStr = board.Square[p] + "";

           if (squareStr.ElementAt(0) == pInfoStr.ElementAt(0)) break;
           else if (squareStr.ElementAt(0) == '0')
           {
               legalMoves.Add(p); continue;
           }
           else if (squareStr.ElementAt(1) == '1')
           {
               // TODO: Capture King 1
               legalMoves.Add(p); break;
           }
           else
           {
               legalMoves.Add(p); break;
           }
         */

        public static void KnightMoveLogic(Board board, int pos, int pieceInfo, ArrayList legalMoves)
        {
            ArrayList x = new ArrayList(8) {  15,  17,  10,  6,
                                                 -15, -17, -10, -6 };

            String pInfoStr = pieceInfo + "";

            foreach (int deltaX in x)
            {
                if (pos + deltaX > 63 || pos + deltaX < 0)
                    continue;

                String str = board.Square[pos + deltaX] + "";

                // If the pieces have the same color, do not add this move
                //  (a piece cannot take another piece of the same color)
                if (str.ElementAt(0) == str.ElementAt(0))
                    continue;
                else if (str.ElementAt(1) == '1')
                {
                    // TODO: King Capture 1.0
                    legalMoves.Add(pInfoStr+ str);
                    continue;
                }
                else
                {
                    legalMoves.Add(pos + deltaX);
                    continue;
                }
            }


            if (pos < 16)
            {
                legalMoves.Remove(pos - 17);
                legalMoves.Remove(pos - 15);

                if (pos < 8)
                {
                    legalMoves.Remove(pos - 10);
                    legalMoves.Remove(pos - 6);
                }
            } else if (pos > 47)
            {
                legalMoves.Remove(pos + 17);
                legalMoves.Remove(pos + 15);

                if (pos > 55)
                {
                    legalMoves.Remove(pos + 10);
                    legalMoves.Remove(pos + 6);
                }
            }

            switch (pos % 8) { 
            
                case 0:
                    legalMoves.Remove(pos + 6);
                    legalMoves.Remove(pos - 10);
                    legalMoves.Remove(pos + 15);
                    legalMoves.Remove(pos - 17);
                    break;

                case 1:
                    legalMoves.Remove(pos + 6);
                    legalMoves.Remove(pos - 10);
                    break;

                case 7:
                    legalMoves.Remove(pos + 10);
                    legalMoves.Remove(pos - 6);

                    break;
                case 8:
                    legalMoves.Remove(pos + 10);
                    legalMoves.Remove(pos - 6);
                    legalMoves.Remove(pos + 17);
                    legalMoves.Remove(pos - 15);
                    break;

                default:
                    break;
            }
        }

        public static void PawnMoveLogic(Board board, int pos, int pieceInfo, ArrayList legalMoves)
        {
            // TODO: Implement en passant
            String pInfoStr = pieceInfo + "";

            // Logic for a white pawn
            if (pInfoStr.ElementAt(0) == '8')
            {
                // Can the pawn move forward? (one or two squares)
                if (board.Square[pos + 8] == 0)
                {
                    legalMoves.Add(pos + 8);

                    // If the pawn is still on the 7th rank, (and the square in front of it is empty)
                    // it can move up two squares
                    if (8 < pos && pos < 17 && board.Square[pos + 16] == 0)
                        legalMoves.Add(pos + 16);
                }

                // Can the pawn capture left or right?
                string squareStr = board.Square[pos + 7] + "";

                if (squareStr.ElementAt(0) == '8') legalMoves.Add(pos + 7);

                squareStr = board.Square[pos + 9] + "";

                if (squareStr.ElementAt(0) == '8') legalMoves.Add(pos + 9);
            } 

            // Logic for a black pawn
            else
            {
                // Can the pawn move forward? (one or two squares)
                if (board.Square[pos - 8] == 0)
                {
                    legalMoves.Add(pos - 8);

                    if (8 < pos && pos < 17 && board.Square[pos - 16] == 0)
                        legalMoves.Add(pos - 16);
                }

                // Can the pawn capture left or right?
                string squareStr = board.Square[pos - 7] + "";

                if (squareStr.ElementAt(0) == '7') legalMoves.Add(pos - 7);

                squareStr = board.Square[pos - 9] + "";

                if (squareStr.ElementAt(0) == '7') legalMoves.Add(pos - 9);
            }
        }

        public static void KingMoveLogic(Board board, int pos, int pieceInfo, ArrayList legalMoves)
        {
            string pieceInfoStr = pieceInfo + "";

            // x is an array of all possible moves on the board, in the following format:
            // [King's current position] + [Element in x] = [King's position after move is played]
            ArrayList x = new ArrayList(8) { 7,  8,  9,  1, 
                                            -7, -8, -9, -1};
            
            // If the king is on the top/bottom file and/or the leftmost/rightmost rank,
            // remove the moves that will go off the board
            if (pos % 8 == 0)
            {
                // Moves to the left
                x.Remove(-1);
                x.Remove(7);
                x.Remove(-9);
            }
            if (pos < 8)
            {
                // Moves below the king
                x.Remove(-9);
                x.Remove(-8);
                x.Remove(-7);
            }
            if (pos > 55)
            {
                // Moves above the king
                x.Remove(7);
                x.Remove(8);
                x.Remove(9);
            }
            if ((pos+1) % 8 == 0)
            {
                // Moves to the right
                x.Remove(9);
                x.Remove(1);
                x.Remove(-7);
            }

            foreach (int deltaX in x)
            {
                string squareStr = board.Square[pos + deltaX] + "";
                
                if (squareStr.ElementAt(0) == pieceInfoStr.ElementAt(0)) continue;
                else if (squareStr.ElementAt(1) == '1')
                {
                    // TODO: King capture 1.5
                    legalMoves.Add(pos + deltaX); continue;
                } else
                {
                    legalMoves.Add(pos + deltaX); continue;
                }
            }
        }

        public static void PossibleMovesByDirection(Direction[] arr, Board board, int pos, int pieceInfo, ArrayList legalMoves)
        {
            foreach (Direction d in arr)
            {
                int p = pos;

                String pInfoStr = pieceInfo + "";

                switch(d)
                {
                    case Direction.N:
                        while (p - 56 < 0)
                        {
                            p += 8;

                            String squareStr = board.Square[p] + "";

                            if (squareStr.ElementAt(0) == pInfoStr.ElementAt(0)) break;
                            else if (squareStr.ElementAt(0) == '0')
                            {
                                legalMoves.Add(p); continue;
                            }
                            else if (squareStr.ElementAt(1) == '1')
                            {
                                // TODO: Capture King 1
                                legalMoves.Add(p); break;
                            }
                            else
                            {
                                legalMoves.Add(p); break;
                            }
                        }
                        continue;

                    case Direction.NW:
                        while (p % 8 != 0 && p < 56)
                        {
                            p += 7;

                            if (p >= 63) break;

                            String squareStr = board.Square[p] + "";

                            if (squareStr.ElementAt(0) == pInfoStr.ElementAt(0)) break;
                            else if (squareStr.ElementAt(0) == '0')
                            {
                                legalMoves.Add(p); continue;
                            }
                            else if (squareStr.ElementAt(1) == '1')
                            {
                                // TODO: Capture King 2
                                legalMoves.Add(p); break;
                            }
                            else
                            {
                                legalMoves.Add(p); break;
                            }
                        }
                        continue;

                    case Direction.NE:
                        while ((p + 1) % 8 != 0 && p <= 56)
                        {
                            p += 9;

                            String squareStr = board.Square[p] + "";

                            if (squareStr.ElementAt(0) == pInfoStr.ElementAt(0)) break;
                            else if (squareStr.ElementAt(0) == '0')
                            {
                                legalMoves.Add(p); continue;
                            }
                            else if (squareStr.ElementAt(1) == '1')
                            {
                                // TODO: Capture King 3
                                legalMoves.Add(p); break;
                            }
                            else
                            {
                                legalMoves.Add(p); break;
                            }
                        }
                        continue;

                    case Direction.W:
                        while (p % 8 != 0)
                        {
                            p -= 1;

                            String squareStr = board.Square[p] + "";
                            
                            if (squareStr.ElementAt(0) == pInfoStr.ElementAt(0)) break;
                            else if (squareStr.ElementAt(0) == '0')
                            {
                                legalMoves.Add(p); continue;
                            }
                            else if (squareStr.ElementAt(1) == '1')
                            {
                                // TODO: Capture King 4
                                legalMoves.Add(p); break;
                            }
                            else
                            {
                                legalMoves.Add(p); break;
                            }
                        }
                        continue;
                    
                    case Direction.E:
                        while ((p+1) % 8 != 0)
                        {
                            p += 1;

                            String squareStr = board.Square[p] + "";

                            if (squareStr.ElementAt(0) == pInfoStr.ElementAt(0)) break;
                            else if (squareStr.ElementAt(0) == '0')
                            {
                                legalMoves.Add(p); continue;
                            }
                            else if (squareStr.ElementAt(1) == '1')
                            {
                                // TODO: Capture King 5
                                legalMoves.Add(p); break;
                            }
                            else
                            {
                                legalMoves.Add(p); break;
                            }
                        }
                        continue;

                    case Direction.SW:
                        while (p % 8 != 0 && p > 7) 
                        {
                            p -= 9;

                            String squareStr = board.Square[p] + "";

                            if (squareStr.ElementAt(0) == pInfoStr.ElementAt(0)) break;
                            else if (squareStr.ElementAt(0) == '0')
                            {
                                legalMoves.Add(p); continue;
                            }
                            else if (squareStr.ElementAt(1) == '1')
                            {
                                // TODO: Capture King 6
                                legalMoves.Add(p); break;
                            }
                            else
                            {
                                legalMoves.Add(p); break;
                            }
                        }
                        continue;

                    case Direction.SE:
                        while ((p + 1) % 8 != 0 && p > 7)
                        {
                            p -= 7;

                            String squareStr = board.Square[p] + "";

                            if (squareStr.ElementAt(0) == pInfoStr.ElementAt(0)) break;
                            else if (squareStr.ElementAt(0) == '0')
                            {
                                legalMoves.Add(p); continue;
                            }
                            else if (squareStr.ElementAt(1) == '1')
                            {
                                // TODO: Capture King 7
                                legalMoves.Add(p); break;
                            }
                            else
                            {
                                legalMoves.Add(p); break;
                            }
                        }
                        continue;

                    case Direction.S:
                        while (p > 7)
                        {
                            p -= 8;

                            String squareStr = board.Square[p] + "";

                            if (squareStr.ElementAt(0) == pInfoStr.ElementAt(0)) break;
                            else if (squareStr.ElementAt(0) == '0')
                            {
                                legalMoves.Add(p); continue;
                            }
                            else if (squareStr.ElementAt(1) == '1')
                            {
                                // TODO: Capture King 8
                                legalMoves.Add(p); break;
                            }
                            else
                            {
                                legalMoves.Add(p); break;
                            }
                        }
                        continue;
                }
            }
        }



    }
}
