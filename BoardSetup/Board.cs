using System;
using System.Reflection;
using System.Text;

namespace BoardSetup;
public class Board
{
    public String FEN;
    private readonly String defaultBackrank = "rnbqkbnr";

    internal int[] Square;

    /// <summary>
    ///     Constructor for a new board, initializes FEN
    /// </summary>
    /// <param name="isChess960"> Decides whether to use default or to generate 960 FEN </param>
    public Board(bool isChess960)
    {
        if (!isChess960)
        {
            FEN = $"{defaultBackrank}/pppppppp/8/8/8/8/PPPPPPPP/{defaultBackrank.ToUpper()} w KQkq - 0 1";
        } else
        {
            FEN = GenerateFEN();
        }
    }

    /// <summary>
    ///     Generate a new FEN to begin a game of Chess960.
    /// </summary>
    /// <returns> The generated FEN as a String, in the standardized FEN format </returns>
    public static String GenerateFEN() 
    {
        // Generate some RRNNBBKQ s.t:
        //      Rooks surround the king
        //      Bishops are on different-colored squares
        Random rand = new Random();
        StringBuilder sb = new StringBuilder("", 8);
        
        string possiblePieces = "RQBNK";
        string possiblePiecesNoBishop = "RQNK";

        string sbStr = "";

        while (true) {
            sbStr = sb.ToString();

            if (sbStr.Length == 8 || possiblePieces.Length == 0) break;

            if (sbStr.Count(c => c == 'Q') == 1) possiblePieces = possiblePieces.Replace( "Q", String.Empty );
    
            if (sbStr.Count(c => c == 'K') == 1) possiblePieces = possiblePieces.Replace( "K", String.Empty );

            if (sbStr.Count(c => c == 'N') == 2) possiblePieces = possiblePieces.Replace( "N", String.Empty );

            if (sbStr.Count(c => c == 'B') == 2) possiblePieces = possiblePieces.Replace( "B", String.Empty );

            if (sbStr.Count(c => c == 'R') == 2) possiblePieces = possiblePieces.Replace( "R", String.Empty );

            possiblePiecesNoBishop = possiblePieces.Replace("B", String.Empty);

            // Bishop condition
            //  Make sure that the bishops are not on the same color
            if (sbStr.Contains('B'))
            {
                int bIndex = sbStr.IndexOf('B');

                // If the second bishop would end up on the same colored square,
                //  add to the string using a version of possiblePieces that does not have 'B'
                if (bIndex % 2 == sbStr.Length % 2)
                {
                    sb = RookCondition(sb, rand, possiblePiecesNoBishop, sbStr);
                } // Otherwise, add to it normally, as the bishops would end up on different squares
                else
                {
                    sb = RookCondition(sb, rand, possiblePieces, sbStr);
                }
            } else
            {
                sb = RookCondition(sb, rand, possiblePieces, sbStr);
            }
        }

        sbStr = sb.ToString();

        return $"{sbStr.ToLower()}/pppppppp/8/8/8/8/PPPPPPPP/{sbStr} w KQkq - 0 1";
    }

    /// <summary>
    ///     Helper method to handle the logic of the Rook Condition of generating the FEN.
    /// (To allow both Queenside and Kingside castling, the King must be in the center of both rooks)
    /// </summary>
    /// <param name="sb"> The StringBuilder that will build the FEN </param>
    /// <param name="rand"> The Random instance that will generate random numbers </param>
    /// <param name="possiblePieces"> The String of possiblePieces that can be added to the FEN </param>
    /// <param name="sbStr"> The String that represents the FEN thus far </param>
    /// <returns></returns>
    public static StringBuilder RookCondition(StringBuilder sb, Random rand, String possiblePieces, String sbStr)
    {
        if (sbStr.Contains('R'))
        {
            if (sbStr.Contains('K'))
                sb.Append(possiblePieces.ElementAt(rand.Next(0, possiblePieces.Length)));
            else
                sb.Append(possiblePieces.ElementAt(rand.Next(1, possiblePieces.Length)));
        }
        else
        {
            sb.Append(possiblePieces.ElementAt(rand.Next(0, possiblePieces.Length - 1)));
        }

        return sb;
    }

    public static void Main() 
    {
        
    }
}
