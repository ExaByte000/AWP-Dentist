using Microsoft.VisualStudio.TestTools.UnitTesting;
using StomotologLibrary;
using System;


namespace StomotologTestProject
{
    [TestClass]
    public class TimeCheck
    {
        /// <summary>
        /// Тестирует две обычные строки
        /// </summary>
        [TestMethod]
        public void EmptyDateTime_NoNull_true()
        {
            //Arrange
            DateTime date = Convert.ToDateTime("2000 - 05 - 23, 12:00:00");
            string time = "12:00";



            //Act
            bool actual = Сhecks.EmptyDateTime(date, time);

            //Assert
            Assert.IsTrue(actual);

        }

        /// <summary>
        /// Тестирует одну обычную строку и одну строку со значением Null
        /// </summary>
        [TestMethod]
        public void EmptyDateTime_OneNull_true()
        {
            //Arrange
            DateTime date = Convert.ToDateTime(null);
            string time = "12:00";



            //Act
            bool actual = Сhecks.EmptyDateTime(date, time);

            //Assert
            Assert.IsTrue(actual);

        }
        /// <summary>
        /// Тестирует все строки со значением Null
        /// </summary>
        [TestMethod]
        public void EmptyDateTime_AllNull_false()
        {
            //Arrange
            DateTime date = Convert.ToDateTime(null);
            string time = null;



            //Act
            bool actual = Сhecks.EmptyDateTime(date, time);

            //Assert
            Assert.IsFalse(actual);

        }

        /// <summary>
        /// Тестирует нормальную строку с датой и строку с пробелаи
        /// </summary>
        [TestMethod]
        public void EmptyDateTime_StringSpase_false()
        {
            //Arrange
            DateTime date = Convert.ToDateTime("2000 - 05 - 23, 12:00:00");
            string time = "                  ";



            //Act
            bool actual = Сhecks.EmptyDateTime(date, time);

            //Assert
            Assert.IsFalse(actual);

        }

        /// <summary>
        /// Тестирует нормальную строку с датой и строку со значением Null
        /// </summary>
        [TestMethod]
        public void EmptyDateTime_StringNull_false()
        {
            //Arrange
            DateTime date = Convert.ToDateTime("2000 - 05 - 23, 12:00:00");
            string time = null;



            //Act
            bool actual = Сhecks.EmptyDateTime(date, time);

            //Assert
            Assert.IsFalse(actual);

        }
    }
}
