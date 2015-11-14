using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TwitterApplication
{
    class User
    {
        public string userName { set; get; }

        public List<string> follows = new List<string>();

        public bool userFollows(string follow)
        {
            if (follows.Contains(follow))
                return true;
            else
                return false;
        }       
    }
}
