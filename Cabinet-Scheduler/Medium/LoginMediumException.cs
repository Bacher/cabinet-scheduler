using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medium
{
    public class LoginMediumException : MediumException
    {
        public LoginMediumException()
            : base("Login failed.")
        { }
    }
}
