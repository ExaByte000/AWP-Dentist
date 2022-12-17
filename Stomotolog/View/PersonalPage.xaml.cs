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
    /// Логика взаимодействия для PersonalPage.xaml
    /// </summary>
    public partial class PersonalPage : Page
    {
        Core db = new Core();
        
        List<Schedule> arraySchedule;
        Schedule activePatient;
        List<History> arrayHistory;
        List<Schedule> schedule;
        History itemHistory;
        Schedule itemSchedule;

        /// <summary>
        /// Инициализация
        /// </summary>
        public PersonalPage(List<Patients> arrayPatients)
        {
            InitializeComponent();

            DiagnosesComboBox.ItemsSource = db.context.Diagnoses.ToList();
            DiagnosesComboBox.DisplayMemberPath = "DagnosisName";
            DiagnosesComboBox.SelectedValuePath = "IDDiagnosis";
            DiagnosesComboBox.SelectedIndex = 0;

            arraySchedule = db.context.Schedule.Where(x => x.UserID == App.ActiveUser.IDUser).ToList();
            foreach(var i in arraySchedule)
            {
                Console.WriteLine();
                
            }
            Update();
            
            PatientsListView.ItemsSource = arraySchedule;
            if (arraySchedule.Count == 0)
            {
                TextBlock newTextBlock = new TextBlock
                {
                    Text = "На ближайшее время записей нет."
                };
                Image newImage = new Image();
                Uri newUri = new Uri("/Assets/images/logo.png", UriKind.Relative);
                newImage.Source = new BitmapImage(newUri);
                InfoStackPanel.Children.Add(newTextBlock);
            }
        }
        
        /// <summary>
        /// Обновление данных
        /// </summary>
        public void Update()
        {
            PatientsListView.ItemsSource = db.context.Schedule.Where(x => x.UserID == App.ActiveUser.IDUser).ToList();
        }

        /// <summary>
        /// Разлогиниванье Пользователя
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
        /// Удаление записи на прием
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DeleteTimeMenuClick(object sender, RoutedEventArgs e)
        {
            try
            {
                Button selectedButton = (Button)sender;
                MessageBoxResult result = MessageBox.Show("Вы действительно хотите удалить запись на прием?", "Удаление", MessageBoxButton.YesNoCancel, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                Schedule activeSchedule = selectedButton.DataContext as Schedule;
                if (PatientsViewModel.Del(db,activeSchedule))
                {
                    MessageBox.Show("Данные удалены");
                    //обновление DataGrid
                    //PatientsListView.ItemsSource = null;
                    Update();
                }
                else
                {
                    MessageBox.Show("Данные не удалены! Что-то пошло не по плану!");
                }
                   
                    
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Данные не удалены! Что-то пошло не по плану!");
            }
        }

        private void ChangeResultReceptionClick(object sender, RoutedEventArgs e)
        {
            Button activeButton = sender as Button;
            activePatient = activeButton.DataContext as Schedule;

            arrayHistory = db.context.History.Where(x => x.PatientID == activePatient.PatientID).ToList();
            itemHistory = arrayHistory.Where(x => x.Date == activePatient.Date && x.Time == activePatient.Time).FirstOrDefault();
            schedule = db.context.Schedule.Where(x => x.PatientID == activePatient.PatientID).ToList();
            itemSchedule = schedule.Where(x => x.Date == activePatient.Date && x.Time == activePatient.Time).FirstOrDefault();

            СomplaintsTextBox.Text = itemSchedule.Сomplaints;
            LaborСostsTextBox.Text = itemSchedule.LaborСosts;
            ResultsTextBox.Text = itemSchedule.Results;
            ComplicationsTextBox.Text = itemSchedule.Complications;
            StageTextBox.Text = itemSchedule.Stage;
            ICDcodeTextBox.Text = itemSchedule.ICDcode;
            DiagnosesComboBox.SelectedIndex = Convert.ToInt32(itemSchedule.DiagnosisID);


            AllPatients.IsEnabled = false;
            LogOut.IsEnabled = false;
            PatientsListView.IsEnabled = false;
            PatientsListView.Opacity = 0.2;
            ResultReception.Visibility = Visibility.Visible;
        }

        private void BackButton(object sender, RoutedEventArgs e)
        {
            AllPatients.IsEnabled = true;
            LogOut.IsEnabled = true;
            PatientsListView.IsEnabled = true;
            PatientsListView.Opacity = 1;
            ResultReception.Visibility = Visibility.Collapsed;
            Update();
        }

        private void SaveChangesResult(object sender, RoutedEventArgs e)
        {
            itemHistory.Сomplaints = СomplaintsTextBox.Text;
            itemHistory.Results = ResultsTextBox.Text;
            itemHistory.LaborСosts = LaborСostsTextBox.Text;
            itemHistory.DiagnosisID = Convert.ToInt32(DiagnosesComboBox.SelectedValue);
            itemHistory.Complications = ComplicationsTextBox.Text;
            itemHistory.Stage = StageTextBox.Text;
            itemHistory.ICDcode = ICDcodeTextBox.Text;

            itemSchedule.Сomplaints = СomplaintsTextBox.Text;
            itemSchedule.Results = ResultsTextBox.Text;
            itemSchedule.LaborСosts = LaborСostsTextBox.Text;
            itemSchedule.DiagnosisID = Convert.ToInt32(DiagnosesComboBox.SelectedValue);
            itemSchedule.Complications = ComplicationsTextBox.Text;
            itemSchedule.Stage = StageTextBox.Text;
            itemSchedule.ICDcode = ICDcodeTextBox.Text;
            if (PatientsViewModel.SaveResultReception(activePatient, itemHistory, itemSchedule, db))
            {
                MessageBox.Show("Изменения сохранены");
            }
            else
            {
                MessageBox.Show("Изменения не сохранены, что то пошло не по плану");
            }

        }
    }

       
    
}
