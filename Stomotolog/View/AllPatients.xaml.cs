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
    /// Логика взаимодействия для AllPatients.xaml
    /// </summary>
    public partial class AllPatients : Page
    {
        Core db = new Core();
        List<Patients> arrayPatients;
        Patients activePatient;
        List<Schedule> arraySchedule;
        List<PatAlle> activePatAlle;
        
        Patients item;



        /// <summary>
        /// Инициализация
        /// </summary>
        public AllPatients()
        {
            InitializeComponent();

            AllergyComboBox.ItemsSource = db.context.Allergy.ToList();
            AllergyComboBox.DisplayMemberPath = "NameAllergy";
            AllergyComboBox.SelectedValuePath = "IDAllergy";
            AllergyComboBox.SelectedIndex = 0;

            Update();

            if (arrayPatients.Count == 0)
            {
                TextBlock newTextBlock = new TextBlock
                {
                    Text = "Информация о пациентах отсутствует"
                };
                Image newImage = new Image();
                Uri newUri = new Uri("/Assets/images/logo.png", UriKind.Relative);
                newImage.Source = new BitmapImage(newUri);
                ChangeTime.Children.Add(newTextBlock);
                //InfoStackPanel.Children.Add(newImage);


            }
            TextBlockUserName.Text = App.Current.Resources["UserName"].ToString();
        }

        /// <summary>
        /// Блокировка кнопок и непрозрачность ListView
        /// </summary>
        private void Block()
        {
            AddPatientButton.IsEnabled = false;
            NearestButton.IsEnabled = false;
            LogOutButton.IsEnabled = false;
            PatientsListView.IsEnabled = false;
            ExtractButton.IsEnabled = false;
            PatientsListView.Opacity = 0.2;
        }

        /// <summary>
        /// Разблокировка кнопок и прозрачность ListView
        /// </summary>
        private void Unblock()
        {
            AddPatientButton.IsEnabled = true;
            NearestButton.IsEnabled = true;
            LogOutButton.IsEnabled = true;
            PatientsListView.IsEnabled = true;
            ExtractButton.IsEnabled = true;
            PatientsListView.Opacity = 1;
        }

        /// <summary>
        /// Обновление таблицы
        /// </summary>
        private void Update()
        {
            arrayPatients = db.context.Patients.Where(x => x.UserID == App.ActiveUser.IDUser).ToList();
            PatientsListView.ItemsSource = arrayPatients;
        }
        /// <summary>
        /// Обновление таблица аллергии
        /// </summary>
        private void UpdateAlle()
        {
            activePatAlle = db.context.PatAlle.Where(x => x.PatientID == activePatient.IDPatient).ToList();
            if (activePatAlle.Count != 0)
            {
                NoAllergyTextBlock.Visibility = Visibility.Collapsed;
                AllergyListView.Visibility = Visibility.Visible;
            }
            else
            {
                NoAllergyTextBlock.Visibility = Visibility.Visible;
                AllergyListView.Visibility = Visibility.Collapsed;

            }
        }

        
        /// <summary>
        /// Разлогиниванье пользователя
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LogOutButtonClick(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new AuthUserPage());
        }

        /// <summary>
        ///  Выписка пациента
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DeleteMenuClick(object sender, RoutedEventArgs e)
        {
            try
            {
                MenuItem selectedButton = (MenuItem)sender;
                item = selectedButton.DataContext as Patients;
                
                MessageBoxResult result = MessageBox.Show("ВНИМАНИЕ!\n Если вы выпишите пациента, то удалятся\n все записи на прием с его участием.\n Вы действительно хотите выписать пациента?", "Выписка", MessageBoxButton.YesNoCancel, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    MessageBox.Show("Перед выпиской нужно составить заключение.", "Выписка");
                    ConclusionGrid.Visibility = Visibility.Visible;
                    Block();
                }
                //обновление DataGrid
                Update();

            }
            catch (Exception)
            {
                MessageBox.Show("Данные не удалены! Что то пошло не по плану!");
            }
        }
        
        /// <summary>
        /// Вызов контекстного меню
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ContextButton_Click(object sender, RoutedEventArgs e)
        {
            Button activeButton = sender as Button;
            activePatient=activeButton.DataContext as Patients;

            string text = "Аллергия пациента: " + activePatient.Surname+ " " + activePatient.Name;
            PatientAllergyTextBlock.Text = text;
            Name.Text = text;

            AllergyListView.ItemsSource = db.context.PatAlle.Where(x => x.PatientID == activePatient.IDPatient).ToList();

            UpdateAlle();
            
            ContextMenu cm = this.FindResource("cmButton") as ContextMenu;
            cm.PlacementTarget = sender as Button;
            cm.IsOpen = true;
            
        }
       
        /// <summary>
        /// Переход к окну изменения времени записи
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ChangeTimeMenuClick(object sender, RoutedEventArgs e)
        {
            Block();
            
            ChangeTime.Visibility = Visibility.Visible;

        }


        /// <summary>
        /// Переход к окну добавления нового пациента
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddPatientButtonClick(object sender, RoutedEventArgs e)
        {
            Block();
            
            AddPatient.Visibility = Visibility.Visible;
        }

        private void ChangeDentalFormClick(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new DentalFormPage(activePatient));
        }
        /// <summary>
        /// Переход к списку записей на прием
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NearestButtonClick(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new PersonalPage(arrayPatients));
        }

        /// <summary>
        /// Добавление записи на прием
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MakeАppointment_Click(object sender, RoutedEventArgs e)
        {
            if(PatientsViewModel.EditTime(Convert.ToDateTime(DateTextBoxCalendar.SelectedDate.Value), TimeTextBox.Text, activePatient.IDPatient))
            {
                Update();
                MessageBox.Show("Запись на прием успешно добавлена!");
                
            }
            else
            {
                MessageBox.Show("Запись не была добавлена.");
            }
        }

       
        /// <summary>
        /// Закрытие окна с возможностью записи времени приема
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BackTimeButton_Click(object sender, RoutedEventArgs e)
        {
            Unblock();
            ChangeTime.Visibility = Visibility.Collapsed;
            TimeTextBox.Text = null;
            
        }

        /// <summary>
        /// Добавление нового пациента
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddPatient_Click(object sender, RoutedEventArgs e)
        {
            if (PatientsViewModel.AddPathient(SureNameTextBox.Text, NameTextBox.Text, LastNameTextBox.Text, PhoneTextBox.Text, db))
            {
                Update();
                MessageBox.Show("Пациент был успешно добавлен!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("Что то пошло не по плану! \nВозможно не совпадают пароли либо такой логин уже существует!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        /// <summary>
        /// Закрытие окна, позволяющего добавить пациента
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BackPatientButton_Click(object sender, RoutedEventArgs e)
        {
            Unblock();
            AddPatient.Visibility = Visibility.Collapsed;
            SureNameTextBox.Text = null;
            NameTextBox.Text = null;
            LastNameTextBox.Text = null;
            PhoneTextBox.Text = null;
            

        }

        /// <summary>
        /// Переход на страницу с историей посещения
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HistoryClick(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new HistoryPage(activePatient));
        }

        /// <summary>
        /// Открытие окна просмотра аллергий
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CheckAllergyClick(object sender, RoutedEventArgs e)
        {
            Block();
            CheckAllergy.Visibility = Visibility.Visible;
        }

        /// <summary>
        /// Закрытие окна 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AllergyQuit(object sender, RoutedEventArgs e)
        {
            Unblock();
            CheckAllergy.Visibility = Visibility.Collapsed;
        }
        /// <summary>
        /// Открытие окна для редактирования аллергии
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EditAllergyClick(object sender, RoutedEventArgs e)
        {
            Block();
            SelectAllergy.Visibility = Visibility.Visible;
            
        }

        /// <summary>
        /// Сохранение данных об аллергии пациента
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SaveAllergyButton_Click(object sender, RoutedEventArgs e)
        {
            if(AllergyComboBox.SelectedIndex == 0)
            {
                foreach(var i in activePatAlle)
                {
                    db.context.PatAlle.Remove(i);
                }    
                db.context.SaveChanges();

                MessageBox.Show("Информания об аллергиях пациента обнулена");
            }
            else
            {
                
                if (activePatAlle.Where(x => x.PatientID == activePatient.IDPatient && x.AllergyID == Convert.ToInt32(AllergyComboBox.SelectedValue)).Count() == 0)
                {
                    if (PatientsViewModel.SaveAllergy(activePatient.IDPatient, Convert.ToInt32(AllergyComboBox.SelectedValue)))
                    {
                        MessageBox.Show("Информация об аллергии успешно сохранена!");
                    }
                    else
                    {
                        MessageBox.Show("Информация не сохранена, что-то пошло не по плану!");
                    }
                }
                else
                {
                    MessageBox.Show("Такой тип аллергии уже имеется у пациента");
                }
            }
            UpdateAlle();
            
        }

        /// <summary>
        /// Закрытие окна для редактирования аллергии
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CloseAllergyButton_Click(object sender, RoutedEventArgs e)
        {
            Unblock();
            SelectAllergy.Visibility = Visibility.Collapsed;
        }
        private void ExtractButtonClick(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new ExtractPatientPage(activePatient));
        }

        private void SaveConclusionButtonClick(object sender, RoutedEventArgs e)
        {
            if (PatientsViewModel.SaveConclusion(ConclusionTextBox.Text, activePatient.Surname, activePatient.Name, activePatient.MiddleName, db) && PatientsViewModel.DeletePatient(item, arraySchedule, db))
            {
                MessageBox.Show("Заключение успешно сахранено!");
                MessageBox.Show("Пациент выписан!");
                ConclusionGrid.Visibility = Visibility.Collapsed;
                ConclusionTextBox.Text = null;
                Unblock();
                Update();

            }
            else
            {
                MessageBox.Show("Заключение не сохранено");
            }
            
        }
        
        private void BackButtonClick(object sender, RoutedEventArgs e)
        {
            ConclusionGrid.Visibility = Visibility.Collapsed;
            Unblock();
        }
    }
}
