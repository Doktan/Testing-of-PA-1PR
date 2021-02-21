using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstPractos
{
   public class ModuleReplaceBySharp
    {
        public static string ReplaceBySharp(string text)
        {
            string result = "";
             string[] subs = text.Split(new char[] { ' ', ',' });
             foreach(string s in subs)
             {
                 int outNumber;
                 bool isNumber = int.TryParse(s, out outNumber);
                 if(isNumber && (outNumber >= 1 && outNumber <= 10))
                 {
                    string sharp = "";
                    for (int i = 0; i < s.Length; i++)
                         sharp += "#";
                     result += sharp;
                 }
                 else
                 {
                     result += s;
                 }

             }
            return result;
            /*string[] subs = text.Split(new char[] { ' ', ',' });
            string[] spaces = {''};
            int interation = 0; 
            for(int i = 0; i < subs.Length + 1;  i++)
            {
                while(text[i].Equals(' ') || text[i].Equals(','))
                {
                    spaces[iteration] += text[i];
                }
            }
            return result;*/
        }
    }
}
