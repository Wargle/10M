using Access_10M.Sources.Metier;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Access_10M.Sources.Vue_Model
{
    /// <summary>
    /// La classe qui s'occupe de l'instanciation d'objet HumanVm.
    /// </summary>
    public class FabriqueHumanVm
    {
        /// <summary>
        /// Instancie un HumanVm à partir d'un MySqlDataReader et calcule la ressemblance de cet objet.
        /// </summary>
        /// <param name="reader">Le résultat de la requête BDD</param>
        /// <param name="hRef">L'Human de référence</param>
        /// <returns></returns>
        public static HumanVm FabriqueHumanVmFromSQL(MySqlDataReader reader, Human hRef)
        {
            HumanVm hvm = new HumanVm(FabriqueHuman.FabriqueHumanFromSQL(reader));
            hvm.SimilarityCalcul(hRef);
            return hvm;
        }

        /// <summary>
        /// Intancie un HumanVm à partir de paramètres.
        /// </summary>
        /// <param name="p">Le prénom</param>
        /// <param name="n">Le nom</param>
        /// <param name="s">Le sexe</param>
        /// <param name="d">La date de naissance</param>
        /// <returns></returns>
        public static HumanVm FrabriqueHumanVmFromParams(string p, string n, string s, DateTime d)
        {
            return new HumanVm(new Human(p, n, s, d, 0));
        }
    }
}
