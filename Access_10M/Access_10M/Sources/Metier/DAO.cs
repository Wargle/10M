using Access_10M.Sources.Utils;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Access_10M.Sources.Metier
{
    /// <summary>
    /// Classe qui englobe l'accès à une base de données.
    /// </summary>
    public class DAO
    {
        /// <summary>
        /// Classe qui stocke l'objet BDD en Singleton.
        /// </summary>
        public class DataBase
        {
            private static MySqlConnection connection;

            public static MySqlConnection GetInstance()
            {
                if(connection == null)
                {
                     string host = Application.Current.Resources["host"].ToString(),
                        db = Application.Current.Resources["db"].ToString(),
                        user = Application.Current.Resources["user"].ToString(),
                        pw = Application.Current.Resources["password"].ToString();

                    connection = new MySqlConnection("SERVER=" + host + "; DATABASE=" + db + "; UID=" + user + "; PASSWORD=" + pw +";");
                }
                return connection;
            }

            public static void ForceClose()
            {
                if (connection != null)
                {
                    connection.Close();
                    connection = null;
                }
            }
        }

        /// <summary>
        /// Renvoi une instance ouverte de la BDD
        /// </summary>
        /// <returns></returns>
        public static MySqlConnection InitConnection()
        {
            try
            {
                MySqlConnection instance = DataBase.GetInstance();
                instance.Close();
                instance.Open();

                return instance;
            }
            catch (Exception e)
            {
                throw new MyException("Error:: Connection to DataBase", e.Source + "\n" + e.Message);
            }
        }

        /// <summary>
        /// Permet d'ajouter les valeurs de la requête dans la Commande Sql
        /// </summary>
        /// <param name="cmd">L'objet MySqlCommand</param>
        /// <param name="vals">Toutes les valeurs</param>        
        private static void InitValues(MySqlCommand cmd, Dictionary<string, object> vals)
        {
            if (vals != null)
            {
                foreach (KeyValuePair<string, object> kv in vals)
                {
                    cmd.Parameters.AddWithValue(kv.Key, kv.Value);
                }
            }
        }

        /// <summary>
        /// Permet d'écrire des données dans la BDD
        /// </summary>
        /// <param name="query">Un objet MyQuery</param>
        public static void Execute(MyQuery query)
        {
            try
            {
                MySqlConnection instance = InitConnection();

                MySqlCommand command = new MySqlCommand(query.Request, instance) { CommandTimeout = 3600 };
                InitValues(command, query.Vals);

                command.ExecuteNonQuery();
            }
            catch (MyException me)
            {
                throw me;
            }
            catch (Exception e)
            {
                throw new MyException("Error:: Command ExecuteNonQuery on DataBase", e.Source + "\n" + e.Message);
            }
        }

        /// <summary>
        /// Permet d'écrire des données dans la BDD et de retouner des valeurs
        /// </summary>
        /// <param name="query">Un objet MyQuery</param>
        /// <returns>La valeur de la requête</returns>
        public static object ExecuteWithRet(MyQuery query)
        {
            try
            {
                MySqlConnection instance = InitConnection();

                MySqlCommand command = new MySqlCommand(query.Request, instance) { CommandTimeout = 3600 };
                InitValues(command, query.Vals);

                return command.ExecuteScalar();
            }
            catch (MyException me)
            {
                throw me;
            }
            catch (Exception e)
            {
                throw new MyException("Error:: Command ExecuteScalar on DataBase", e.Source + "\n" + e.Message);
            }
        }
        
        /// <summary>
        /// Permet de lire des données situées dans la BDD
        /// </summary>
        /// <param name="query">Un objet MyQuery</param>
        public static MySqlDataReader Read(MyQuery query)
        {
            try
            {
                MySqlConnection instance = InitConnection();

                MySqlCommand command = new MySqlCommand(query.Request, instance) { CommandTimeout = 3600 };
                InitValues(command, query.Vals);

                MySqlDataReader reader = command.ExecuteReader();

                return reader;
            }
            catch (MyException me)
            {
                throw me;
            }
            catch (Exception e)
            {
                throw new MyException("Error:: Command ExecuteReader on DataBase", e.Source + "\n" + e.Message);
            }
        }

        /// <summary>
        /// Permet de fermer la connexion à la BDD en cas de problème
        /// </summary>
        public static void ForceClose()
        {
            try
            {
                DataBase.ForceClose();
            }
            catch (Exception e)
            {
                throw new MyException("Error:: While Closing DataBase", e.Source + "\n" + e.Message);
            }
        }
    }
}