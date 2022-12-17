using Stomotolog.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stomotolog.ViewModel
{
    public class DentalFormViewModel
    {
        private static Core db = new Core();

        /// <summary>
        /// Сохранение информации о зубе в бд
        /// </summary>
        /// <param name="idpat"></param>
        /// <param name="idtooth"></param>
        /// <param name="disc"></param>
        /// <returns></returns>
        public static bool Save(int idpat, int idtooth, string disc)
        {
            try
            { 
                if (!String.IsNullOrWhiteSpace(disc))
                {
                    DentalForm add = new DentalForm()
                    {
                        PatientID = idpat,
                        TeethID = idtooth,
                        Discription = disc
                    };
                    db.context.DentalForm.Add(add);
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
    }
}
