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
            GameBoard board = new GameBoard(10);
            InitializeComponent();
            DataContext = board;
        }
    }
}
