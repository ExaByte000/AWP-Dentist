using Stomotolog.Model;
using StomotologLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Stomotolog.ViewModel
{
    public class PatientsViewModel
    {
       private static  Core db = new Core();

        /// <summary>
        /// Занесение данных о пациенте в базу
        /// </summary>
        /// <param name="surname"></param>
        /// <param name="name"></param>
        /// <param name="lastname"></param>
        /// <param name="phone"></param>
        /// <returns></returns>
        public static bool AddPathient(string surname, string name, string lastname, string phone, Core db)
        {


            try
            {
                if (Сhecks.EmptyString(surname, name))
                {
                Patients add = new Patients()
                {
                    Surname = surname,
                    Name = name,
                    MiddleName = lastname,
                    Phone = phone,
                    UserID = App.ActiveUser.IDUser
                };
                db.context.Patients.Add(add);
                db.context.SaveChanges();

                return true;

                }
                else
                {
                    return false;
                }
            }
            catch
            {

                return false;
            }

        }

        

        /// <summary>
        /// Занесение даныых о времени примеа в базу
        /// </summary>
        /// <param name="date"></param>
        /// <param name="time"></param>
        /// <param name="currentPatient"></param>
        /// <returns></returns>
        public static bool EditTime(DateTime date, string time, int currentPatient)
        {
            try
            {
                if (Сhecks.EmptyDateTime(date, time))
                {
                    History data = new History()
                    {
                        Date = date,
                        Time = TimeSpan.Parse(time),
                        PatientID = currentPatient,
                    };
                    
                    Schedule datetime = new Schedule()
                    {
                        Date = date,
                        Time = TimeSpan.Parse(time),
                        PatientID = currentPatient,
                        UserID = App.ActiveUser.IDUser
                    };
                    db.context.Schedule.Add(datetime);
                    db.context.History.Add(data);
                    db.context.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {

                return false;
            }
        }
        /// <summary>
        /// Метод, сортирующий историю посещений
        /// </summary>
        /// <param name="arrayHistory"></param>
        public static List<History> AcceptSort(List<History> arrayHistory, string text, int index)
        {
            if (index == 0)
            {
                if (!string.IsNullOrEmpty(text))
                {
                    arrayHistory = arrayHistory.Where(x => x.Date.ToShortDateString().ToLower().Contains(text.ToLower())).ToList();
                }
                return arrayHistory;
            }
            else if (index == 1)
            {
                if (!string.IsNullOrEmpty(text))
                {
                    arrayHistory = arrayHistory.Where(x => x.Time.ToString().ToLower().Contains(text.ToLower())).ToList();
                }
                return arrayHistory;
            }
            return null;
        }

        /// <summary>
        /// Сортировка выписанных пациентов
        /// </summary>
        /// <param name="arrayExtract"></param>
        /// <param name="text"></param>
        /// <param name="index"></param>
        /// <param name="activePatient"></param>
        /// <returns></returns>
        public static List<Extract> Sort(List<Extract> arrayExtract, string text, int index)
        {
            switch (index)
            {
                case 0:
                    if (!String.IsNullOrEmpty(text))
                    {
                        arrayExtract = arrayExtract.Where(x => x.Name.ToLower().Contains(text.ToLower())).ToList();
                    }
                    return arrayExtract;

                case 1:
                    if (!String.IsNullOrEmpty(text))
                    {
                        arrayExtract = arrayExtract.Where(x => x.Surname.ToLower().Contains(text.ToLower())).ToList();
                    }
                    return arrayExtract;

                case 2:
                    if (!String.IsNullOrEmpty(text))
                    {
                        arrayExtract = arrayExtract.Where(x => x.MiddleName.ToLower().Contains(text.ToLower())).ToList();
                    }
                    return arrayExtract;

                case 3:
                    if (!String.IsNullOrEmpty(text))
                    {
                        arrayExtract = arrayExtract.Where(x => x.Discription.ToLower().Contains(text.ToLower())).ToList();
                    }
                    return arrayExtract;


            }
            return null;
        }
        /// <summary>
        /// Удаление данных из таблицы "Расписание"
        /// </summary>
        /// <param name="selectedButton"></param>
        public static bool Del(Core db,Schedule selectedShedule)
        {
           
            if (selectedShedule!=null)
            {
                db.context.Schedule.Remove(selectedShedule);
                if (db.context.SaveChanges() > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            return false;

        }
        /// <summary>
        /// Сохранение данных об аллергии в бд
        /// </summary>
        /// <param name="IDpat"></param>
        /// <param name="IDalle"></param>
        /// <returns></returns>
        public static bool SaveAllergy(int IDpat, int IDalle)
        {
            try
            {
                PatAlle all = new PatAlle()
                {
                    PatientID = IDpat,
                    AllergyID = IDalle,
                };
                db.context.PatAlle.Add(all);
                db.context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public static bool SaveResultReception(Schedule activePatient, History itemH, Schedule itemS, Core db)
        {
            try
            {

                History data = new History()
                {
                    Date = activePatient.Date,
                    Time = activePatient.Time,
                    PatientID = activePatient.PatientID,
                    Сomplaints = itemH.Сomplaints,
                    Results = itemH.Results,
                    LaborСosts = itemH.LaborСosts,
                    DiagnosisID = itemH.DiagnosisID,
                    Complications = itemH.Complications,
                    Stage = itemH.Stage,
                    ICDcode = itemH.ICDcode,
                };
                Schedule data2 = new Schedule()
                {
                    Date = activePatient.Date,
                    Time = activePatient.Time,
                    PatientID = activePatient.PatientID,
                    Сomplaints = itemS.Сomplaints,
                    Results = itemS.Results,
                    LaborСosts = itemS.LaborСosts,
                    DiagnosisID = itemS.DiagnosisID,
                    Complications = itemS.Complications,
                    Stage = itemS.Stage,
                    ICDcode = itemS.ICDcode,
                    UserID = itemS.UserID
                };
                db.context.History.Remove(itemH);
                db.context.History.Add(data);
                db.context.Schedule.Remove(itemS);
                db.context.Schedule.Add(data2);
                db.context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public static bool SaveConclusion(string text, string surname, string name, string lastname,Core db)
        {
            //try
            //{
                if (!String.IsNullOrWhiteSpace(text))
                {
                    Extract data = new Extract()
                    {
                        Name = name,
                        Surname = surname,
                        MiddleName = lastname,
                        Discription = text,
                    };
                    db.context.Extract.Add(data);
                    db.context.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }
            //}
            //catch
            //{
            //    return false;
            //}
        }
        public static bool DeletePatient(Patients item, List<Schedule> arraySchedule, Core db)
        {
                arraySchedule = db.context.Schedule.Where(x => x.PatientID == item.IDPatient).ToList();
                foreach (Schedule iten in arraySchedule)
                {
                    db.context.Schedule.Remove(iten);
                }
                db.context.Patients.Remove(item);
                db.context.SaveChanges();
                return true;
        }
    }
}
