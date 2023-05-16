using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SzyfrPlayfaira
{
    public static class PlayfairPrepare
    {
        private static string Prepare(this string s) 
            => s.ToLower().Replace(" ", "").Replace("j", "i");
        public static string PrepareKey(this string s) => s.Prepare();

        public static string PrepareText(this string s)
        {
            s = s.Prepare();
            if (s.Length % 2 != 0)
                s = s + "z";
            return s;
        }
        
    }
}
