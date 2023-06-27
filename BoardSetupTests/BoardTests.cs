using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoardSetupTests
{
    [TestClass]
    public class BoardTests
    {
        /// <summary>
        /// Tests whether or not the regular expression trims the FEN correctly to draw
        /// </summary>
        [TestMethod]
        public void DefaultConstructor()
        {
            BoardSetup.Board board = new BoardSetup.Board(false);

            board.ToString();
        }


    }
}
