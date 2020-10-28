using Microsoft.Win32;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;
using System.Collections.Generic;

namespace Gomoku
{
    public class SaveLoadGame
    {
        public RelayCommand SaveGameCommand { get; set; } = new RelayCommand(SaveGame);
        public RelayCommand OpenGameCommand { get; set; } = new RelayCommand(OpenGame);

        public static void SaveGame(object parameter)
        {
            var gameState = GameState.Instance;
            var steps = StepList.Instance.Steps;

            SaveFileDialog saveFileDialog = new SaveFileDialog();
            if (saveFileDialog.ShowDialog() == true)
            {

                JsonSerializer serializer = new JsonSerializer();
                using (StreamWriter file = new StreamWriter(saveFileDialog.FileName))
                {
                    serializer.Serialize(file, new { gameState = gameState, steps = steps});
                }
                // above using code block was written as below line at first, it would be wrong because 
                // the streamwriter is not flushed (needed for content to be actually written) and disposed.
                //serializer.Serialize(new StreamWriter(saveFileDialog.FileName), gameState);
            }
        }

        public static void OpenGame(object parameter)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            string text = "";
            if (openFileDialog.ShowDialog() == true)
                text = File.ReadAllText(openFileDialog.FileName);

            JObject game = JsonConvert.DeserializeObject(text) as JObject;
            GameState gameState = game["gameState"].ToObject<GameState>();

            var steps = game["steps"].ToObject<List<Step>>();
            StepList.Instance.Steps = steps;
            GameState.Instance.LoadSavedGame(gameState);
        }
    }
}
