using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Access_10M.Sources.Metier
{
    public class FabriqueHuman
    {
        public static Human FabriqueHumanFromSQL(MySqlDataReader reader)
        {
            Human h = new Human(
                (string)reader["prenom"],
                (string)reader["nom"],
                (string)reader["sexe"],
                (DateTime)reader["datenais"],
                (int)reader["number"],
                (int)reader["id"]);
            return h;
        }
    }
}
