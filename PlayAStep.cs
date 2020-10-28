using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gomoku
{
    public class PlayAStep
    {
        public int Row { get; private set; }
        public int Col { get; private set; }

        public RelayCommand PlayAStepCommand { get; private set; }

        public PlayAStep(int row, int col)
        {
            Row = row;
            Col = col;
            PlayAStepCommand = new RelayCommand(OnClicked, CanBeClicked);
        }
       
        /// <summary>
        /// Called when this cell is clicked
        /// </summary>
        /// <param name="parameter">This is content passed from CommandParameter in xaml</param>
        public void OnClicked(object parameter)
        {
            if (GameState.Instance.BlackSTurn)
            {
                GameState.Instance.Board[Row][Col].Content = "●";
            }
            else
            {
                GameState.Instance.Board[Row][Col].Content = "○";
            }

            StepList.Instance.AddAStep(new Step() { Player = GameState.Instance.BlackSTurn ? "Black" : "White", Row = Row, Col = Col });
            GameState.Instance.UndoCommand.RaiseCanExecuteChanged();

            if (GameState.Instance.Steps >= 8 && GameState.Instance.SomeoneHasWon(this.Row, this.Col))
            {
                GameState.Instance.GameEnded = true;
                GameState.Instance.Winner = GameState.Instance.BlackSTurn ? "Black" : "White";
            }

            GameState.Instance.Steps++;
            GameState.Instance.BlackSTurn = !GameState.Instance.BlackSTurn;
        }

        /// <summary>
        /// Can this cell be clicked.
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns>True if it can be clicked, false otherwise</returns>
        public bool CanBeClicked(object parameter)
        {
            string content = GameState.Instance.Board[Row][Col].Content; 
            return !GameState.Instance.GameEnded && (content == null || String.Equals(content, String.Empty));
        }
    }
}
