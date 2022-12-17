using Stomotolog.Model;
using Stomotolog.ViewModel;
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
    /// Логика взаимодействия для ExtractPatientPage.xaml
    /// </summary>
    public partial class ExtractPatientPage : Page
    {
        Core db = new Core();
        List<Extract> arrayExtract;
        Patients currentPatient;
        
        public ExtractPatientPage(Patients activePatient)
        {
            InitializeComponent();
            currentPatient = activePatient;

            
            arrayExtract = db.context.Extract.ToList();

            ExtractListView.ItemsSource = arrayExtract;
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

        private void ChangeText(object sender, TextChangedEventArgs e)
        {
            ExtractListView.ItemsSource = PatientsViewModel.Sort(arrayExtract, Search.Text, ExtractSort.SelectedIndex);
        }
    }
}
