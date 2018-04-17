using Access_10M.Sources.Utils;
using System;

namespace Access_10M.Sources.Metier
{
    /// <summary>
    /// Classe Model qui stocke les valeurs d'un Human.
    /// </summary>
    public class Human
    {
        private int id;

        private string prenom, nom, sexe;
        private DateTime dateais;
        private int number;

        public string Prenom { get { return prenom; } set { prenom = value; } }
        public string Nom { get { return nom; } set { nom = value; } }
        public string Sexe { get { return sexe; } set { sexe = value; } }
        public DateTime Datenais { get { return dateais; } set { dateais = value; } }
        public int Number { get { return number; } set { number = value; } }

        public Human(string p, string n, string s, DateTime d, int nu, int id = 0)
        {
            this.id = id;
            Prenom = p;
            Nom = n;
            Sexe = s;
            Datenais = d;
            Number = nu;
        }

        public override string ToString()
        {
            return prenom + " " + nom;
        }
    }
}
