using Microsoft.VisualStudio.TestTools.UnitTesting;
using StomotologLibrary;
using System;


namespace StomotologTestProject
{
    [TestClass]
    public class SurnameNameCheck
    {
        /// <summary>
        /// Тестирует две обычных строки
        /// </summary>
        [TestMethod]
        public void EmptyString_NotEmptyString_true()
        {
            //Arrange
            string name = "sefsefsef";
            string surname = "awdafsf";
            


            //Act
            bool actual = Сhecks.EmptyString(surname, name);

            //Assert
            Assert.IsTrue(actual);

        }
        /// <summary>
        /// Тестирует одну обычную строку и одну пустую
        /// </summary>
        [TestMethod]
        public void EmptyString_OneEmptyString_false()
        {
            //Arrange
            string name = "sefsefsef";
            string surname = string.Empty;
            


            //Act
            bool actual = Сhecks.EmptyString(surname, name);

            //Assert
            Assert.IsFalse(actual);

        }
        /// <summary>
        /// Тестирует две пустых строки
        /// </summary>
        [TestMethod]
        public void EmptyString_EmptyStrings_false()
        {
            //Arrange
            string name = string.Empty;
            string surname = string.Empty;
            


            //Act
            bool actual = Сhecks.EmptyString(surname, name);

            //Assert
            Assert.IsFalse(actual);

        }
        /// <summary>
        /// Тестирует одну обычную строку и одну строку с пробелами
        /// </summary>
        [TestMethod]
        public void EmptyString_SpaseString_false()
        {
            //Arrange
            string name = "sefsefsf";
            string surname = "          ";



            //Act
            bool actual = Сhecks.EmptyString(surname, name);

            //Assert
            Assert.IsFalse(actual);

        }
        /// <summary>
        /// Тестирует одну пустую строку и одну строку с пробелами
        /// </summary>
        [TestMethod]
        public void EmptyString_SpaseANDEmtyString_false()
        {
            //Arrange
            string name = string.Empty;
            string surname = "          ";



            //Act
            bool actual = Сhecks.EmptyString(surname, name);

            //Assert
            Assert.IsFalse(actual);

        }
    }
}
