using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;

namespace BoardSetup;
public class Board
{
    public string FEN;
    public string trimmedFEN;

    private readonly string defaultBackrank = "rnbqkbnr";


    Regex lastPartOfFEN = new Regex(@" (w{1}|b{1}) ([A-Z]{0,2}[kq]{0,2}|-{1}) ([a-z]{1}[1-8]{1}|-) [0-9]+ [0-9]+");

    internal int[] Square;

    public readonly Dictionary<char, int> charToPiece = new Dictionary<char, int>()
    { {'R', ConcatenateChars(Piece.White,Piece.Rook)}, 
      {'N', ConcatenateChars(Piece.White,Piece.Knight)},
      {'B', ConcatenateChars(Piece.White,Piece.Bishop)},
      {'Q', ConcatenateChars(Piece.White,Piece.Queen)},
      {'K', ConcatenateChars(Piece.White,Piece.King)}, 
      {'P', ConcatenateChars(Piece.White,Piece.Pawn)},
      {'r', ConcatenateChars(Piece.Black,Piece.Rook)}, 
      {'n', ConcatenateChars(Piece.Black,Piece.Knight)},
      {'b', ConcatenateChars(Piece.Black, Piece.Bishop)}, 
      {'q', ConcatenateChars(Piece.Black, Piece.Queen)}, 
      {'k', ConcatenateChars(Piece.Black, Piece.King)}, 
      {'p', ConcatenateChars(Piece.Black, Piece.Pawn)} };

    /// <summary>
    /// Helper method to convert chars to ints 
    /// (Credit to Rex M, StackOverflow)
    /// 
    /// https://stackoverflow.com/questions/1014292/concatenate-integers-in-c-sharp
    /// </summary>
    /// <param name="a"> the first integer to convert </param>
    /// <param name="b"> the second integer "    " </param>
    /// <returns> the two integers concatenated (e.g, for integers 0 and 1, returns 01) </returns>
    public static int ConcatenateChars(int a, int b)
    {
        return int.Parse(a.ToString() + b.ToString());
    }


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
        new Board(FEN);
    }

    /// <summary>
    ///     Constructor for a board via string of FEN. (used by other constructor)
    /// </summary>
    /// <param name="FEN"></param>
    public Board(string FEN)
    {
        Square = new int[64];

        string trimmedFEN = lastPartOfFEN.Replace(FEN, String.Empty);
        Debug.WriteLine(trimmedFEN);


        int i = 0;
        foreach (char c in trimmedFEN)
        {
            if (c == '/')
                continue;
            else if (Char.IsDigit(c))
            {
                int j = c - '0';
                for (int k = 0; k < j; k++)
                {
                    Square[i] = Piece.Empty;
                    i++;
                }
                continue;
            } else if (charToPiece.ContainsKey(c))
                Square[i] = charToPiece[c];
            i++;
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
            {
                // Handling edge case:
                //   When the bishop can't be placed in the same index as another (Bishop condition),
                //   but it's the only possible piece left
                if (possiblePieces.Length == 0)
                {
                    // To solve this edge case, this code simply swaps item at that index
                    // with a bishop
                    char c = sb.ToString().ElementAt(sb.Length-1);

                    sb.Remove(sb.Length-1, 1);
                    sb.Append('B');
                    sb.Append(c);

                    return sb;
                }
                sb.Append(possiblePieces.ElementAt(rand.Next(0, possiblePieces.Length)));
            }
                
            else
                sb.Append(possiblePieces.ElementAt(rand.Next(1, possiblePieces.Length)));
        }
        else
        {
            sb.Append(possiblePieces.ElementAt(rand.Next(0, possiblePieces.Length - 1)));
        }

        return sb;
    }

    /// <summary>
    ///     Returns the Square array as a string
    /// </summary>
    /// <returns></returns>
    public override String ToString()
    {
        // TODO: Implement this function
        return "";
    }

}
