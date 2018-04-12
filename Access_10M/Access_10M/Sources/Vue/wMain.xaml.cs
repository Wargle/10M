using Access_10M.Sources.Metier;
using Access_10M.Sources.Utils;
using Access_10M.Sources.Vue_Model;
using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace Access_10M.Sources.Vue
{
    /// <summary>
    /// Logique d'interaction pour wMain.xaml
    /// </summary>
    public partial class wMain : Window
    {
        private AnimationControl animInfo, animSettings;
        private Planet planet;
        private HumanVm hRef;

        private BackgroundWorker worker;
        private MyException exception;
        private string command;

        public wMain()
        {
            planet = new Planet();
            InitializeComponent();
            listHuman.ItemsSource = planet.GetHumans();
            listHuman.Items.SortDescriptions.Add(new SortDescription("Similarity", ListSortDirection.Descending));

            animInfo = new AnimationControl();
            animInfo.AddAnimation(infoGrid, Resources["showInfo"] as Storyboard, Resources["hideInfo"] as Storyboard);

            animSettings = new AnimationControl();
            animSettings.AddAnimation(gridSettings, Resources["showSettings"] as Storyboard, Resources["hideSettings"] as Storyboard);
        }

        public void ShowInfo(string title, string mess)
        {
            if (!animInfo.IsExecuted)
            {
                animInfo.Back();
            }
            tb_title.Text = title;
            tb_mess.Text = mess;

            animInfo.Execute();
        }

        private void Click_CloseInfo(object sender, RoutedEventArgs e)
        {
            if(animInfo.IsExecuted)
                animInfo.Back();
        }

        private void KeyDown_EventControl(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if(e.Key == System.Windows.Input.Key.Enter)
            {
                Click_Search(null, null);
            }
        }

        private void LostFocus_TextBox(object sender, RoutedEventArgs e)
        {
            ((TextBox)sender).Text = ((TextBox)sender).Text.ToUpper();
        }

        private string TextBoxGetText(TextBox tb, string pattern)
        {
            if(MyRegex.IsMatch(pattern, tb.Text))
            {
                tb.BorderBrush = new SolidColorBrush(Colors.Black);
                return tb.Text;
            }
            else
            {
                tb.BorderBrush = new SolidColorBrush(Colors.Red);
                throw new MyException("Problem:: Illegal Argument", "Error on TextBox\nThe value ." + tb.Text 
                    + ". is not acceptable for the specified field.");
            }
        }

        private void ExecutionSearch(object sender, DoWorkEventArgs e)
        {
            try
            {
                MyQuery query = new MyQuery(command);
                planet.SearchHuman(query, hRef);
            }
            catch (MyException ex)
            {
                exception = ex;
            }
        }

        private void RunWorkerCompletedHandler(object sender, RunWorkerCompletedEventArgs e)
        {
            if(exception != null)
            {
                ShowInfo(exception.Title, exception.Message);
                exception = null;
            }
            pb_search.Visibility = Visibility.Hidden;
            Console.WriteLine("End Thread.");
        }

        private int ReachMaxInt(int val, int max)
        {
            if (val >= max)
                val = max - 1;
            return val;
        }

        private string TransformWordToRequest(string word)
        {
            string req = "^(";
            int len = word.Length;
            double path = len * 70f / 100f;
            int pa = (int)Math.Ceiling(path);
            for(int i = 0; i <= len - pa; i++)
            {
                if (i != 0)
                    req += ".*";
                req += word.Substring(i, pa) + ".*|";
            }
            foreach (char c in word)
                req += c + ".?";
            return req + ")$";
        }

        private void Click_Search(object sender, RoutedEventArgs e)
        {            
            try
            {
                command = "SELECT * FROM " + Application.Current.Resources["db_table"] + " WHERE ";

                string prenom = TextBoxGetText(tbPrenom, "^[a-zA-Z\\-]+$");
                string nom = TextBoxGetText(tbNom, "^[a-zA-Z\\-]+$");
                DateTime datenais = new DateTime(1, 1, 1);
                if (!tbDatenais.Text.Equals(""))
                {
                    datenais = DateTime.Parse(TextBoxGetText(tbDatenais, "^(((0[1-9]|[1-2][0-9]|3[0-1])[\\/\\-])((1[0-2]|0[1-9]|)[\\/\\-])[0-9]{1,4}"
                        + "|[0-9]{1,4}([\\/\\-](1[0-2]|0[1-9]|))([\\/\\-](0[1-9]|[1-2][0-9]|3[0-1])))$"));
                }
                string sexe = "H";
                if(slSexe.Value < 50)
                    sexe = "F";

                command += "prenom REGEXP '" + TransformWordToRequest(prenom) + "'";
                command += " AND nom REGEXP '" + TransformWordToRequest(nom) + "'";
                Console.WriteLine(command);

                hRef = FabriqueHumanVm.FrabriqueHumanVmFromParams(prenom, nom, sexe, datenais);

                worker = new BackgroundWorker { WorkerSupportsCancellation = true };
                worker.DoWork += ExecutionSearch;
                worker.RunWorkerCompleted += RunWorkerCompletedHandler;

                pb_search.Visibility = Visibility.Visible;
                worker.RunWorkerAsync();
            }
            catch (MyException ex)
            {
                ShowInfo(ex.Title, ex.Message);
            }
            catch (Exception ex)
            {
                ShowInfo("Error:: An error occurred", ex.Source + "\n" + ex.Message);
            }
        }

        private void KeyUp_TextBox(object sender, System.Windows.Input.KeyEventArgs e)
        {
            Application.Current.Resources["host"] = tbHost.Text;
            Application.Current.Resources["db"] = tbDb.Text;
            Application.Current.Resources["user"] = tbUser.Text;
            Application.Current.Resources["password"] = tbPw.Password;
            Application.Current.Resources["db_table"] = tbTable.Text;
            DAO.ForceClose();
        }

        private void MouseLeftDown_Logo(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if(e.ClickCount == 2)
            {
                animSettings.Toggle();
            }
        }
    }
}
