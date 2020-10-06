using System.Collections.Generic;

namespace Gomoku
{
    public class GameBoard : ObservableObject
    {
        private readonly GameState _innerState;

        /// <summary>
        /// Gets board of the game
        /// </summary>
        public List<List<Cell>> Board => _innerState.Board;

        /// <summary>
        /// Initialize a GameBoard instance
        /// </summary>
        /// <param name="innerState">Game state</param>
        public GameBoard(GameState innerState)
        {
            _innerState = innerState ?? throw new System.ArgumentNullException(nameof(innerState));
            InitializeGame();
            // subscribe to game state's property changed event
            _innerState.PropertyChanged += _innerState_PropertyChanged;
        }

        /// <summary>
        /// Notify UI the board is updated if the game state's board is updated.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _innerState_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(_innerState.Board))
            {
                OnPropertyChanged(nameof(Board));
            }
        }

        // Initialize the game
        private void InitializeGame()
        {
            _innerState.BlackSTurn = true;
            _innerState.Steps = 0;
            _innerState.GameEnded = false;
        }
    }
}
