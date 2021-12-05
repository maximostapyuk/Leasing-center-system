using System;
using System.Data;
using System.Windows;

namespace Client
{
    /// <summary>
    /// Логика взаимодействия для CrudWindow.xaml
    /// </summary>
    public partial class CrudWindow : Window
    {
        private int timeout = 100;

        public CrudWindow()
        {
            InitializeComponent();
            ClientObject.SendRequestToServer("Клиент подключён");
            
            ReadClient();
            
        }
        private void ReadClient()
        {
            SurnameRedComboBox.Items.Clear();
            SurnameDelComboBox.Items.Clear();

            ClientObject.SendRequestToServer("SELECT CLIENT");
            System.Threading.Thread.Sleep(timeout);
            DataTable dataTable = ClientObject.SendSelectRequestToServer("@");
            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                SurnameRedComboBox.Items.Add(dataTable.Rows[i][2].ToString());
            }
            ClientObject.SendRequestToServer("SELECT CLIENT");
            System.Threading.Thread.Sleep(timeout);
            dataTable = ClientObject.SendSelectRequestToServer("@");
            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                SurnameDelComboBox.Items.Add(dataTable.Rows[i][2].ToString());
            }



        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ReadClient();
            if (ClientSearchTextBox.Text == "")
            {
                ClientSearchLabel.Content = "Поиск по всей таблице \"Клиенты\"";
            }
            else
            {
                ClientSearchLabel.Content = "Поиск: " + ClientSearchTextBox.Text;
            }

