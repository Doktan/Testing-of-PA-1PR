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
            // ������������ ������ ����� � �����
            // ��� ����� �������� �������� ��������� ����� � ���������� �������
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
            //������������ ������� �������� ����
            //����� ����������� false, ���� ���� ����
            // true, ���� � ��� ���� ������
            string[] test = { "1", "2", "3", "hello world"};
            StreamWriter sw = new StreamWriter("test2.txt");
            for (int i = 0; i < test.Length; i++)
                sw.WriteLine(test[i]);
            sw.Close();

            string test2 = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaa"; // ����� ������ = 20
            sw = new StreamWriter("test3.txt");
            sw.WriteLine(test2);
            sw.Close();

            Assert.IsFalse(TestingPr3.CreateFile.getInstance().InsertInFile("test5.txt", "test3.txt")); // ������� ��������� 
            Assert.IsTrue(new FileInfo("test5.txt").Exists); // �������� �������������
            Assert.AreEqual(0, new FileInfo("test5.txt").Length); // ����� ����������


            Assert.IsTrue(TestingPr3.CreateFile.getInstance().InsertInFile("test4.txt", "test2.txt")); // ������� ��������� 
            Assert.IsTrue(new FileInfo("test4.txt").Exists); // �������� �������������

            StreamReader sr = new StreamReader("test4.txt");
            string line;
            int count = 0;
            while ((line = sr.ReadLine()) != null)
                count++;
            sr.Close();
            Assert.AreEqual(4, count); // ����� ����������

            File.Delete("test2.txt");
            File.Delete("test3.txt");
            File.Delete("test4.txt");
            File.Delete("test5.txt");
        }
    }
}
