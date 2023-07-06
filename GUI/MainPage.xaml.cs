using BoardSetup;
using Microsoft.Maui.Graphics;
using System.Security.Cryptography;

namespace GUI
{
    public partial class MainPage : ContentPage
    {
        int count = 0;
        private string files = "abcdefgh";

        private Dictionary<string, Frame> frames = new Dictionary<string, Frame>();
        private Dictionary<int, Color> squareColors = new Dictionary<int, Color>();

        private readonly Dictionary<int, string> squares = new Dictionary<int, string>()
        {
            {0, "a1"},{1, "b1"},{2, "c1"},{3, "d1"},{4, "e1"},{5, "f1"},{6, "g1"},{7, "h1"},
            {8, "a2"},{9, "b2"},{10,"c2"},{11,"d2"},{12,"e2"},{13,"f2"},{14,"g2"},{15,"h2"},
            {16,"a3"},{17,"b3"},{18,"c3"},{19,"d3"},{20,"e3"},{21,"f3"},{22,"g3"},{23,"h3"},
            {24,"a4"},{25,"b4"},{26,"c4"},{27,"d4"},{28,"e4"},{29,"f4"},{30,"g4"},{31,"h4"},
            {32,"a5"},{33,"b5"},{34,"c5"},{35,"d5"},{36,"e5"},{37,"f5"},{38,"g5"},{39,"h5"},
            {40,"a6"},{41,"b6"},{42,"c6"},{43,"d6"},{44,"e6"},{45,"f6"},{46,"g6"},{47,"h6"},
            {48,"a7"},{49,"b7"},{50,"c7"},{51,"d7"},{52,"e7"},{53,"f7"},{54,"g7"},{55,"h7"},
            {56,"a8"},{57,"b8"},{58,"c8"},{59,"d8"},{60,"e8"},{61,"f8"},{62,"g8"},{63,"h8"},
        };

        public MainPage()
        {
            InitializeComponent();
        }

        private void OnCounterClicked(object sender, EventArgs e)
        {
            count++;

            if (count == 1)
                ChessBtn.Text = $"Clicked {count} time";
            else
                ChessBtn.Text = $"Clicked {count} times";

            SemanticScreenReader.Announce(ChessBtn.Text);
        }

        private void OnChessClicked(object sender, EventArgs e)
        {
            Board b = new Board(false);

            squareColors[0] = Colors.AntiqueWhite;
            squareColors[1] = Colors.RosyBrown;

            WelcomeScreen.IsVisible= false;
            ChessScreen.IsVisible= true;

            InitializeBoard(Grid, frames);
        }

        private void OnChess960Clicked(object sender, EventArgs e)
        {
            Board b = new Board(true);

            squareColors[0] = Colors.AntiqueWhite;
            squareColors[1] = Colors.RosyBrown;

            WelcomeScreen.IsVisible = false;
            ChessScreen.IsVisible = true;

            InitializeBoard(Grid, frames);
        }


        private void InitializeBoard(VerticalStackLayout grid, Dictionary<string, Frame> frames)
        {
            int i = 0;

            for (int rank = 8; rank > 0; rank--)
            {
                var horiz = new HorizontalStackLayout()
                {
                    VerticalOptions = LayoutOptions.Center,
                    IsClippedToBounds = false,
                };

                foreach (char file in files)
                {
                    var frame = new Frame
                    {
                        BackgroundColor = squareColors[i % 2],
                        StyleId = $"{file}{rank}",
                        HasShadow = false,
                        IsClippedToBounds = false,
                        BorderColor = squareColors[i % 2]
                    };
                    i++;
                    horiz.Add(frame);
                    frames[$"{file}{rank}"] = frame;
                }

                Grid.Add(horiz);
                i++;
            }
        }

    }
}