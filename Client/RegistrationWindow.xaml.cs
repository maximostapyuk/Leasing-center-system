using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Логика взаимодействия для RegistrationWindow.xaml
    /// </summary>
    public partial class RegistrationWindow : Window
    {
        private int timeout = 100;

        public RegistrationWindow()
        {
            InitializeComponent();
        }

        private void RegButton_Click(object sender, RoutedEventArgs e)
        {
            if ((Login1.Text == "") || (Password1.Text == "") || (Surname1.Text == "")
               || (Name1.Text == "") || (Thirdname.Text == "") || (Email1.Text == "") ||
               (Address1.Text == "") || (PhoneCode1.Text == "") || (Phone1.Text == "") ||
               (Sex.Text == "") || (Age1.Text == ""))
            {
                RegLable.Content="Одно из полей является пустым";
            }
            else
            {
                if (!Int32.TryParse(Phone1.Text, out int res1) || !Int32.TryParse(Age1.Text, out int res2))
                {
                    RegLable.Content = "Возраст и телефон должны быть числом!";
                }
                else if(!Check.IsValidEmailAddress(Email1.Text))
                {
                    RegLable.Content = "Поле Email заполнено неверно!";
                }
                else
                {
                    ClientObject.SendRequestToServer("REGISTRATION");
                    System.Threading.Thread.Sleep(timeout);
                    ClientObject.SendRequestToServer(Login1.Text);
                    System.Threading.Thread.Sleep(timeout);
                    ClientObject.SendRequestToServer(Password1.Text);
                    System.Threading.Thread.Sleep(timeout);
                    ClientObject.SendRequestToServer(Surname1.Text);
                    System.Threading.Thread.Sleep(timeout);
                    ClientObject.SendRequestToServer(Name1.Text);
                    System.Threading.Thread.Sleep(timeout);
                    ClientObject.SendRequestToServer(Thirdname.Text);
                    System.Threading.Thread.Sleep(timeout);
                    ClientObject.SendRequestToServer(Email1.Text);
                    System.Threading.Thread.Sleep(timeout);
                    ClientObject.SendRequestToServer(Address1.Text);
                    System.Threading.Thread.Sleep(timeout);
                    ClientObject.SendRequestToServer(PhoneCode1.Text);
                    System.Threading.Thread.Sleep(timeout);
                    ClientObject.SendRequestToServer(Phone1.Text);
                    System.Threading.Thread.Sleep(timeout);
                    ClientObject.SendRequestToServer(Sex.Text);
                    System.Threading.Thread.Sleep(timeout);
                    ClientObject.SendRequestToServer(Age1.Text);
                    System.Threading.Thread.Sleep(timeout);
                    RegLable.Content = ClientObject.SendRequestToServer("1");

                    System.Threading.Thread.Sleep(1000);
                    this.Close();
                    
                }
            }
        }

       
    }
    public static class Check
    {
        public static bool IsValidEmailAddress(this string s)
        {
            Regex regex = new Regex(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$");
            return regex.IsMatch(s);
        }
    }
   

}
