using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medium
{
    public class NotLoggedMediumException : MediumException
    {
        public NotLoggedMediumException()
            : base("First must be logged.")
        { }
    }
}
