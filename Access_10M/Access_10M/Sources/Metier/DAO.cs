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
    public class DAO
    {
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

        /**
         *  Permet d'ajouter les valeurs de la requête dans la Commande Sql 
         */
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

        /**
         * Permet d'écrire des données dans la BDD
         */
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

        /**
         * Permet d'écrire des données dans la BDD et de retouner des valeurs
         */
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

        /**
         * Permet de lire des données situées dans la BDD
         */
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

        /**
         * Permet de fermer la connexion à la BDD en cas de problème
         */
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