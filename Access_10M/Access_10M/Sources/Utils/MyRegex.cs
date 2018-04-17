using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Access_10M.Sources.Utils
{
    /// <summary>
    /// Classe qui permet d'englober la validation de chaine de caractères par Regex
    /// </summary>
    public class MyRegex
    {
        /// <summary>
        /// Retourne si une chaine de caractères valide un patterne.
        /// </summary>
        /// <param name="pattern">Le patterne</param>
        /// <param name="input">La chaine de caractères</param>
        /// <returns>true ou false</returns>
        public static bool IsMatch(string pattern, string input)
        {
            Regex regex = new Regex(pattern);
            return regex.IsMatch(input);
        }
    }
}
