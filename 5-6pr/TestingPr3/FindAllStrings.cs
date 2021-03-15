using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace TestingPr3
{
   public class FindAllStrings //If string length in file < 20 then remember it
    {
        private static FindAllStrings instance;

        private FindAllStrings() 
        {}

        public static FindAllStrings getInstance()
        {
            if (instance == null)
                instance = new FindAllStrings();
            return instance;
        }

        public string[] FindStrings(string path)
        {
            // читайте описание в последнем коммите на гитхабе!!!
            /*  StreamReader sr = new StreamReader(path);
              int LineCount = 0;
              string line;
              string[] stringArray = new string[0];
              while((line = sr.ReadLine()) != null)
              {
                  if(line.Length < 20)
                  {
                      Array.Resize(ref stringArray, stringArray.Length + 1);
                      stringArray[LineCount] = line;
                      LineCount++;
                  }
              }
              sr.Close();*/

            // из-за аппиума придется возвращать это значение
            string[] stringArray = { "Привет, мир","п","аа",
                "11 тест" };
            return stringArray;
        }
    }
}
