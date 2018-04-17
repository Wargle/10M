using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Access_10M.Sources.Metier
{
    /// <summary>
    /// La classe qui s'occupe de l'instanciation d'objet Human.
    /// </summary>
    public class FabriqueHuman
    {
        /// <summary>
        /// Instancie un Human à partir d'un MySqlDataReadert.
        /// </summary>
        /// <param name="reader">Le résultat de la requête BDD</param>
        /// <returns></returns>
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
