using MachineModel;
using MVVMBaseLib;
using Newtonsoft.Json;
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

namespace SettingMachine
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            LoadData();
        }
        public List<SettingKeyenceMachine> ListMachine { get; set; }

        private void LoadData()
        {
            ListMachine = new List<SettingKeyenceMachine>();
            ListMachine = JsonDB.Instance.Read<List<SettingKeyenceMachine>>(@"./SETUP/Setting.txt");
            //KeyenceGrid.ItemsSource = ListMachine;
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            JsonDB.Instance.Write(@"./SETUP/Setting.txt", KeyenceGrid.ItemsSource);
            Environment.Exit(1);
        }

        private void Button_Apply_Click(object sender, RoutedEventArgs e)
        {
            JsonDB.Instance.Write(@"./SETUP/Setting.txt", KeyenceGrid.ItemsSource);
        }
    }

    public class ViewModel : ViewModelBase
    {
        public ViewModel()
        {
            ListMachine = new List<SettingKeyenceMachine>();
            ListMachine = JsonDB.Instance.Read<List<SettingKeyenceMachine>>(@"./SETUP/Setting.txt");
            CommandInit();
        }

        private void CommandInit()
        {
            OKCommand = new RelayCommand(() => { OKCommandExecute(); });
            ApplyCommand = new RelayCommand(() => ApplyCommandExecute());
        }

        private void ApplyCommandExecute()
        {

        }

        private void OKCommandExecute()
        {

        }

        public List<SettingKeyenceMachine> ListMachine { get; set; }

        public ICommand OKCommand { get; set; }
        public ICommand ApplyCommand { get; set; }
    }
}
