using System.Collections.Generic;

namespace Gomoku
{
    public class GameBoard : ObservableObject
    {
        private List<List<Cell>> board;

        /// <summary>
        /// Gets or sets board of the game
        /// </summary>
        public List<List<Cell>> Board { 
            get { return this.board; } 
            set { 
                this.board = value;
                OnPropertyChanged(nameof(Board));
            } 
        }

        /// <summary>
        /// Initialize a GameBoard instance
        /// </summary>
        /// <param name="rows">Number of rows for the game board</param>
        public GameBoard(int rows)
        {
            Board = new List<List<Cell>>();
            for (int i = 0; i < rows; i++)
            {
                List<Cell> row = new List<Cell>();
                Board.Add(row);
                for (int j = 0; j < rows; j++)
                {
                    row.Add(new Cell());
                }
            }
        }
    }
}
