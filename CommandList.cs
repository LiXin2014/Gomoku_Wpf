using System.Collections.Generic;

namespace Gomoku
{
    public class CommandList
    {
        public Stack<RelayCommand> Steps { get; set; }

        public RelayCommand UndoCommand { get; set; }

        public CommandList()
        {
            Steps = new Stack<RelayCommand>();
            UndoCommand = new RelayCommand(RemoveStep);
        }

        public void AddStep(RelayCommand step)
        {
            Steps.Push(step);
        }

        public void RemoveStep(object parameter)
        {
            if(Steps.Count > 0)
            {
                Steps.Peek().UnExecute();
                Steps.Pop();
            }
        }
    }
}