            ClientObject.SendRequestToServer("SELECT CLIENT");
            System.Threading.Thread.Sleep(timeout);
            if (ClientSearchTextBox.Text != "")
            {
                DataTable dataTable = ClientObject.SendSelectRequestToServer(ClientSearchTextBox.Text);
                ClientDataGrid.ItemsSource = dataTable.DefaultView;
                ClientDataGrid.Columns[0].Visibility = Visibility.Collapsed;

            }
            else
            {
                DataTable dataTable = ClientObject.SendSelectRequestToServer("@");

                ClientDataGrid.ItemsSource = dataTable.DefaultView;
                ClientDataGrid.Columns[0].Visibility = Visibility.Collapsed;

            }
            ClientDataGrid.Columns[6].Visibility = System.Windows.Visibility.Hidden;



        }

        //Add client
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if ((AddClientAge.Text == "") || (AddClientNameTextBox.Text == "") || (AddClientSurnameTextBox.Text == "")
                || (AddThirdnameTextBox.Text == "") || (AddClientGenderTextBox.Text == "") || (AddClientEmailTextBox.Text == "") ||
                (AddClientPhoneCodeTextBox.Text == "") || (AddClientPhoneTextBox.Text == "") || (AddClientAdressTextBox.Text == ""))
            {
                AddClientLabel.Content = "Одно из основных полей пустое";
            }
            else
            {
                if (!Int32.TryParse(AddClientAge.Text, out int res1) || !Int32.TryParse(AddClientPhoneTextBox.Text, out int res2))
                {
                    AddClientLabel.Content = "Возраст и телефон должны быть числом";
                }
                else
                {
                    ClientObject.SendRequestToServer("ADD CLIENT");
                    System.Threading.Thread.Sleep(timeout);
                    ClientObject.SendRequestToServer(AddClientNameTextBox.Text);
                    System.Threading.Thread.Sleep(timeout);
                    ClientObject.SendRequestToServer(AddClientSurnameTextBox.Text);
                    System.Threading.Thread.Sleep(timeout);
                    ClientObject.SendRequestToServer(AddThirdnameTextBox.Text);
                    System.Threading.Thread.Sleep(timeout);
                    ClientObject.SendRequestToServer(AddClientGenderTextBox.Text);
                    System.Threading.Thread.Sleep(timeout);
                    ClientObject.SendRequestToServer(AddClientEmailTextBox.Text);
                    System.Threading.Thread.Sleep(timeout);
                    ClientObject.SendRequestToServer(AddClientPhoneCodeTextBox.Text);
                    System.Threading.Thread.Sleep(timeout);
                    ClientObject.SendRequestToServer(AddClientPhoneTextBox.Text);
                    System.Threading.Thread.Sleep(timeout);
                    ClientObject.SendRequestToServer(AddClientAge.Text);
                    System.Threading.Thread.Sleep(timeout);
                    AddClientLabel.Content = ClientObject.SendRequestToServer(AddClientAdressTextBox.Text);
                }
            }
        }

        //Del client
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            if (SurnameDelComboBox.Text == "")
            {
                DelclLabel.Content = "Выберите клиента";
            }
            else
            {
                string DelID = GetClientID(SurnameDelComboBox.Text);
                    ClientObject.SendRequestToServer("DELETE CLIENT");
                    System.Threading.Thread.Sleep(timeout);
                    DelclLabel.Content = ClientObject.SendRequestToServer(DelID);
                
            }
        }

        //Change client
        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            string clientID;
            if ((SurnameRedComboBox.Text == "") || (ChangeClientAge.Text == "") || (ChangeClientNameTextBox.Text == "") || (ChangeClientSurnameTextBox.Text == "")
                || (ChangeClientThirdnameTextBox.Text == "") || (ChangeClientGenderTextBox.Text == "") || (ChangeClientEmailTextBox.Text == "")
                || (ChangeClientAdressTextBox.Text == "") || (ChangeClientPhoneCodeTextBox.Text == "") || (ChangeClientPhoneTextBox.Text == ""))
            {
                ChangeClientLabel.Content = "Одно из основных полей пустое";
            }
            else
            {
                if (!Int32.TryParse(ChangeClientPhoneTextBox.Text, out int res1) || !Int32.TryParse(ChangeClientAge.Text, out int res2))
                {
                    ChangeClientLabel.Content = "Поля ID, возраст и телефон должны быть числами";
                }
                else
                {
                    clientID = GetClientID(SurnameRedComboBox.Text);
                    ClientObject.SendRequestToServer("UPDATE CLIENT");
                    System.Threading.Thread.Sleep(timeout);
                    ClientObject.SendRequestToServer(clientID);
                    System.Threading.Thread.Sleep(timeout);
                    ClientObject.SendRequestToServer(ChangeClientNameTextBox.Text);
                    System.Threading.Thread.Sleep(timeout);
                    ClientObject.SendRequestToServer(ChangeClientSurnameTextBox.Text);
                    System.Threading.Thread.Sleep(timeout);
                    ClientObject.SendRequestToServer(ChangeClientThirdnameTextBox.Text);
                    System.Threading.Thread.Sleep(timeout);
                    ClientObject.SendRequestToServer(ChangeClientGenderTextBox.Text);
                    System.Threading.Thread.Sleep(timeout);
                    ClientObject.SendRequestToServer(ChangeClientEmailTextBox.Text);
                    System.Threading.Thread.Sleep(timeout);
                    ClientObject.SendRequestToServer(ChangeClientPhoneTextBox.Text);
                    System.Threading.Thread.Sleep(timeout);
                    ClientObject.SendRequestToServer(ChangeClientPhoneCodeTextBox.Text);
                    System.Threading.Thread.Sleep(timeout);
                    ClientObject.SendRequestToServer(ChangeClientAge.Text);
                    System.Threading.Thread.Sleep(timeout);
                    ChangeClientLabel.Content = ClientObject.SendRequestToServer(ChangeClientAdressTextBox.Text);
                }
            }
        }
        private string GetClientID(string surname)
        {
            ClientObject.SendRequestToServer("SELECT CLIENT");
            System.Threading.Thread.Sleep(timeout);
            DataTable dataTable = ClientObject.SendSelectRequestToServer(surname);
            string clID = dataTable.Rows[0][0].ToString();
            return clID;
        }
        //Cars group
        //Search car
        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            if (SearchTextBox.Text == "")
            {
                ClientSearchLabel.Content = "Поиск по всей таблице \"Автомобилей\"";
            }
            else
            {
                SearchLabel.Content = "Поиск: " + SearchTextBox.Text;
            }

            ClientObject.SendRequestToServer("SELECT CAR");
            System.Threading.Thread.Sleep(timeout);
            if (SearchTextBox.Text != "")
            {
                DataTable dataTable = ClientObject.SendSelectRequestToServer(SearchTextBox.Text);
                SeekerDataGrid.ItemsSource = dataTable.DefaultView;
                //SeekerDataGrid.Columns[6].Visibility = System.Windows.Visibility.Hidden;
                SeekerDataGrid.Columns[0].Visibility = Visibility.Collapsed;

            }
            else
            {
                DataTable dataTable = ClientObject.SendSelectRequestToServer("@");
                SeekerDataGrid.ItemsSource = dataTable.DefaultView;
                //SeekerDataGrid.Columns[6].Visibility = System.Windows.Visibility.Hidden;
                SeekerDataGrid.Columns[0].Visibility = Visibility.Collapsed;

            }
        }

        //Add cars
        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            if ((AddVinBox.Text == "") || (AddCurrencyBox.Text == "") ||
                (AddSpeedBox.Text == "") || (AddGearBox.Text == "") || (AddCostBox.Text == "") ||
                (AddBrandBox.Text == "") || (AddNameBox.Text == "") || (AddDiscr.Text == ""))
            {
                AddBox.Content = "Одно из основных полей пустое";
            }
            else
            {
                if (!Int32.TryParse(AddSpeedBox.Text, out int res1) || !Int32.TryParse(AddCostBox.Text, out int res2))
                {
                    AddBox.Content = "Скорость и стоимость должна быть числом";
                }
                else
                {
                    ClientObject.SendRequestToServer("ADD CAR");
                    System.Threading.Thread.Sleep(timeout);
                    ClientObject.SendRequestToServer(AddGearBox.Text);
                    System.Threading.Thread.Sleep(timeout);
                    ClientObject.SendRequestToServer(AddCurrencyBox.Text);
                    System.Threading.Thread.Sleep(timeout);
                    ClientObject.SendRequestToServer(AddSpeedBox.Text);
                    System.Threading.Thread.Sleep(timeout);
                    ClientObject.SendRequestToServer(AddCostBox.Text);
                    System.Threading.Thread.Sleep(timeout);
                    ClientObject.SendRequestToServer(AddDiscr.Text);
                    System.Threading.Thread.Sleep(timeout);
                    ClientObject.SendRequestToServer(AddBrandBox.Text);
                    System.Threading.Thread.Sleep(timeout);
                    ClientObject.SendRequestToServer(AddVinBox.Text);
                    System.Threading.Thread.Sleep(timeout);
                    AddBox.Content = ClientObject.SendRequestToServer(AddNameBox.Text);
                }
            }
        }

        //Del cars
        private void Button_Click_6(object sender, RoutedEventArgs e)
        {
            if (DelIdBox.Text == "")
            {
                DelLabel.Content = "Поле Id пустое";
            }
            else
            {
                ClientObject.SendRequestToServer("DELETE CAR");
                System.Threading.Thread.Sleep(timeout);
                DelLabel.Content = ClientObject.SendRequestToServer(DelIdBox.Text);
            }
        }

        //Change cars
        private void Button_Click_7(object sender, RoutedEventArgs e)
        {

            if ((BrandBox.Text == "") || (AddDiscr.Text == "") || (NameBox.Text == "") ||
                (CostBox.Text == "") || (VinBox.Text == "") || (CurrencyBox.Text == "") ||
                (SpeedBox.Text == "") || (GearBox.Text == ""))
            {
                AddBox.Content = "Одно из основных полей пустое";
            }
            else
            {
                if (!Int32.TryParse(CostBox.Text, out int res1) || !Int32.TryParse(SpeedBox.Text, out int res2))
                {
                    AddBox.Content = "Поля Id, Стоимость и Максимальная скорость должны быть числами";
                }
                else
                {
                    ClientObject.SendRequestToServer("UPDATE CAR");
                    System.Threading.Thread.Sleep(timeout);
                    ClientObject.SendRequestToServer(VinBox.Text);
                    System.Threading.Thread.Sleep(timeout);
                    ClientObject.SendRequestToServer(GearBox.Text);
                    System.Threading.Thread.Sleep(timeout);
                    ClientObject.SendRequestToServer(CurrencyBox.Text);
                    System.Threading.Thread.Sleep(timeout);
                    ClientObject.SendRequestToServer(SpeedBox.Text);
                    System.Threading.Thread.Sleep(timeout);
                    ClientObject.SendRequestToServer(CostBox.Text);
                    System.Threading.Thread.Sleep(timeout);
                    ClientObject.SendRequestToServer(NameBox.Text);
                    System.Threading.Thread.Sleep(timeout);
                    ClientObject.SendRequestToServer(BrandBox.Text);
                    System.Threading.Thread.Sleep(timeout);
                    ChangeLabel.Content = ClientObject.SendRequestToServer(ChangeBox.Text);
                }
            }
        }

        private void Button_Click_8(object sender, RoutedEventArgs e)
        {
            if (SurnameRedComboBox.Text !="") 
            {
                ClientObject.SendRequestToServer("SELECT CLIENT");/*GET CLIENT INFO*/
                System.Threading.Thread.Sleep(timeout);

                DataTable dataTable = ClientObject.SendSelectRequestToServer(SurnameRedComboBox.Text);
                ChangeClientGenderTextBox.Text = dataTable.Rows[0][1].ToString();
                ChangeClientSurnameTextBox.Text = dataTable.Rows[0][2].ToString();
                ChangeClientNameTextBox.Text = dataTable.Rows[0][3].ToString();
                ChangeClientThirdnameTextBox.Text = dataTable.Rows[0][4].ToString();
                ChangeClientAge.Text = dataTable.Rows[0][5].ToString();
                ChangeClientEmailTextBox.Text = dataTable.Rows[0][6].ToString();
                ChangeClientAdressTextBox.Text = dataTable.Rows[0][7].ToString();
                ChangeClientPhoneCodeTextBox.Text = dataTable.Rows[0][8].ToString();
                ChangeClientPhoneTextBox.Text = dataTable.Rows[0][9].ToString(); 
            }

        }

        private void SurnameRedComboBox_Copy_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {

        }

    }
}
