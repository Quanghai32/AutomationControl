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

namespace PrismTest
{
    /// <summary>
    /// Interaction logic for ucTest.xaml
    /// </summary>
    public partial class MachineView : UserControl
    {
        public MachineView()
        {
            InitializeComponent();
            Binding bd = new Binding();
        }
        KeyenceMachineViewModel mc = new KeyenceMachineViewModel();

        private void mainBorder_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount > 1)
            {
                DetailView d = new DetailView();
                d.DataContext = mc;
                //d.Show();
            }
        }
    }
}
