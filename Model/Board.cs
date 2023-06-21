using System.Reflection;

namespace BoardSetup;
public class Board
{
    private static String? FEN;
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
        return "";
    }

    public static void Main() 
    {

    }
}
