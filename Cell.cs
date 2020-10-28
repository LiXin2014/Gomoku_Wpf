using Newtonsoft.Json;
using System;

namespace Gomoku
{
    [JsonObject(MemberSerialization.OptIn)]
    public class Cell : ObservableObject
    {
        #region Properties
        // Content of the cell
        private string content;
        // Position of the cell in board
        [JsonProperty]
        public int Row { get; set; }
        [JsonProperty]
        public int Col { get; set; }
        public RelayCommand ButtonCommand { get; private set; }
        #endregion

        /// <summary>
        /// Gets or sets content of a cell.
        /// </summary>
        [JsonProperty]
        public string Content { 
            get { return content; } 
            set { 
                this.content = value;
                OnPropertyChanged(nameof(Content));
            } 
        }

        public Cell(int row, int col)
        {
            PlayAStep play = new PlayAStep(row, col);
            this.ButtonCommand = play.PlayAStepCommand;
            this.Row = row;
            this.Col = col;
        }

       #region equals override
        public override bool Equals(Object obj)
        {
            //Check for null and compare run-time types.
            if ((obj == null) || !this.GetType().Equals(obj.GetType()))
            {
                return false;
            }
            else
            {
                Cell c = (Cell)obj;
                return (Content == c.Content);
            }
        }

        public static bool operator ==(Cell x, Cell y)
        {
            return x.Equals(y);
        }

        public static bool operator !=(Cell x, Cell y)
        {
            return !x.Equals(y);
        }
        #endregion
    }
}
