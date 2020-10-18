using System.Windows.Input;

namespace Gomoku
{
    public interface IUndoableCommand : ICommand
    {
        void UnExecute();
    }
}
