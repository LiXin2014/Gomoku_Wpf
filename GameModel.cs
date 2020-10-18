namespace Gomoku
{
    public class GameModel
    {
        public GameBoard GameBoard { get; }
        public GameState GameState { get; }
        public SaveLoadGame SaveGame { get; }

        public GameModel()
        {
            GameBoard = new GameBoard(GameState.Instance);
            GameState = GameState.Instance;
            SaveGame = new SaveLoadGame();
        }
    }
}
