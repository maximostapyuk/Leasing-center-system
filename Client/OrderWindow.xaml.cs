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
using System.Windows.Shapes;

namespace Client
{
    /// <summary>
    /// Логика взаимодействия для OrderWindow.xaml
    /// </summary>
    public partial class OrderWindow : Window
    {
        public OrderWindow()
        {
            InitializeComponent();
            GetInfo();
            PrintOrder();
        }
        private void GetInfo()
        {
            ClientObject.SendRequestToServer("SELECT CLIENT");
            System.Threading.Thread.Sleep(100);
            DataTable dataTable = ClientObject.SendSelectRequestToServer("@");
            ClientLeasingTable.ItemsSource = dataTable.DefaultView;
            ClientObject.SendRequestToServer("SELECT CAR");
            System.Threading.Thread.Sleep(100);
            dataTable = ClientObject.SendSelectRequestToServer("@");
            CarLeasingTable.ItemsSource = dataTable.DefaultView;
            ClientObject.SendRequestToServer("SELECT OPERATIONS");
            System.Threading.Thread.Sleep(100);
            dataTable = ClientObject.SendSelectRequestToServer("@");
            LeasingOpTable.ItemsSource = dataTable.DefaultView;            
        }
        private void PrintOrder()
        {
            PrintDialog dialog = new PrintDialog();
            if (dialog.ShowDialog() == true)
            {
                dialog.PrintVisual(this.reportContent, "report");
            }
        }


        }
}
