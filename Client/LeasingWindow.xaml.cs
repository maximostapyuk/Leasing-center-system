using System.Data;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;

namespace Client
{
    /// <summary>
    /// Логика взаимодействия для LeasingWindow.xaml
    /// </summary>
    public partial class LeasingWindow : Window
    {
        private int timeout = 100;
        public string workLogin = "";

        public LeasingWindow(string login)
        {
            InitializeComponent();
            workLogin = login;
            ReadClient("@");
            ReadCar("@");
            ReadOperation("@");
        }
        
        private void LeasingOperation_Click(object sender, RoutedEventArgs e)
        {
            ClientIdLabel.Content = "";
            CarVinLabel.Content = "";
            ClientObject.SendRequestToServer("SELECT CLIENT");
            System.Threading.Thread.Sleep(timeout);
            DataTable dataTable = ClientObject.SendSelectRequestToServer("@");
            bool checkID = false;
            for(int i=0;i< ClientLeasingTable.Items.Count-1;i++)
            {
                if(ClientIdLeasing.Text == dataTable.Rows[i][0].ToString())
                {
                    checkID = true;
                    break;
                }
            }
            if (!checkID)
            {
                ClientIdLabel.Content = "Введен неверный ID.";
            }
            else
            {
                ClientObject.SendRequestToServer("SELECT CAR");
                System.Threading.Thread.Sleep(timeout);
                DataTable dataTbl = ClientObject.SendSelectRequestToServer("@");
                bool checkVIN = false;
                for (int i = 0; i < CarLeasingTable.Items.Count-1; i++)
                {
                    if (CarVinLeasing.Text == dataTbl.Rows[i][0].ToString())
                    {
                        checkVIN = true;
                        break;
                    }
                }
                if (!checkVIN)
                {
                    CarVinLabel.Content = "Введен неверный VIN.";
                }
                else
                {
                    ClientObject.SendRequestToServer("ADD OPERATION");
                    System.Threading.Thread.Sleep(timeout);
                    ClientObject.SendRequestToServer(ClientIdLeasing.Text);
                    System.Threading.Thread.Sleep(timeout);
                    ClientObject.SendRequestToServer(workLogin);
                    
                    System.Threading.Thread.Sleep(timeout);
                    CarVinLabel.Content = ClientObject.SendRequestToServer(CarVinLeasing.Text);
                }
            }


        }
        private void ReadClient(string info)
        {
            ClientObject.SendRequestToServer("SELECT CLIENT");
            System.Threading.Thread.Sleep(timeout);
            DataTable dataTable = ClientObject.SendSelectRequestToServer(info);
            ClientLeasingTable.ItemsSource = dataTable.DefaultView;
            //ClientLeasingTable.Columns[0].Visibility = Visibility.Collapsed;
        }
        private void ReadCar(string info)
        {
            ClientObject.SendRequestToServer("SELECT CAR");
            System.Threading.Thread.Sleep(timeout);
            DataTable dataTable = ClientObject.SendSelectRequestToServer(info);
            CarLeasingTable.ItemsSource = dataTable.DefaultView;
            //CarLeasingTable.Columns[0].Visibility = Visibility.Collapsed;
        }
        private void ReadOperation(string info)
        {
            ClientObject.SendRequestToServer("SELECT OPERATIONS");
            System.Threading.Thread.Sleep(timeout);
            DataTable dataTable = ClientObject.SendSelectRequestToServer(info);
            LeasingOpTable.ItemsSource = dataTable.DefaultView;
            //LeasingOpTable.Columns[0].Visibility = Visibility.Collapsed;
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ReadOperation("@");
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            PrintDialog dialog = new PrintDialog();
            if(dialog.ShowDialog() == true)
            {
                this.labelContent.Visibility = Visibility.Visible;
                dialog.PrintVisual(this.reportContent, "report");
                this.labelContent.Visibility = Visibility.Hidden;
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            
            int[] statistic = new int[6] { 0, 0, 0, 0, 0, 0 };

            ClientObject.SendRequestToServer("SELECT OPERATIONS");
            System.Threading.Thread.Sleep(timeout);
            DataTable dataTable = ClientObject.SendSelectRequestToServer("@");
            System.Threading.Thread.Sleep(timeout);
            int amount;
            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                amount = new Regex("12").Matches(dataTable.Rows[i][4].ToString()).Count;
                if (amount > 0)
                {
                    statistic[0]++;

                }
            }

            for (int j = 1; j < 6; j++)
            {
                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    string[] vs ;
                    vs= dataTable.Rows[i][4].ToString().Split('.');
                    amount = new Regex("0"+j.ToString()).Matches(vs[1]).Count;
                    if (amount > 0)
                    {
                        statistic[j]++;

                    }
                }
            }                  

            ChartForm chartForm = new ChartForm(statistic);
            chartForm.Show();

        }
    }
}
