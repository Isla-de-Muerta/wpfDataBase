using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
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

namespace WpfApp2
{
    /// <summary>
    /// Логика взаимодействия для Register.xaml
    /// </summary>
    public partial class Register : Window
    {
        private ApplicationContext applicationContext;
        public Register()
        {
            InitializeComponent();
            applicationContext = new ApplicationContext();
        }

        private void CreateUser(object sender, RoutedEventArgs e)
        {
            if (checkEmail() && checkNames() && checkPassword())
            {
                var user = new User
                {
                    name = nameTxtBox.Text,
                    familyName = familyNameTxtBox.Text,
                    email = EmailTxtBox.Text,
                    adress = adressTxtBox.Text,
                    password = passwordTxtBox.Password,
                    phoneNumber = phoneNumberTxtBox.Text
                };

                applicationContext.users.Add(user);
                applicationContext.SaveChanges();
            }
        }

        private bool checkPassword()
        {
            if (passwordTxtBox.Password.Length > 4)
            {
                return true;
            }
            SnackBarRegister.MessageQueue.Enqueue("Invalid password, should contain more than 4");
            return false;
        }

        private bool checkNames()
        {
            if (string.IsNullOrWhiteSpace(nameTxtBox.Text) && string.IsNullOrWhiteSpace(familyNameTxtBox.Text))
            {
                SnackBarRegister.MessageQueue.Enqueue("Please fill name and family name");
                return false;
            }
            return true;
        }

        private bool checkEmail()
        {
            try
            {
                var mail = new MailAddress(EmailTxtBox.Text);
                return true;
            }
            catch (ArgumentException)
            {
                SnackBarRegister.MessageQueue.Enqueue("Please insert email");
                return false;
            }
            catch (FormatException)
            {
                SnackBarRegister.MessageQueue.Enqueue("Please insert correct email");
                return false;
            }
        }

        private void phoneNumberTxtBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}
