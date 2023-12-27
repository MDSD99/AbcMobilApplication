using AbcMobil.Models;
using AbcMobil.Services;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Xamarin.Forms;

namespace AbcMobil.ViewModels
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        public IDataService ApiService => DependencyService.Get<IDataService>();
        public IDevice device => DependencyService.Get<IDevice>();

        bool isBusy = false;
        public bool IsBusy
        {
            get { return isBusy; }
            set
            {
                isBusy = value;
                OnPropertyChanged(nameof(IsBusy));
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
