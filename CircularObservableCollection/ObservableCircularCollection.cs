using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;

namespace CircularObservableCollection
{
    public class ObservableCircularCollection<T> : ICollection<T>, INotifyCollectionChanged, INotifyPropertyChanged
    {
        private readonly ObservableCollection<T> _internCollection;
        private readonly uint _size;
        private uint _index;

        public event NotifyCollectionChangedEventHandler CollectionChanged;
        public event PropertyChangedEventHandler PropertyChanged;

        public ObservableCircularCollection(uint size)
        {
            _internCollection = new ObservableCollection<T>();
            _index = 0;
            _size = size;


            _internCollection.CollectionChanged += OnCollectionChanged;

        }

        private void OnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (CollectionChanged != null)
                CollectionChanged(this, e);
        }

        private void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(sender, e);
            }
        }

        public void Add(T item)
        {
            if (_index < _size)
            {
                _internCollection.Add(item);
                _index++;
                OnCollectionChanged(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, item));

            }
            else
            {
                int i;
                checked
                {
                    i = (int)(_index % _size);
                }
          //      if (i != _index) throw new IndexOutOfRangeException();
                var oldItem = _internCollection[i];
                _internCollection[i] = item;
                _index++;
                OnCollectionChanged(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Replace, item, oldItem, i));

            }
            OnPropertyChanged(this, new PropertyChangedEventArgs(nameof(_internCollection.Count)));
        }

        public void Clear()
        {
            _internCollection.Clear();
            _index = 0;
            PropertyChanged(this, new PropertyChangedEventArgs(nameof(_internCollection.Count)));
        }

        public bool Contains(T item)
        {
            return _internCollection.Contains(item);
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            _internCollection.CopyTo(array, arrayIndex);
        }

        public bool Remove(T item)
        {
            var result = _internCollection.Remove(item);
            _index--;
            OnPropertyChanged(this, new PropertyChangedEventArgs(nameof(_internCollection.Count)));
            return result;
        }

        public IEnumerator<T> GetEnumerator()
        {
            return _internCollection.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _internCollection.GetEnumerator();
        }

        public int Count
        {
            get
            {
                return _internCollection.Count;
            }
        }

        public bool IsReadOnly
        {
            get
            {
                return ((ICollection<T>)_internCollection).IsReadOnly;
            }
        }
    }
}
