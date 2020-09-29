using System.ComponentModel;

namespace Gomoku
{
    /// <summary>
    /// A base class that is an event sender of Property Changed.
    /// </summary>
    public class ObservableObject : INotifyPropertyChanged
    {
        /// <summary>
        /// Property changed event
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Raise property changed event
        /// Note: It should be protecte virtual based on best practice.
        /// </summary>
        /// <param name="propName"></param>
        public void OnPropertyChanged(string propName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
    }
}
