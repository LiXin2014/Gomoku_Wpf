﻿using Newtonsoft.Json;
using System;

namespace Gomoku
{
    [JsonObject(MemberSerialization.OptIn)]
    public class Cell : ObservableObject
    {
        // Content of the cell
        private string content;
        private readonly GameState state;

        // Position of the cell in board
        [JsonProperty]
        public int Row { get; set; }
        [JsonProperty]
        public int Col { get; set; }

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

        public Cell(int row, int col, GameState state)
        {
            this.ButtonCommand = new RelayCommand(OnClicked, UnClicked, CanBeClicked);
            this.Row = row;
            this.Col = col;
            this.state = state;
        }

        #region Button command
        public RelayCommand ButtonCommand { get; set; }

        /// <summary>
        /// Called when this cell is clicked
        /// </summary>
        /// <param name="parameter">This is content passed from CommandParameter in xaml</param>
        public void OnClicked(object parameter)
        {
            if (GameState.Instance.BlackSTurn)
            {
                this.Content = "●";
            } else
            {
                this.Content = "○";
            }

            if(GameState.Instance.Steps >= 8 && GameState.Instance.SomeoneHasWon(this.Row, this.Col))
            {
                GameState.Instance.GameEnded = true;
                GameState.Instance.Winner = GameState.Instance.BlackSTurn ? "Black" : "White";
                return;
            }

            GameState.Instance.Steps++;
            GameState.Instance.BlackSTurn = !GameState.Instance.BlackSTurn;

            state.UndoList.AddStep(ButtonCommand);
        }

        /// <summary>
        /// Called when user wants to undo a step.
        /// </summary>
        /// <param name="parameter"></param>
        public void UnClicked()
        {
            this.Content = "";

            if (GameState.Instance.GameEnded)
            {
                GameState.Instance.GameEnded = false;
                GameState.Instance.Winner = "";
            }

            GameState.Instance.Steps--;
            GameState.Instance.BlackSTurn = !GameState.Instance.BlackSTurn;
        }

        /// <summary>
        /// Can this cell be clicked.
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns>True if it can be clicked, false otherwise</returns>
        public bool CanBeClicked(object parameter)
        {
            return !GameState.Instance.GameEnded && (Content == null || String.Equals(Content, String.Empty));
        }
        #endregion

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
