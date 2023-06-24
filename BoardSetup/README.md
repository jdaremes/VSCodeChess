```
Author: jdaremes
Start Date: 21-Jun-2023
Repo: https://github.com/jdaremes/VSCodeChess
Project: BoardSetup
Commit Date: 24-Jun-2023
Copyright: James Semerad
```
# BoardSetup

NOTE: A 'file' refers to a column on the chessboard, and a 'rank' refers to a row
Generally, files are denoted by 'a-h', and ranks by '1-8'
Rather than convert between letters and numbers to describe a piece's position, this project represents a board using an array of 64 integers, each of which describes a specific piece according to readonly variables in
the Piece class. This representation is 'Square-centric,' and allows the program to verify that certain moves are legal or illegal using the state of the entire board.

## Overview of Current Functionality

The BoardSetup project includes 3 files: 

	-Board.cs, which generates the FEN (default for normal Chess, generates one for Chess960), and sets up the board, including 
	pieces in its array representation

	-Move.cs, an internal class that holds every method for calculating a piece's moves. 
	NOTE: This class currently only calculates moves that are 'pseudolegal', meaning that the opponent's King can be potentially 
	be captured. In the future, this class will also determine whether a move gives the opponent a move where the player's King can
	be captured, invalidating the original move.

	-Piece.cs, an abstract class which holds the information of what type/color a piece is, where it is on the board, and includes 
	abstract methods for calculating that piece's possible/legal moves.

	-Pieces.cs, which contains each piece's override of the methods in the Piece.cs class.

### Works Cited

	1. Notes on 'Square-Centric' Board Representation -- https://www.chessprogramming.org/Board_Representation#Square_Centric