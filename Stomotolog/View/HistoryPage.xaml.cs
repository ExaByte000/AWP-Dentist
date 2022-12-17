using Stomotolog.Model;
using Stomotolog.ViewModel;
//using StomotologLibrary;
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

namespace Stomotolog.View
{
    /// <summary>
    /// Логика взаимодействия для HistoryPage.xaml
    /// </summary>
    public partial class HistoryPage : Page
    {

        /// <summary>
        /// Инициализация
        /// </summary>
        Core db = new Core();
        Patients currnetPatients;
        List<History> arrayHistory;
        public HistoryPage(Patients activePatient)
        {
            InitializeComponent();

            arrayHistory = db.context.History.ToList();

            currnetPatients = activePatient;
            string name = "История посещений пациента: "+currnetPatients.Surname + " " + currnetPatients.Name + " " + currnetPatients.MiddleName;
            NamePatient.SetValue(TextBlock.TextProperty, name);

            arrayHistory = arrayHistory.Where(x => x.PatientID == activePatient.IDPatient).ToList();

            HistoryListView.ItemsSource = arrayHistory;

        }

        /// <summary>
        /// Разлогиниванье пользователя
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LogOutButton(object sender, RoutedEventArgs e)
        {
            
            this.NavigationService.Navigate(new AuthUserPage());
        }

        /// <summary>
        /// Переход на страницу "Все пациенты"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AllPatientsButton(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new AllPatients());
        }

        /// <summary>
        /// Переход на страницу "Расписание"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NearestButton(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new PersonalPage(null));
        }

        /// <summary>
        /// Вызов метода, сортирующего историю посещений
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ChangeText(object sender, TextChangedEventArgs e)
        {
            HistoryListView.ItemsSource = PatientsViewModel.AcceptSort(arrayHistory, Search.Text, HistorySort.SelectedIndex);
        }
    }
}
