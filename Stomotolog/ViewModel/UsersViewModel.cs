using Stomotolog.Model;
using StomotologLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Stomotolog.ViewModel
{
    public class UsersViewModel
    {
        static Core db = new Core();

        /// <summary>
        /// Логика авторизации пользователя
        /// </summary>
        /// <param name="login"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public static bool AuthUser(string login, string password)
        {
            List<Users> arrayUsers;
            arrayUsers = db.context.Users.ToList();
            int countRecord = arrayUsers.Where(x => x.Login == login && x.Password == password).Count();
            if (countRecord == 1)
            {
                return true;
            }
            else
            {
                return false;
            }     
        }

        /// <summary>
        /// Регистрация и запись данных о пользователе
        /// </summary>
        /// <param name="surname"></param>
        /// <param name="name"></param>
        /// <param name="lastname"></param>
        /// <param name="login"></param>
        /// <param name="password"></param>
        /// <param name="password2"></param>
        /// <returns></returns>
        public static bool RegUser(string surname,  string name,  string lastname, string login, string password, string password2)
        {
            try
            {
                if (Сhecks.EmptyString(login, password))
                {
                    if (password == password2)
                    {
                        Users registr = new Users()
                        {
                            Password = password,
                            Login = login,
                            Name = name,
                            Surname = surname,
                            MiddleName = lastname
                        };
                        db.context.Users.Add(registr);
                        db.context.SaveChanges();
                        return true;

                    }
                    else
                    {
                        return false;
                    }

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
        /// Получение ФИО залогиненного пользователя
        /// </summary>
        /// <param name="login"></param>
        /// <returns></returns>
        public static string GetName(string login)
        {
            List<Users> users = db.context.Users.Where(x => x.Login == login).ToList();
            string firstName = "";
            string lastName = "";
            if (users.Count > 0)
            {
                foreach (Stomotolog.Model.Users str in users)
                {
                    firstName = str.Name;
                    lastName = str.Surname;
                }
            }
            return lastName + " " +  firstName;
        }

        /// <summary>
        /// Выбор данных в зависимости от авторизованного пользователя
        /// </summary>
        /// <param name="login"></param>
        /// <param name="password"></param>
        public static void SetNewActiveUser(string login, string password)
        {
            App.ActiveUser = db.context.Users.Where(x => x.Login == login && x.Password == password).FirstOrDefault();
        }
    }
}
