using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HarmoniaRemote
{
    class Helpers
    {
        private Helpers() { } // Private ctor for class with all static methods.

        public static string GetAppDir()
        {
            return System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
        }
    }
}
