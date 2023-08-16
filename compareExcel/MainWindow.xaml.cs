using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Data;
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

namespace compareExcel
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private void addexecl1(object sender, RoutedEventArgs e)
        {
            string path = path_fileExecl(1);
            show1.Text = path;
        }

        private void addexecl2(object sender, RoutedEventArgs e)
        {
            string path = path_fileExecl(2);
            show2.Text = path;
        }

        private void CompareEvent(object sender, RoutedEventArgs e)
        {
            progessRing.Visibility = Visibility.Visible;

            try
            {
                var checker = new CompareModel(columnsBox1.Text, columnsBox2.Text, show1.Text, show2.Text);
                DataTable dt = checker.Check();
                dg1.ItemsSource = dt.AsDataView();

                checker = new CompareModel(columnsBox2.Text, columnsBox1.Text, show2.Text, show1.Text);
                DataTable dt2 = checker.Check();
                dg2.ItemsSource = dt2.AsDataView();

            }
            catch
            {

            }finally { progessRing.Visibility = Visibility.Hidden; }

        }

        string path_fileExecl(int numFile)
        {
            string path = string.Empty;

            var openFileDialog1 = new OpenFileDialog()
            {
                Filter = "Excel Files|*.xls;*.xlsx;*.xlsm",
                Title = "select excel  " + numFile,
            };
            
            if (openFileDialog1.ShowDialog() == true)
            {
                path = openFileDialog1.FileName;
            }
            return path;
        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
