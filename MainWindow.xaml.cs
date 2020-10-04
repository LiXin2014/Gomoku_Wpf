using System.Windows;

namespace Gomoku
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            GameModel model = new GameModel();
            InitializeComponent();
            DataContext = model;
        }
    }
}
