using System;
using System.Collections;
using System.Diagnostics;
using System.IO;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.ExceptionServices;
using BoardSetup;

namespace BoardSetupTests
{
    [TestClass]
    public class FENgenerationTests
    {
        // List of all possible FENs
        ArrayList FENarr;

        /// <summary>
        ///     Add all FENs to a single ArrayList to iterate over later
        /// </summary>
        [TestInitialize]
        public void Initialize()
        {
            FENarr = new ArrayList(960);

            using (StreamReader sr = new StreamReader(Directory.GetCurrentDirectory().Replace("bin\\Debug\\net7.0", "960table.txt")))
            {
                string line;

                while ((line = sr.ReadLine()) != null)
                {
                    string line1 = line.Substring(36, 89 - 36).Replace("w - ", "w KQkq ");

                    FENarr.Add(line1);
                }
            }
        }

        /// <summary>
        ///     Test that every FEN gets generated by this algorithm
        /// 
        ///     Run the generateFEN() method 7150 times, rounding up from the solution to 
        /// the expected number of times required to generate every number from 1 to 960.
        /// (Calculated from the Coupon Collector's Problem, see project README for more details)
        /// 
        /// </summary>
        [TestMethod]
        public void FENgenerationTest()
        {
            for (int i = 0; i < 10000; i++)
            {
                string fen = BoardSetup.Board.GenerateFEN();

                if (!FENarr.Contains(fen))
                    throw new Exception("Error: Algorithm generated a false FEN: " + fen);
            }
        }

        /// <summary>
        ///     Helper method to check the formatting of the strings that are generated inside
        /// of the FENarr array.
        /// 
        /// </summary>
        [TestMethod]
        public void FENarrTest()
        {
            for (int i = 0; i < FENarr.Count; i++)
                Debug.WriteLine(FENarr[i]);
        }

        /// <summary>
        ///     Method to ascertain whether every FEN in the list is being generated by the program.
        /// Note: Sometimes, this test generates a list for which the count is less than the other.
        /// At this point, I'm not sure if this can simply be explained by variance in the algorithm,
        /// or if some FENs are more likely to be generated than others.
        /// 
        /// </summary>
        [TestMethod]
        public void BoardSetupTest()
        {
            // Create two different sorted sets (no duplicates)
            // One will contain all elements in FENarr, the other  
            SortedSet<string> FENset = new SortedSet<string>() { };
            SortedSet<string> FENset2 = new SortedSet<string>() { };

            // Create the SortedSet analagous to FENarr (in the same way as FENarr was created)
            using (StreamReader sr = new StreamReader(Directory.GetCurrentDirectory().Replace("bin\\Debug\\net7.0", "960table.txt")))
            {
                string line;

                while ((line = sr.ReadLine()) != null)
                {
                    string line1 = line.Substring(36, 89 - 36).Replace("w - ", "w KQkq ");
                    FENset.Add(line1);
                }
            }

            // Remove the first element as it's not an FEN
            FENset.Remove(FENset.ElementAt(0));

            // Randomly generate 40000 FENs, add them to FENset2
            for (int i = 0; i < 40000; i++)
            {
                string fen = BoardSetup.Board.GenerateFEN();
                FENset2.Add(fen);
            }

            // Compare the counts and the arrays
            Debug.WriteLine(FENset.Count);
            Debug.WriteLine(FENset2.Count);

            Debug.WriteLine(Enumerable.SequenceEqual(FENset, FENset2));

            // If one's count is higher than the other, throw an exception
            if (FENset.Count > FENset2.Count)
                throw new Exception("Error: Not every value in FENset is contained in FENset2");
            
            // Log each element in both arrays using Debug
            for (int i = 0; i < 960; i++)
            {
                Debug.Write("\n");
                int j = i + 1;
                Debug.WriteLine("Element " + j + ":");

                Debug.WriteLine(FENset.ElementAt(i));
                Debug.WriteLine(FENset2.ElementAt(i));
            }
        }

        /// <summary>
        ///     Creates a dictionary of each FEN and its frequency over 400000 calls to GenerateFEN()
        /// NOTE: This method currently indicates that some FENs are more likely than others.
        /// 
        /// </summary>
        [TestMethod]
        public void DictionaryTest()
        {
            // Create a new dictionary to hold all possible FENs and their frequencies
            Dictionary<string, int> fenFrequencies = new Dictionary<string, int>();

            foreach (string fen in FENarr)
                fenFrequencies.Add(fen, 0);

            // Run GenerateFEN() 400000 times, add each result to the Dictionary
            for (int i = 0; i < 400000; i++)
            {
                string fen = BoardSetup.Board.GenerateFEN();
                fenFrequencies[fen]++;
            }

            // Print every element in the dictionary
            foreach (KeyValuePair<string, int> keyValuePair in fenFrequencies) 
                Debug.WriteLine($"FEN: {keyValuePair.Key} Count: {keyValuePair.Value}");
        }
    }
}
