using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Access_10M.Sources.Utils
{
    /// <summary>
    /// Classe qui permet d'étendre les méthodes de l'ObservableCollextion
    /// </summary>
    public static class ObservableCollectionExtension
    {
        /// <summary>
        /// Permet d'accéder à la méthode Add d'une ObservableCollection à partir d'un Thread.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection"></param>
        /// <param name="item"></param>
        public static void AddByThread<T>(this ObservableCollection<T> collection, T item)
        {
            Action<T> addMethod = collection.Add;
            Application.Current.Dispatcher.BeginInvoke(addMethod, item);
        }

        /// <summary>
        /// Permet d'accéder à la méthode Clear d'une ObservableCollection à partir d'un Thread.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection"></param>
        public static void ClearByThread<T>(this ObservableCollection<T> collection)
        {
            Action clearMethod = collection.Clear;
            Application.Current.Dispatcher.BeginInvoke(clearMethod);
        }

        /// <summary>
        /// Permet de supprimer des éléments d'une ObersvableCollection sous condition.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="observableCollection"></param>
        /// <param name="condition"></param>
        /// <returns></returns>
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
