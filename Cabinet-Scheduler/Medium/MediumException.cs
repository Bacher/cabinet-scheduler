using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medium
{
    public class MediumException : ApplicationException
    {
        public MediumException()
            : base()
        { }

        public MediumException(string message)
            : base(message)
        { }
    }
}
