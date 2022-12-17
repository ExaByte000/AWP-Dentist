
using System;
using System.Windows;
using System.Windows.Controls;
using Stomotolog.ViewModel;

namespace Stomotolog.View
{
    /// <summary>
    /// Логика взаимодействия для AuthUser.xaml
    /// </summary>
    public partial class AuthUserPage : Page
    {
        /// <summary>
        /// Инициализация
        /// </summary>
        public AuthUserPage()
        {
            InitializeComponent();
            Console.WriteLine(Convert.ToDateTime(null));
        }

        /// <summary>
        /// Переход на страницу регистрации
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RegButton_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new RegUserPage());
        }

        /// <summary>
        /// Авторизация пользователя
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SingInButton_Click(object sender, RoutedEventArgs e)
        {
            if(UsersViewModel.AuthUser(LogInTextBox.Text, AuthPasswordBox.Password))
            {
                App.Current.Resources["UserName"] = UsersViewModel.GetName(LogInTextBox.Text);
                UsersViewModel.SetNewActiveUser(LogInTextBox.Text, AuthPasswordBox.Password);
                this.NavigationService.Navigate(new AllPatients());
            }
            else
            {
                MessageBox.Show("Неверно введен логин или пароль!\n(Чувствительны к регисту)", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
            }
                
            
        }
    }
}
