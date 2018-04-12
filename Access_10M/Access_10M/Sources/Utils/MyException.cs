using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Access_10M.Sources.Utils
{
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
