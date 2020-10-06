using System.Collections.Generic;
using MessagePack;

namespace Gomoku
{
    [MessagePackObject]
    public sealed class GameState : ObservableObject
    {
        [SerializationConstructor]
        private GameState() 
        {
            StartNewGameCommand = new RelayCommand(StartNewGame);
        }
        public static GameState Instance { get; } = new GameState();

        #region properties
        [Key(0)]
        public List<List<Cell>> Board { get; set; }

        [IgnoreMember]
        private bool blackSTurn;
        // Indicate if it's Black's turn
        [Key(1)]
        public bool BlackSTurn
        {
            get { return blackSTurn; }
            set
            {
                blackSTurn = value;
                OnPropertyChanged(nameof(BlackSTurn));
            }
        }
        // Total steps till now
        [Key(2)]
        public int Steps { get; set; }

        [IgnoreMember]
        private bool gameEnded;
        [Key(3)]
        public bool GameEnded
        {
            get { return gameEnded; }
            set
            {
                gameEnded = value;
                OnPropertyChanged(nameof(GameEnded));
            }
        }

        [IgnoreMember]
        private string winner;
        [Key(4)]
        public string Winner
        {
            get { return winner; }
            set
            {
                winner = value;
                OnPropertyChanged(nameof(Winner));
            }
        }
        #endregion

        #region check game state
        public bool SomeoneHasWon(int row, int col)
        {
            return CheckVertical(row, col) || CheckHorizontal(row, col) || CheckDiagonalUpperLeftStart(row, col) || CheckDiagonalUpperRightStart(row, col);
        }

        private bool CheckVertical(int row, int col)
        {
            int match = 0;
            // go up
            int startRow = row - 1;
            while (startRow >= 0 && Board[startRow][col] == Board[row][col])
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

        private bool CheckHorizontal(int row, int col)
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
        private bool CheckDiagonalUpperLeftStart(int row, int col)
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

        private bool CheckDiagonalUpperRightStart(int row, int col)
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
        #endregion

        #region Start New Game Command
        [IgnoreMember]
        public RelayCommand StartNewGameCommand { get; set; }

        public void StartNewGame(object parameter)
        {
            // set game state
            BlackSTurn = true;
            Steps = 0;
            GameEnded = false;

            // reset the board
            int rows = Board.Count;
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < rows; j++)
                {
                    Board[i][j].Content = "";
                    // Raise can execute changed event, CanBeClicked method will be called on each Cell to determin the IsEnabled property for the matching button.
                    // That's why the GameEnded state must be reset before this line, otherwise CanBeClicked will give wrong result.
                    Board[i][j].ButtonCommand.RaiseCanExecuteChanged();
                }
            }
        }
        #endregion

        public void LoadSavedGame(GameState savedGame)
        {
            Board = savedGame.Board;
            BlackSTurn = savedGame.BlackSTurn;
            Steps = savedGame.Steps;
            GameEnded = savedGame.GameEnded;
        }
    }
}

/*
 * Note: How to bind to static properties:
 * https://docs.microsoft.com/en-us/dotnet/desktop/wpf/getting-started/whats-new?redirectedfrom=MSDN&view=netframeworkdesktop-4.8#static_properties
 * https://stackoverflow.com/questions/20319857/data-binding-with-static-properties-in-wpf/20322241
 */
