using Autodesk.Revit.UI;
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

namespace RevitAPICategoryOpeningApplicationInterface
{
    /// <summary>
    /// Логика взаимодействия для MainView.xaml
    /// </summary>
    public partial class MainView : Window
    {
        public MainView(ExternalCommandData commandData)// при запуске окна передаем commandData
        {
            InitializeComponent();
            MainViewViewModel vm = new MainViewViewModel(commandData);
            vm.CloseRequest += (s, e) => this.Close();//при выполнении команды закрываем окно, логика по которой окно будет закрываться самостоятельно
            DataContext = vm;
        }
    }
}
