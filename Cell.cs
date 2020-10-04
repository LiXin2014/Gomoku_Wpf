using System;
using System.Windows;

namespace Gomoku
{
    public class Cell : ObservableObject
    {
        // Content of the cell
        private string content;
        // Position of the cell in board
        public (int, int) Position { get; set; }
        public RelayCommand ButtonCommand { get; set; }

        /// <summary>
        /// Gets or sets content of a cell.
        /// </summary>
        public string Content { 
            get { return content; } 
            set { 
                this.content = value;
                OnPropertyChanged(nameof(Content)); 
            } 
        }

        public Cell(int row, int col)
        {
            this.ButtonCommand = new RelayCommand(OnClicked, CanBeClicked);
            this.Position = (row, col);
        }

        /// <summary>
        /// Called when this cell is clicked
        /// </summary>
        /// <param name="parameter">This is content passed from CommandParameter in xaml</param>
        public void OnClicked(object parameter)
        {
            if (GameState.BlackSTurn)
            {
                this.Content = "●";
            } else
            {
                this.Content = "○";
            }

            if(GameState.Steps >= 8 && GameState.SomeoneHasWon(Position.Item1, Position.Item2))
            {
                GameState.GameEnded = true;
                GameState.Winner = GameState.BlackSTurn ? "Black" : "White";
                MessageBox.Show($"{GameState.Winner} has won!");
                return;
            }

            GameState.Steps++;
            GameState.BlackSTurn = !GameState.BlackSTurn;
        }

        /// <summary>
        /// Can this cell be clicked.
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns>True if it can be clicked, false otherwise</returns>
        public bool CanBeClicked(object parameter)
        {
            return Content == null || String.Equals(Content, String.Empty) && !GameState.GameEnded;
        }

        public override bool Equals(Object obj)
        {
            //Check for null and compare run-time types.
            if ((obj == null) || !this.GetType().Equals(obj.GetType()))
            {
                return false;
            }
            else
            {
                Cell c = (Cell)obj;
                return (Content == c.Content);
            }
        }

        public static bool operator ==(Cell x, Cell y)
        {
            return x.Equals(y);
        }

        public static bool operator !=(Cell x, Cell y)
        {
            return !x.Equals(y);
        }
    }
}
