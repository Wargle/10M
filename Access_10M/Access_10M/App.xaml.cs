using Access_10M.Sources.Vue;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Access_10M
{
    /// <summary>
    /// Logique d'interaction pour App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            try
            {
                StreamReader file = new StreamReader(@"..\..\Resources\params.json");
                string line = file.ReadLine();
                Dictionary<object, object> parameters = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<object, object>>(line);

                foreach (KeyValuePair<object, object> param in parameters)
                {
                    Current.Resources[param.Key] = param.Value;
                }

                file.Close();
            }
            catch(Exception ex)
            {
                
            }

            base.OnStartup(e);
        }

        public static void MyOnExit()
        {
            try
            {
                Dictionary<object, object> parameters = new Dictionary<object, object>();
                ResourceDictionary paramsRes = Current.Resources.MergedDictionaries[2];
                foreach (var k in paramsRes.Keys)
                {
                    parameters[k] = Current.Resources[k];
                }

                StreamWriter file = new StreamWriter(@"..\..\Resources\params.json");
                file.Write(Newtonsoft.Json.JsonConvert.SerializeObject(parameters));
                file.Close();

                if (wMain.easter != null)
                    wMain.easter.Abort();
            }
            catch (Exception ex)
            {

            }
            Current.Shutdown();
        }

        protected override void OnExit(ExitEventArgs e)
        {
            MyOnExit();
        }
    }
}
