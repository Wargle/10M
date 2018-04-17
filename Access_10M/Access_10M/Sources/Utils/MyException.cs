using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Access_10M.Sources.Utils
{
    /// <summary>
    /// Classe Exception permettant de stocker un titre à notre exception.
    /// </summary>
    public class MyException : Exception
    {
        private string title;
        public string Title { get { return title; } private set { title = value; } }

        public MyException(string title, string message)
            : base(message)
        {
            Title = title;
        }
    }
}
