using MessagePack;
using MessagePack.Resolvers;
using System;
using System.IO;
using System.Net.NetworkInformation;

namespace Gomoku
{
    public class SaveLoadGame
    {
        public RelayCommand SaveGameCommand { get; set; } = new RelayCommand(SaveGame);
        public RelayCommand OpenGameCommand { get; set; } = new RelayCommand(OpenGame);

        public static void SaveGame(object parameter)
        {

            var gameState = GameState.Instance;
            var bytes = MessagePackSerializer.Serialize(gameState, ContractlessStandardResolverAllowPrivate.Options);

            string path = Directory.GetCurrentDirectory() + "\\GameState.txt";

            using (FileStream fs = new FileStream(path, FileMode.OpenOrCreate, FileAccess.Write))
            {
                fs.Write(bytes, 0, bytes.Length);
            }
        }

        public static void OpenGame(object parameter)
        {
            string path = Directory.GetCurrentDirectory() + "\\GameState.txt";
            if (!File.Exists(path))
            {
                throw new FileNotFoundException();
            }

            // Open the file to read from.
            using (FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read))
            {
                byte[] bytes = new byte[fs.Length];
                fs.Read(bytes, 0, (int)fs.Length);
                // Usually we don't need to mark private member as [IgnoreMember], but since I am using AllowPrivate here, we had to mark it.
                // The reason I use AllowPrivate here is because GameState has a private constructor.
                GameState gameState = MessagePackSerializer.Deserialize<GameState>(bytes, ContractlessStandardResolverAllowPrivate.Options);
                GameState.Instance.LoadSavedGame(gameState);
            }
        }
    }
}
