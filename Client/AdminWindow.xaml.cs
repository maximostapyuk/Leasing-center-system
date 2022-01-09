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
    /// Логика взаимодействия для AdminWindow.xaml
    /// </summary>
    public partial class AdminWindow : Window
    {
        private static AdminWindow instance;

        public static AdminWindow getInstance(string login)
        {
            if (instance == null)
                instance = new AdminWindow(login);
            return instance;
        }
        
        public string workLogin = "";

        public AdminWindow(string login)
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
            MethodWindow methodWindow = new MethodWindow();
            methodWindow.Show();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            UserManagementWindow userManagementWindow = new UserManagementWindow();
            userManagementWindow.Show();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            OrderWindow orderWindow = new OrderWindow();
            orderWindow.Show();
            System.Threading.Thread.Sleep(1000);
            orderWindow.Close();
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            LeasingWindow leasingWindow = new LeasingWindow(workLogin);
            leasingWindow.Show();
        }
    }
}
