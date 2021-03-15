using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestingPr3;
using System.IO;
using System;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Windows;
using OpenQA.Selenium;


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

            string[] res = FindAllStrings.getInstance().FindStrings("test1.txt");
            string[] expRes = { "1", "2", "3", "hello world" };
            CollectionAssert.AreEqual(res, FindAllStrings.getInstance().FindStrings("test1.txt"));

            Array.Resize(ref test, test.Length - 4);
            test[0] = "";
            sw = new StreamWriter("test1.txt", false);
            for (int i = 0; i < test.Length; i++)
                sw.WriteLine(test[i]);
            sw.Close();
            string[] expRes2 = { "" };
            CollectionAssert.AreEqual(expRes2, FindAllStrings.getInstance().FindStrings("test1.txt"));

            test[0] = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa";
            sw = new StreamWriter("test1.txt", false);
            for (int i = 0; i < test.Length; i++)
                sw.WriteLine(test[i]);
            sw.Close();

            string[] expRes3 = { };
            CollectionAssert.AreEqual(expRes3, FindAllStrings.getInstance().FindStrings("test1.txt"));
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

            Assert.IsFalse(CreateFile.getInstance().InsertInFile("test5.txt", "test3.txt")); // функция сработала 
            Assert.IsTrue(new FileInfo("test5.txt").Exists); // проверка существования
            Assert.AreEqual(0, new FileInfo("test5.txt").Length); // длина правильная


            Assert.IsTrue(CreateFile.getInstance().InsertInFile("test4.txt", "test2.txt")); // функция сработала 
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

        [TestMethod]
        public void IntegratatedTest()
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

    [TestClass]
    public class UItests
    {
        private const string WindowsApplicationDriverUrl = "http://127.0.0.1:4723";
        private const string WpfAppId = @"C:\Users\Doktan\source\repos\TestingPr3\TestingPr3\bin\Debug\TestingPr3.exe";

        protected static WindowsDriver<WindowsElement> session;

        // проведем инициализацию всех компонентов приложения
        private static WindowsElement Selector = null;
        private static WindowsElement LabelCombo = null;
        private static WindowsElement LabelInput = null;
        private static WindowsElement FileName = null;
        private static WindowsElement InputInFile = null;
        private static WindowsElement LabelOutput = null;
        private static WindowsElement OutputWindow = null;

        private static WindowsElement ButtonInArray = null;
        private static WindowsElement ButtonInFile = null;

        public object Привет { get; private set; }

        [ClassInitialize]
        public static void Setup(TestContext context)
        {
            if (session == null)
            {
                //DesiredCapabilities test = new DesiredCapabilities();
                var appiumOptions = new AppiumOptions();
                appiumOptions.AddAdditionalCapability("app", WpfAppId);
                appiumOptions.AddAdditionalCapability("deviceName", "WindowsPC");
                session = new WindowsDriver<WindowsElement>(new Uri(WindowsApplicationDriverUrl), appiumOptions);
           
            }
        }

        [TestMethod]
        public void Initialize()
        {
            // проверка инициализации элементов UI
            Selector = session.FindElementByAccessibilityId("Selector");
            Assert.IsNotNull(Selector);
            LabelCombo = session.FindElementByAccessibilityId("LabelCombo");
            Assert.IsNotNull(LabelCombo);
            LabelInput = session.FindElementByAccessibilityId("LabelInput");
            Assert.IsNotNull(LabelInput);
            FileName = session.FindElementByAccessibilityId("FileName");
            Assert.IsNotNull(FileName);
            InputInFile = session.FindElementByAccessibilityId("InputInFile");
            Assert.IsNotNull(InputInFile);
            LabelOutput = session.FindElementByAccessibilityId("LabelOutput");
            Assert.IsNotNull(LabelOutput);
            OutputWindow = session.FindElementByAccessibilityId("OutputWindow");
            Assert.IsNotNull(OutputWindow);

          /*  ButtonInArray = session.FindElementByAccessibilityId("ButtonInArray");
            Assert.IsNotNull(ButtonInArray);
            ButtonInFile = session.FindElementByAccessibilityId("ButtonInFile");
            Assert.IsNotNull(ButtonInFile);*/

            // проверка доступности элементов на момент запуска программы
            Assert.IsTrue(Selector.Enabled);
            Assert.IsFalse(FileName.Enabled);
            Assert.IsFalse(InputInFile.Enabled);
            Assert.IsFalse(OutputWindow.Enabled);

          //  Assert.IsTrue(ButtonInArray.Enabled);
         //   Assert.IsTrue(ButtonInFile.Enabled);

            //проверка работы элементов UI
            //выберем вывод в массив
            Selector.Click();
            Selector.SendKeys(Keys.Down + Keys.Down + Keys.Enter);
            Assert.AreEqual("Записать в массив", Selector.Text);
            string[] resArray = OutputWindow.Text.Split("\n");

            Selector.Click();
            Selector.SendKeys(Keys.Up + Keys.Enter);
            Assert.AreEqual("Записать в файл", Selector.Text);

            Assert.IsTrue(FileName.Enabled);
            Assert.IsTrue(InputInFile.Enabled);

            FileName.SendKeys("test");
            InputInFile.Click();
            string[] resFile = OutputWindow.Text.Split("\n");

            Array.Clear(resArray, 0, 1);
            Array.Clear(resFile, 0, 1);

            CollectionAssert.AreEqual(resArray, resFile);
            
           // ButtonInArray.Click();
            // System.Threading.Thread.Sleep(5000);
            //FileName.SendKeys("test");
            // InputInFile.Click();
            // Assert.AreEqual("test",FileName.Text);
           // System.Console.WriteLine(OutputWindow.Text);
          //  Assert.IsFalse(FileName.Enabled);
            
            //Console.WriteLine(OutputWindow.Text);
        }

      /*  [TestMethod]
        public void Enabled()
        {
            Assert.IsTrue(Selector.Enabled);
            Assert.IsFalse(FileName.Enabled);
            Assert.IsFalse(InputInFile.Enabled);
            Assert.IsFalse(OutputWindow.Enabled);
        }*/
       /* [TestMethod]
        public void UItest()
        {
            var Selector = session.FindElementByAccessibilityId("Selector");
            Selector.Click();
            var InArray = session.FindElementByAccessibilityId("InArray");
            //var InArray = session.FindElementByName("InArray");
            InArray.Click();
            //var OutputWindow = session.FindElementByAccessibilityId("OutputWindow");
           // System.Console.WriteLine(OutputWindow.Text);
            Assert.AreEqual("Записать в массив", Selector.Text);
        }*/

        [ClassCleanup]
        public static void Cleanup()
        {
            if (session != null)
            {
                session.Close();
                session.Quit();
            }
        }
    }
}
