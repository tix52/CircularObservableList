using CircularObservableCollection;
using NUnit.Framework;
using System;
using System.Collections.Specialized;

namespace CircularObservableCollectionTest
{
    [TestFixture]
    public class CircularObservableCollectionTest
    {
        [Test]
        public void AddTest()
        {
            uint size = 3;
            ObservableCircularCollection<int> testList = new ObservableCircularCollection<int>(size);
            testList.CollectionChanged += AddTrigger;
            for (int i = 0; i < size; i++)
            {
                testList.Add(i);
            }
            Assert.AreEqual(size, testList.Count);

            testList.CollectionChanged -= AddTrigger;
            for (int i = 0; i < size*size*size; i++)
            {
                testList.Add(i);
            }
            Assert.AreEqual(size, testList.Count);

        }

        [Test, ExpectedException(typeof(OverflowException))]
        public void OverflowExceptionTest()
        {
            uint size = 4294967295;
            ObservableCircularCollection<int> testList = new ObservableCircularCollection<int>(size);
        }


        #region EventHandlers
        private void AddTrigger(object sender, NotifyCollectionChangedEventArgs e)
        {
            Assert.AreEqual(NotifyCollectionChangedAction.Add, e.Action);
        }
        #endregion
    }
}
