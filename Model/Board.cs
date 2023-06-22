using System.Reflection;
using System.Random;
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
        char[] possPieces = "";
        string possiblePieces = "RQBNK";
        // "RBNK"

        while (true) {
            if (sb.Contains('Q')) {
                count--;
                possiblePieces.Replace( 'Q','' );
            }
                
            if (sb.Contains('K')) {
                count--;
                possiblePieces.Remove( 'K','' );
            }

            if (sb.count('N') == 2) {
                count--;
                possiblePieces.Replace( 'N','' );
            }

            if (sb.count('B') == 2) {
                count--;
                possiblePieces.Replace( 'B','' );
            }

            if (sb.count('R') == 2) {
                count--;
                possiblePieces.Replace( 'R','' );
            }

            if ( possiblePieces.Length == 0 )
                break;

            // Rook condition
            //  See if we've placed a rook already

            if (!sb.Contains('R')) 
            {
                char square = possiblePieces.charAt( rand.NextInt( 0, count-1 ));
                sb.Add(square);
                continue;
            } else 
            {
                if (sb.Contains('K')) 
                {
                    char square = possiblePieces.charAt( rand.NextInt( 0, count-1 ));
                    sb.Add(square);
                    continue;
                } else 
                {
                    char square = possiblePieces.charAt( rand.NextInt( 1, count ));
                    sb.Add(square);
                    continue;
                }
            }

            



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



        return "";
    }

    public static void Main() 
    {
        
    }
}
