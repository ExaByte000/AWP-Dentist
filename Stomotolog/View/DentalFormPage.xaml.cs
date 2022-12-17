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
    /// Логика взаимодействия для DentalFormPage.xaml
    /// </summary>
    public partial class DentalFormPage : Page
    {
        Core db = new Core();
        Patients currnetPatients;
        List<Teeth> arrayTeeth;
        List<DentalForm> arrayDental;
        Teeth activeTooth;
        int numberTooth;
        

        /// <summary>
        /// Инициализация
        /// </summary>
        public DentalFormPage(Patients activePatient)
        {
            InitializeComponent();
            currnetPatients = activePatient;
            arrayTeeth = db.context.Teeth.ToList();
            TeethListView.ItemsSource = arrayTeeth;
            string name = "Зубная формула пациента: " + currnetPatients.Surname + " " + currnetPatients.Name + " " + currnetPatients.MiddleName;
            PatientName.SetValue(TextBlock.TextProperty, name);
             
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
        /// Вызов контекстного меню
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SelectTooth(object sender, RoutedEventArgs e)
        {
            Button activeItem = sender as Button;
            activeTooth = activeItem.DataContext as Teeth;
            
            numberTooth = Convert.ToInt32(activeTooth.IDTeeth);

            string info = "Информация о зубе номер: " + activeTooth.NumberingTeeth.ToString();
            string number = "Изменение информации о зубе номер: " + activeTooth.NumberingTeeth.ToString();
            NumberTooth.SetValue(TextBlock.TextProperty, number);
            InfoTooth.SetValue(TextBlock.TextProperty, info);

            arrayDental = db.context.DentalForm.ToList();
            arrayDental = arrayDental.Where(x => x.TeethID == activeTooth.IDTeeth && x.PatientID == currnetPatients.IDPatient).ToList();
            DentalForm form = arrayDental.FirstOrDefault();
            if(form != null)
            {
                Scroll.Height = 250;
                DiscriptionTextBlock.Text = form.Discription;
            }
            else
            {
                Scroll.Height = 20;
                DiscriptionTextBlock.Text = "Информация о зубе отсутствует!";
            }
            ContextMenu cm = this.FindResource("cmButton") as ContextMenu;
            cm.PlacementTarget = sender as ListViewItem;
            cm.IsOpen = true;
           
        }

        /// <summary>
        /// Открытие окна для просмотра информации о зубе
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CheckInfoClick(object sender, RoutedEventArgs e)
        {
            AllPatients.IsEnabled = false;
            LogOut.IsEnabled = false;
            TeethListView.IsEnabled = false;
            TeethListView.Opacity = 0.1;
            Info.Visibility = Visibility.Visible;
        }

        /// <summary>
        /// Открыте окна для изменеия информации о зубе 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EditInfoClick(object sender, RoutedEventArgs e)
        {
            AllPatients.IsEnabled = false;
            LogOut.IsEnabled = false;
            TeethListView.IsEnabled = false;
            TeethListView.Opacity = 0.1;
            EditInfo.Visibility = Visibility.Visible;
        }

        /// <summary>
        /// Сохранение изменений описания зуба
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SaveChangeClick(object sender, RoutedEventArgs e)
        {
            arrayDental = db.context.DentalForm.ToList();
            arrayDental = arrayDental.Where(x => x.TeethID == activeTooth.IDTeeth && x.PatientID == currnetPatients.IDPatient).ToList();
            if (arrayDental.Count() != 0)
            {
                MessageBoxResult result = MessageBox.Show("У этого зуба уже присутствует описание, хотите его заменить?", "Изменение", MessageBoxButton.YesNoCancel, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    db.context.DentalForm.Remove(arrayDental.First());
                    db.context.SaveChanges();
                    if (DentalFormViewModel.Save(currnetPatients.IDPatient, numberTooth, DiscriptionTextBox.Text))
                    {
                        MessageBox.Show("Описание успешно сохранено!");
                    }
                    else
                    {
                        MessageBox.Show("Описание не сохранено! Что-то пошло не так :(");
                    }
                }
            }
            else
            {
                if (DentalFormViewModel.Save(currnetPatients.IDPatient, numberTooth, DiscriptionTextBox.Text))
                {
                    MessageBox.Show("Описание успешно сохранено!");
                }
                else
                {
                    MessageBox.Show("Описание не сохранено! Что-то пошло не так :(");
                }
            }
        }


        /// <summary>
        /// Закрытие окна для изменения информации о зубе
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GoBack(object sender, RoutedEventArgs e)
        {
            AllPatients.IsEnabled = true;
            LogOut.IsEnabled = true;
            TeethListView.IsEnabled = true;
            TeethListView.Opacity = 1;
            EditInfo.Visibility = Visibility.Collapsed;
            DiscriptionTextBox.Text = null;
        }

        /// <summary>
        /// Закрытие окна для просмотра информации о зубе
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GoToBack(object sender, RoutedEventArgs e)
        {
            AllPatients.IsEnabled = true;
            LogOut.IsEnabled = true;
            TeethListView.IsEnabled = true;
            TeethListView.Opacity = 1;
            Info.Visibility = Visibility.Collapsed;
        }
    }
}
