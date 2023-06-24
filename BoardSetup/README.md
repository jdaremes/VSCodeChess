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

The VSCodeChess solution currently contains the Board.cs class, which initializes the board based on whether or not the user has selected Chess960 (if they have, it will randomly generate an FEN code to draw the board).

NOTE: Further explanation of Board code can be found in the BoardSetup project README

### Works Cited

	1. Notes on 'Square-Centric' Board Representation -- https://www.chessprogramming.org/Board_Representation#Square_Centric