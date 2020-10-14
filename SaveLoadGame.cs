using Newtonsoft.Json;
using System.IO;

namespace Gomoku
{
    public class SaveLoadGame
    {
        public RelayCommand SaveGameCommand { get; set; } = new RelayCommand(SaveGame);
        public RelayCommand OpenGameCommand { get; set; } = new RelayCommand(OpenGame);

        public static void SaveGame(object parameter)
        {

            var gameState = GameState.Instance;
            string path = Directory.GetCurrentDirectory() + "\\GameState.txt";

            // deserialize JSON directly from a file
            using (StreamWriter file = File.CreateText(path))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(file, gameState);
            }
        }

        public static void OpenGame(object parameter)
        {
            string path = Directory.GetCurrentDirectory() + "\\GameState.txt";
            if (!File.Exists(path))
            {
                throw new FileNotFoundException();
            }

            GameState gameState = JsonConvert.DeserializeObject<GameState>(File.ReadAllText(path));
            GameState.Instance.LoadSavedGame(gameState);
        }
    }
}
