using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstPractos
{
   public class ModuleReplaceByStar
    {
        public static string ReplaceByStar(string text)
        {
            string result = "";
            string[] subs = text.Split(new char[] { ' ', ',' });
            foreach (string s in subs)
            {
                int outNumber;
                bool isNumber = int.TryParse(s, out outNumber);
                if (isNumber && (outNumber >= 1 && outNumber <= 10))
                {
                    string star = "";
                    for (int i = 0; i < s.Length; i++)
                        star += "*";
                    result += star;
                }
                else
                {
                    result += s;
                }

            }
            return result;
        }
    }
}
