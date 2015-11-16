using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TwitterApplication
{
    class Tweet
    {
        public string userName { set; get; }

        public string message { set; get; }

        public override string ToString()
        {
            return userName + "> " + message;
        }

    }
}
