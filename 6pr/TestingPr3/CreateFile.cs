using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestingPr3
{
    public class CreateFile
    {
        private static CreateFile instance;
        private CreateFile()
        {}
        public static CreateFile getInstance()
        {
            if (instance == null)
                instance = new CreateFile();
            return instance;
        }
        public bool InsertInFile(string path, string source)
        {
            string[] stringArray = FindAllStrings.getInstance().FindStrings(source);
            StreamWriter sw = new StreamWriter(path);
            for(int i = 0; i < stringArray.Length; i++)
                sw.WriteLine(stringArray[i]);
            sw.Close();
            if (stringArray.Length == 0)
                return false;
            else
                return true;
        }
    }
}
