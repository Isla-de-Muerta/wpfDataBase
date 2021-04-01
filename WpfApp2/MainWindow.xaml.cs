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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp2
{
    public partial class MainWindow : Window
    {
        private ApplicationContext applicationContext;
        public MainWindow()
        {
            InitializeComponent();
            applicationContext = new ApplicationContext();
        }

        private void Register(object sender, RoutedEventArgs e)
        {
            var registerWindow = new Register();
            registerWindow.Show();
        }

        private void Authorize(object sender, RoutedEventArgs e)
        {
            var user = applicationContext.users.FirstOrDefault(x => x.email == loginTxtBox.Text && x.password == passwordTxtBox.Password);
            if (user != null)
            {
                var productsWindow = new ProductsWindow();
                productsWindow.Show();
            }
            else
                SnackBarRegister.MessageQueue.Enqueue("Incorrect login or password");
        }
    }
}
