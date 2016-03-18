using CircularObservableCollection;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Threading;

namespace ObservableCircularCollectionGui
{
    public class ScreenViewModel : INotifyPropertyChanged
    {
        private readonly Dispatcher dispatcher;

        public ScreenViewModel()
        {
            _onOkButtonClickCommand = new CommandHandler(OkButtonClickHandler, (o) => { return true; });
            _itemSource = new ObservableCircularCollection<int>(0);
            dispatcher = Dispatcher.CurrentDispatcher;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName]string propertyName = null)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        private uint _count;
        public uint Count
        {
            get
            {
                return _count;
            }
            set
            {
                _count = value;
                _itemSource = new ObservableCircularCollection<int>(Count);
                OnPropertyChanged();
                OnPropertyChanged(nameof(ItemSource));

            }
        }

        private ICommand _onOkButtonClickCommand;
        public ICommand OnOkButtonClickcommand
        {
            get
            {
                return _onOkButtonClickCommand;
            }
        }

        private void OkButtonClickHandler(object parameter)
        {
            Async();
        }

        private void Async()
        {
            Task.Factory.StartNew(() =>
            {
                int i = 0;
                while (true)
                    dispatcher.Invoke( () => ItemSource.Add(i++));
            });
        }

        private ObservableCircularCollection<int> _itemSource;
        public ObservableCircularCollection<int> ItemSource
        {
            get
            {
                return _itemSource;
            }
            set
            {
                _itemSource = value;
                OnPropertyChanged();
            }
        }

    }
}
