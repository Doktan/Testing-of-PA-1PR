using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestingPr3;
using System.IO;
using System;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTests
    {
        [TestMethod]
        public void TestFindAllStrings()
        {
            // тестирование поиска строк в файле
            // для этого создадим тестовые текстовые файлы с исходнымид данными
            string[] test = { "1", "2", "3", "hello world", "fffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffff" };
            StreamWriter sw = new StreamWriter("test1.txt");
            for (int i = 0; i < test.Length; i++)
                sw.WriteLine(test[i]);
            sw.Close();

            string[] res = TestingPr3.FindAllStrings.getInstance().FindStrings("test1.txt");
            string[] expRes = { "1", "2", "3", "hello world" };
            CollectionAssert.AreEqual(res, TestingPr3.FindAllStrings.getInstance().FindStrings("test1.txt"));

            Array.Resize(ref test, test.Length - 4);
            test[0] = "";
            sw = new StreamWriter("test1.txt", false);
            for (int i = 0; i < test.Length; i++)
                sw.WriteLine(test[i]);
            sw.Close();
            string[] expRes2 = { "" };
            CollectionAssert.AreEqual(expRes2, TestingPr3.FindAllStrings.getInstance().FindStrings("test1.txt"));

            test[0] = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa";
            sw = new StreamWriter("test1.txt", false);
            for (int i = 0; i < test.Length; i++)
                sw.WriteLine(test[i]);
            sw.Close();

            string[] expRes3 = { };
            CollectionAssert.AreEqual(expRes3, TestingPr3.FindAllStrings.getInstance().FindStrings("test1.txt"));
            File.Delete("test1.txt");
        }

        [TestMethod]
        public void TestCreateFile()
        {
            //тестирование функции создания файл
            //метод возвращавет false, если файл пуст
            // true, если в нем есть записи
            string[] test = { "1", "2", "3", "hello world"};
            StreamWriter sw = new StreamWriter("test2.txt");
            for (int i = 0; i < test.Length; i++)
                sw.WriteLine(test[i]);
            sw.Close();

            string test2 = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaa"; // длина строки = 20
            sw = new StreamWriter("test3.txt");
            sw.WriteLine(test2);
            sw.Close();

            Assert.IsFalse(TestingPr3.CreateFile.getInstance().InsertInFile("test5.txt", "test3.txt")); // функция сработала 
            Assert.IsTrue(new FileInfo("test5.txt").Exists); // проверка существования
            Assert.AreEqual(0, new FileInfo("test5.txt").Length); // длина правильная


            Assert.IsTrue(TestingPr3.CreateFile.getInstance().InsertInFile("test4.txt", "test2.txt")); // функция сработала 
            Assert.IsTrue(new FileInfo("test4.txt").Exists); // проверка существования

            StreamReader sr = new StreamReader("test4.txt");
            string line;
            int count = 0;
            while ((line = sr.ReadLine()) != null)
                count++;
            sr.Close();
            Assert.AreEqual(4, count); // длина правильная

            File.Delete("test2.txt");
            File.Delete("test3.txt");
            File.Delete("test4.txt");
            File.Delete("test5.txt");
        }
    }

    [TestClass]
    public class IntegratedTest
    {
        [TestMethod]
        public void IntTest()
        {
            //данный тест проверяет правильную работоспособность программы
            //в целом, т.е. совместеное работу модуля
            //в моем случае, это возможно, если выбрать режим записи в файл

            //создадим файл, в котором записаны исходные данные
            string[] test = { "1", "2", "3", "hello world" };
            StreamWriter sw = new StreamWriter("test.txt");
            for (int i = 0; i < test.Length; i++)
                sw.WriteLine(test[i]);
            sw.Close();

            //теперь нужно вызывать модуль (внутри него вложен первый модуль!!!)
            CreateFile.getInstance().InsertInFile("test2.txt", "test.txt");

            //проверка, что файл создался
            Assert.IsTrue(File.Exists("test2.txt"));

            StreamReader sr = new StreamReader("test2.txt");
            string line;
            int count = 0;
            string[] actualStrings = new string[4];
            while ((line = sr.ReadLine()) != null)
            {
                actualStrings[count] = line;
                count++;
            }
            sr.Close();

            //проверка количества строк
            Assert.AreEqual(4, count);

            //проверка, что первый модуль поймал правильные строки
            string[] expRes = { "1", "2", "3", "hello world" };
            CollectionAssert.AreEqual(expRes, actualStrings);

            File.Delete("test.txt");
            File.Delete("test2.txt");
        }
    }
}
