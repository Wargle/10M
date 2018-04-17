using Access_10M.Sources.Metier;
using Access_10M.Sources.Utils;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Access_10M.Sources.Vue_Model
{
    /// <summary>
    /// La classe qui contient la liste des HumanVm générer par la recherche en BDD.
    /// </summary>
    public class Planet
    {
        private ObservableCollection<HumanVm> cancer;

        public Planet()
        {
            cancer = new ObservableCollection<HumanVm>();
        }

        public ObservableCollection<HumanVm> GetHumans() { return cancer; }

        /// <summary>
        /// Fait l'appel à la DAO pour effectuer une recherche et mettre à jour la liste.
        /// </summary>
        /// <param name="query">La requête pour la BDD</param>
        /// <param name="humanRef">L'HumanVm de comparaison pour déterminer la ressemblance</param>
        public void SearchHuman(MyQuery query, HumanVm humanRef)
        {
            cancer.ClearByThread();
            MySqlDataReader reader = DAO.Read(query);
            while (reader.Read())
            {
                cancer.AddByThread(FabriqueHumanVm.FabriqueHumanVmFromSQL(reader, humanRef.Model));
            }
        }
    }
}
