using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;
using System.Windows.Controls;

namespace SHSM.Devices
{
    public abstract class Device : INotifyPropertyChanged
    {
        private bool _state;
        public int Id { get; set; }
        public string Name { get; set; }
        public Type Type { get; set; }
        public Place Place { get; set; }
        public bool State
        {
            get => _state;
            set { _state = value; OnPropertyChanged(); }
        }

        [NotMapped]
        public Image ImageOn { get; set; }
        [NotMapped]
        public Image ImageOff { get; set; }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
