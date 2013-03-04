using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;

namespace Medium
{
    public class NetMediumException : MediumException
    {
        public NetMediumException(HttpStatusCode code)
            : base()
        {
            NetStatusCode = code;
        }

        public HttpStatusCode NetStatusCode
        {
            get;
            private set;
        }
    }
}
