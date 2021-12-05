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
using System.Windows.Shapes;

namespace Client
{
    /// <summary>
    /// Логика взаимодействия для EmployeeWindow.xaml
    /// </summary>
    public partial class EmployeeWindow : Window
    {
        public string workLogin = "";

        public EmployeeWindow(string login)
        {
            InitializeComponent();
            workLogin = login;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            CrudWindow crudWindow = new CrudWindow();
            crudWindow.Show();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            OrderWindow orderWindow = new OrderWindow();
            orderWindow.Show();
            System.Threading.Thread.Sleep(1000);
            orderWindow.Close();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            LeasingWindow leasingWindow = new LeasingWindow(workLogin);
            leasingWindow.Show();
        }
    }
}
