using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Access_10M.Sources.Utils
{
    public static class ObservableCollectionExtension
    {
        public static void AddByThread<T>(this ObservableCollection<T> collection, T item)
        {
            Action<T> addMethod = collection.Add;
            Application.Current.Dispatcher.BeginInvoke(addMethod, item);
        }

        public static void ClearByThread<T>(this ObservableCollection<T> collection)
        {
            Action clearMethod = collection.Clear;
            Application.Current.Dispatcher.BeginInvoke(clearMethod);
        }

        public static int Remove<T>(this ObservableCollection<T> observableCollection, Func<T, bool> condition)
        {
            var itemsToRemove = observableCollection.Where(condition).ToList();

            foreach (var itemToRemove in itemsToRemove)
            {
                observableCollection.Remove(itemToRemove);
            }

            return itemsToRemove.Count;
        }
    }
}
