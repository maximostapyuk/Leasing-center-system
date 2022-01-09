using System.Windows;

namespace Client
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private int timeout = 100;
        public string workLogin="";

        public MainWindow()
        {
            InitializeComponent();
            ClientObject.SendRequestToServer("Клиент подключён");
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            if ((LoginBox.Text == "") || (PasBox.Password == ""))
            {
                LogLabel.Content = "Одно из полей является пустым";
            }
            else
            {
                ClientObject.SendRequestToServer("LOG IN");
                System.Threading.Thread.Sleep(timeout);
                ClientObject.SendRequestToServer(LoginBox.Text);
                System.Threading.Thread.Sleep(timeout);
                string answer = ClientObject.SendRequestToServer(PasBox.Password);
                if (answer == "ADMIN")
                {
                    LogLabel.Content = "";
                    workLogin = LoginBox.Text;
                    AdminWindow adminWindow = AdminWindow.getInstance(workLogin);
                    adminWindow.Show();
                }
                else if (answer == "USER")
                {
                    LogLabel.Content = "";
                    workLogin = LoginBox.Text;
                    EmployeeWindow employeeWindow = new EmployeeWindow(workLogin);
                    employeeWindow.Show();
                }                
                else LogLabel.Content = "Введён неверный логин или пароль";
            }            
        }

        private void Registration_Click(object sender, RoutedEventArgs e)
        {
            RegistrationWindow registrationWindow = new RegistrationWindow();
            registrationWindow.Show();
        }
    }
}
