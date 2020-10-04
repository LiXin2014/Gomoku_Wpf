using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gomoku
{
    public class GameState
    {
        public static List<List<Cell>> Board;
        // Indicate if it's Black's turn
        public static bool BlackSTurn { get; set; }
        // Total steps till now
        public static int Steps { get; set; }
        public static bool GameEnded { get; set; }
        public static string Winner { get; set; }

        public static bool SomeoneHasWon(int row, int col)
        {
           return CheckVertical(row, col) || CheckHorizontal(row, col) || CheckDiagonalUpperLeftStart(row, col) || CheckDiagonalUpperRightStart(row, col);
        }

        private static bool CheckVertical(int row, int col) 
        {
            int match = 0;
            // go up
            int startRow = row - 1;
            while(startRow >= 0 && Board[startRow][col] == Board[row][col])
            {
                startRow--;
                match++;
            }
            // go down
            startRow = row + 1;
            while (startRow < Board.Count && Board[startRow][col] == Board[row][col])
            {
                startRow++;
                match++;
            }
            return match >= 4;
        }

        private static bool CheckHorizontal(int row, int col) 
        {
            int match = 0;
            // go left
            int startCol = col - 1;
            while (startCol >= 0 && Board[row][startCol] == Board[row][col])
            {
                startCol--;
                match++;
            }
            // go right
            startCol = col + 1;
            while (startCol < Board.Count && Board[startCol][col] == Board[row][col])
            {
                startCol++;
                match++;
            }
            return match >= 4;
        }
        private static bool CheckDiagonalUpperLeftStart(int row, int col)
        {
            int match = 0;
            // go up
            int startRow = row - 1;
            int startCol = col - 1;
            while (startRow >= 0 && startCol >= 0 && Board[startRow][startCol] == Board[row][col])
            {
                startRow--;
                startCol--;
                match++;
            }
            // go down
            startRow = row + 1;
            startCol = col + 1;
            while (startRow < Board.Count && startCol < Board.Count && Board[startRow][startCol] == Board[row][col])
            {
                startRow++;
                startCol++;
                match++;
            }
            return match >= 4;
        }

        private static bool CheckDiagonalUpperRightStart(int row, int col)
        {
            int match = 0;
            // go up
            int startRow = row - 1;
            int startCol = col + 1;
            while (startRow >= 0 && startCol < Board.Count && Board[startRow][startCol] == Board[row][col])
            {
                startRow--;
                startCol++;
                match++;
            }
            // go down
            startRow = row + 1;
            startCol = col - 1;
            while (startRow < Board.Count && startCol >= 0 && Board[startRow][startCol] == Board[row][col])
            {
                startRow++;
                startCol--;
                match++;
            }
            return match >= 4;
        }
    }
}
