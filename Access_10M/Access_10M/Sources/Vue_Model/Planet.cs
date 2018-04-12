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
    public class Planet
    {
        private ObservableCollection<HumanVm> cancer;

        public Planet()
        {
            cancer = new ObservableCollection<HumanVm>();
        }

        public ObservableCollection<HumanVm> GetHumans() { return cancer; }

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
