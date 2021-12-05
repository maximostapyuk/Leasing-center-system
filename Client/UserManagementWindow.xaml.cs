using System;
using System.Data;
using System.Windows;
using System.Windows.Controls;

namespace Client
{
    /// <summary>
    /// Логика взаимодействия для UserManagementWindow.xaml
    /// </summary>
    public partial class UserManagementWindow : Window
    {
        int timeout = 100;
        public UserManagementWindow()
        {
            InitializeComponent();
            ReadInfo("All");
            ReadInfo("Users");

        }
                
        private void ReadInfo(string info)
        {
            ClientObject.SendRequestToServer("READ EMPLOYEES");
            System.Threading.Thread.Sleep(timeout);       
            DataTable dataTable = ClientObject.SendSelectRequestToServer(info);
            if (info == "All")
                EmployesTable_Del.ItemsSource = dataTable.DefaultView;
            else
                EmployesTable_Red.ItemsSource = dataTable.DefaultView;
        }

        private void RefreshRed_Click(object sender, RoutedEventArgs e)
        {
            ReadInfo("Users");
        }

        private void RefreshDel_Click(object sender, RoutedEventArgs e)
        {
            ReadInfo("All");
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (DelID.Text == "")
            {
                DelEmpLabel.Content = "Поле Id пустое";
            }
            else
            {
                if (!Int32.TryParse(DelID.Text, out int res))
                {
                    DelEmpLabel.Content = "Id должно быть числом";
                }
                else
                {
                    ClientObject.SendRequestToServer("DELETE EMPLOYEE");
                    System.Threading.Thread.Sleep(timeout);
                    DelEmpLabel.Content = ClientObject.SendRequestToServer(DelID.Text);
                }
            }
        }

        private void RedEmploye_Click(object sender, RoutedEventArgs e)
        {
            if (RedID.Text == "")
            {
                RedEmpLabel.Content = "Поле Id пустое";
            }
            else
            {
                if (!Int32.TryParse(RedID.Text, out int res))
                {
                    RedEmpLabel.Content = "Id должно быть числом";
                }
                else
                {                    
                    ClientObject.SendRequestToServer("RED EMPLOYEE ACCESS");
                    System.Threading.Thread.Sleep(timeout);
                    RedEmpLabel.Content = ClientObject.SendRequestToServer(RedID.Text);
                }
            }

        }
    }
}
