using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gomoku
{
    public class GameModel
    {
        public GameBoard GameBoard { get; set; }
        public GameState GameState { get; set; }
        public SaveLoadGame SaveGame { get; set; }

        public GameModel()
        {
            GameBoard = new GameBoard(GameState.Instance);
            GameState = GameState.Instance;
            SaveGame = new SaveLoadGame();
        }
    }
}
