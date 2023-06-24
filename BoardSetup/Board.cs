using System.Reflection;
using System.Text;

namespace BoardSetup;
public class Board
{
    private String FEN;

    private readonly String defaultFEN = "rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1";



    public Board(bool isChess960)
    {
        if (!isChess960)
        {
            FEN = defaultFEN;
        } else
        {
            FEN = GenerateFEN();
        }
    }

    public static String GenerateFEN() 
    {
        // Generate RRNNBBKQ s.t:
        //      Rooks surround the king
        //      Bishops are on different-colored squares
        Random rand = new Random();
        StringBuilder sb = new StringBuilder("", 8);

        int count = 5;
        string possiblePieces = "RQBNK";
        bool bishopOnLightSquare = false; 
        // "RBNK"

        while (true) {
            string sbStr = sb.ToString();
            Console.WriteLine(count);
            Console.WriteLine(sbStr);

            if (sbStr.Length == 8 || possiblePieces.Length == 0) 
                break;

            if (sbStr.Count(c => c == 'Q') == 1) {
                count--;
                possiblePieces = possiblePieces.Replace( "Q", String.Empty );
            }
                
            if (sbStr.Count(c => c == 'K') == 1) {
                count--;
                possiblePieces = possiblePieces.Replace( "K", String.Empty );
            }

            if (sbStr.Count(c => c == 'N') == 2) {
                count--;
                possiblePieces = possiblePieces.Replace( "N", String.Empty );
            }

            if (sbStr.Count(c => c == 'B') == 2) {
                count--;
                possiblePieces = possiblePieces.Replace( "B", String.Empty );
            }

            if (sbStr.Count(c => c == 'R') == 2) {
                count--;
                possiblePieces = possiblePieces.Replace( "R", String.Empty);
            }

            // Rook condition
            //  See if we've placed a rook already
            if (count == 0 || count == -1)
            {
                break;
            }
            sb.Append( possiblePieces.ElementAt( rand.Next(0, count)) );

            
        }


        // Random generation
        
        // K - 1

        // 1st Square - Can't be King, random number from 0 to 4
        // 2nd Square - Can't be king if rook hasn't been palced. 
        //              Can't be rook if a rook has been placed
        // 3rd Square - Can't be king if rook hasn't been placed,
        //              Can't be bishop if a bishop has been placed
        // 4th Square - Can't be king if rook hasn't been placed.


        //      Rooks: Designate one as the left, one as the right
        //              The left rook can never be further than the 6th square (5th index)
        //              The right rook "    "    "   "      "     " 3rd square (2nd index)
        //         i.e, RKR-----  -----RKR, where the dashes indicate the rest of the pieces

        //              Left Rook (0-5)
        //              Right Rook (2-7)

        //      Bishops: Designate one as dark-squared, one as light-squared
        //               Each one can only be placed on 1/2 of the potential squares
        //              One can only be even indices, one only odd (add 1 to index first)



        return sb.ToString();
    }

    public static void Main() 
    {
        
    }
}
