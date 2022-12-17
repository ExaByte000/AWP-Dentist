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
using Stomotolog.Model;
using Stomotolog.ViewModel;

namespace Stomotolog.View
{
    /// <summary>
    /// Логика взаимодействия для RegUser.xaml
    /// </summary>
    public partial class RegUserPage : Page
    {
        /// <summary>
        /// Инициализация
        /// </summary>
        public RegUserPage()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Переход на страницу авторизации
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new AuthUserPage());
        }

        /// <summary>
        /// Регистрация нового пользователя
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RegButton_Click(object sender, RoutedEventArgs e)
        {
            if(UsersViewModel.RegUser(SureNameTextBox.Text, NameTextBox.Text, LastNameTextBox.Text, LogInTextBox.Text, AuthPasswordBox.Password, AuthPasswordBox2.Password))
            {
                MessageBox.Show("Вы были успешно зарегестрированы!","Уведомление",MessageBoxButton.OK,MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("Что то пошло не по плану! \nВозможно не совпадают пароли либо такой логин уже существует!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
    }
}
