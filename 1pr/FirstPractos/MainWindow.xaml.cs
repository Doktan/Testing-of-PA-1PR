using System;
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


namespace FirstPractos
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
        private void Selector_DropDownClosed(object sender, EventArgs e)
        {
            String selectedChar = Selector.Text;
            if(selectedChar != null)
            {
                Button.IsEnabled = true;
            }
            else
            {
                Button.IsEnabled = false;
            }
        }
        private void Selector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if(Selector.Text == "#")
            {
               string result = ModuleReplaceBySharp.ReplaceBySharp(textArea.Text);
               textArea.Text = result;
            }
            if(Selector.Text == "*")
            {
                string result = ModuleReplaceByStar.ReplaceByStar(textArea.Text);
                textArea.Text = result;
            }
        }
    }
}
