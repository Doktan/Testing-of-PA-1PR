using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TestingPr3
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ComboBox_DropDownClosed(object sender, EventArgs e)
        {
           if (Selector.Text == "Записать в файл")
            {
                InputInFile.IsEnabled = true;
                FileName.IsEnabled = true;
            }
            else
            {
                InputInFile.IsEnabled = false;
                FileName.IsEnabled = false;
                string[] stringArray = FindAllStrings.getInstance().FindStrings("f.txt");
                if (stringArray.Length > 0)
                {
                    OutputWindow.Text = "Содержимое массива:\n";
                    for (int i = 0; i < stringArray.Length; i++)
                    {
                        OutputWindow.Text += stringArray[i];
                        OutputWindow.Text += "\n";
                    }
                }
                else
                    OutputWindow.Text = "Массив пуст";
            }
        }

        private void InputInFile_Click(object sender, RoutedEventArgs e)
        {
            string name;
            if (FileName.Text != "")
                name = FileName.Text + ".txt";
            else
                name = "g.txt";
            bool rez = CreateFile.getInstance().InsertInFile(name, "f.txt");
            if (rez)
            {
                OutputWindow.Text = "Содержимое файла " + name + ":\n";
                StreamReader sr = new StreamReader(name);
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    OutputWindow.Text += line;
                    OutputWindow.Text += "\n";
                }
                sr.Close();
            }
            else
                OutputWindow.Text = "Файл пуст";
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            //var LabelOnComboBox = (Label)FindName("LabelCombo");
        }
    }
}
