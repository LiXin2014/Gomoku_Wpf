namespace Gomoku
{
    public class Cell : ObservableObject
    {
        private string content;

        /// <summary>
        /// Gets or sets content of a cell.
        /// </summary>
        public string Content { 
            get { return content; } 
            set { 
                this.content = value;
                OnPropertyChanged(nameof(Content)); 
            } 
        }
    }
}
