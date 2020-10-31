using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gomoku
{
    public class UndoAStep
    {
        public RelayCommand UndoAStepCommand { get; private set; }

        public UndoAStep()
        {
            UndoAStepCommand = new RelayCommand(OnUndoClicked, CanBeUndone);
        }

        /// <summary>
        /// Called when Undo button is clicked
        /// </summary>
        /// <param name="parameter">This is content passed from CommandParameter in xaml</param>
        public void OnUndoClicked(object parameter)
        {
            if (GameState.Instance.GameEnded)
            {
                GameState.Instance.GameEnded = false;
                GameState.Instance.Winner = "";
                GameState.Instance.ResetBoard();
            }

            Step lastStep = StepList.Instance.GetLastStep();
            GameState.Instance.Board[lastStep.Row][lastStep.Col].Content = "";
            GameState.Instance.Board[lastStep.Row][lastStep.Col].ButtonCommand.RaiseCanExecuteChanged();

            GameState.Instance.Steps--;
            GameState.Instance.BlackSTurn = !GameState.Instance.BlackSTurn;

            StepList.Instance.UndoAStep();
        }

        /// <summary>
        /// Can the undo button be clicked
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns>True if it can be clicked, false otherwise</returns>
        public bool CanBeUndone(object parameter)
        {
            return StepList.Instance.Steps.Count != 0;
        }
    }
}
