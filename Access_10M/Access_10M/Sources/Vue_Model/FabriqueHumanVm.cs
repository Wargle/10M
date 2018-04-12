using Access_10M.Sources.Metier;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Access_10M.Sources.Vue_Model
{
    public class FabriqueHumanVm
    {
        public static HumanVm FabriqueHumanVmFromSQL(MySqlDataReader reader, Human hRef)
        {
            HumanVm hvm = new HumanVm(FabriqueHuman.FabriqueHumanFromSQL(reader));
            hvm.SimilarityCalcul(hRef);
            return hvm;
        }

        public static HumanVm FrabriqueHumanVmFromParams(string p, string n, string s, DateTime d)
        {
            return new HumanVm(new Human(p, n, s, d, 0));
        }
    }
}
