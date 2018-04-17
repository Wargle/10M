using Access_10M.Sources.Metier;
using Access_10M.Sources.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Access_10M.Sources.Vue_Model
{
    /// <summary>
    /// La classe HumanVm fait le lien entre la Vue et le Model Human.
    /// Elle contient les divers champs à afficher avec leur coloration.
    /// </summary>
    public class HumanVm
    {
        private event PropertyChangedEventHandler PropertyChanged;
        private List<string> codeColor;

        private Human model;
        public Human Model { get { return model; } private set { model = value; } }

        public string Prenom { get { return model.Prenom; } set { model.Prenom = value; OnPropertyChanged("Prenom"); } }
        public string ColorPrenom { get; set; } = "Black";

        public string Nom { get { return model.Nom; } set { model.Nom = value; OnPropertyChanged("Nom"); } }
        public string ColorNom { get; set; } = "Black";

        public string Sexe { get { return model.Sexe; } set { model.Sexe = value; OnPropertyChanged("Sexe"); } }
        public string ColorSexe { get; set; } = "Black";

        public DateTime Datenais { get { return model.Datenais; } set { model.Datenais = value; OnPropertyChanged("Datenais"); } }
        public string ColorDatenais { get; set; } = "Black";

        public int Number { get { return model.Number; } set { model.Number = value; OnPropertyChanged("Number"); } }
        public string ColorNumber { get; set; } = "Black";

        public float Similarity { get; set; } = 100;

        public HumanVm(Human h)
        {
            model = h;
            codeColor = new List<string>()
            {
                "Red", "Orange", "Yellow", "LightGreen", "Green"
            };
        }

        /// <summary>
        /// Permet de calculer la ressemblance par rapport à l'objet de référence.
        /// Met à jour le pourcentage de ressemblance et les couleurs de champs à afficher.
        /// </summary>
        /// <param name="hRef">L'Human de référence</param>
        public void SimilarityCalcul(Human hRef)
        {
            int dist = EditDistance.CalculEditDistance(model.Nom, hRef.Nom);
            float len = hRef.Nom.Length;
            float percent = len / (len + dist) * 100f;
            Similarity -= 0.35f * (100 - percent);
            ColorNom = codeColor[(int)percent / 25];

            dist = EditDistance.CalculEditDistance(model.Prenom, hRef.Prenom);
            len = hRef.Prenom.Length;
            percent = len / (len + dist) * 100f;
            Similarity -= 0.35f * (100 - percent);
            ColorPrenom = codeColor[(int)percent / 25];

            if (!model.Sexe.Equals(hRef.Sexe))
            {
                ColorSexe = "Red";
                Similarity -= 0.15f * 100;
            }

            if (!model.Datenais.Equals(hRef.Datenais))
            {
                ColorDatenais = "Red";
                Similarity -= 0.15f * 100;
            }
        }

        private void OnPropertyChanged(string prop)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
