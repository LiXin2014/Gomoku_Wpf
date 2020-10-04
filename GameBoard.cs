using System.Collections.Generic;

namespace Gomoku
{
    public class GameBoard : ObservableObject
    {
        private List<List<Cell>> board;
        // Number of rows in the board
        private int rows;

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
            this.rows = rows;
            InitializeGame();
        }

        // Initialize the game
        public void InitializeGame()
        {
            // set the board
            Board = new List<List<Cell>>();
            for (int i = 0; i < this.rows; i++)
            {
                List<Cell> row = new List<Cell>();
                Board.Add(row);
                for (int j = 0; j < this.rows; j++)
                {
                    row.Add(new Cell(i, j));
                }
            }

            // set game state
            GameState.BlackSTurn = true;
            GameState.Steps = 0;
            GameState.GameEnded = false;
            GameState.Board = Board;
        }
    }
}
