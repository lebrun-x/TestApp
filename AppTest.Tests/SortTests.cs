using System;
using System.Runtime;
using TestApp.App;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace TestApp.Tests
{
    [TestClass]
    public class SortTests
    {
        private Jeu jeu;
        [TestInitialize]
        public void TestInitialisation()
        {
            jeu = new Jeu();
            jeu.LaunchSort();
        }

        [TestMethod]
        public void ListSort_IsInitialize()
        {
            Assert.IsNotNull(jeu.Get_ListSort().ElementAt(1));
        }

        [TestMethod]
        public void Vampimoule_GetDammage_Value5()
        {
            Assert.AreEqual(jeu.Get_ListSort().ElementAt(3).GetDammage(), 5);
        }

        [TestMethod]
        public void Vampimoule_GetCost_Value6()
        {
            Assert.AreEqual(jeu.Get_ListSort().ElementAt(3).GetCost(), 6);
        }
    }
}
