using Microsoft.VisualStudio.TestTools.UnitTesting;
using Grater.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Grater.Models;

namespace Grater.Controllers.Tests
{
    [TestClass()]
    public class EventControllerTests
    {
        [TestMethod()]
        public void CreateTest()
        {
            Event e = new Event() { ArticleId = 1,  ArticleBody = "About Mvc Applicationns", ArticleTitle = "Why me?", EventCity = "Dublin",WhoAdd = "Sony@gmail.com" };
            Assert.IsInstanceOfType(e, typeof(Event));
            Assert.AreEqual(e.ArticleId, 1);          
            Assert.AreEqual(e.ArticleBody, "About Mvc Applicationns");
            Assert.AreEqual(e.ArticleTitle, "Why me?");
            Assert.AreEqual(e.EventCity, "Dublin");
            Assert.AreEqual(e.WhoAdd, "Sony@gmail.com");
          
        }

        [TestMethod()]
        public void CreateFinderTest()
        {
            Finder f = new Finder() { ArticleId = 2, ArticleBody = "Great beauty Mvc Applicationns", ArticleTitle = "abuut B?",  WhatSpeciality="Make up artist", WhoAdd = "artem@gmail.com" };
            Assert.IsInstanceOfType(f, typeof(Finder));
            Assert.AreEqual(f.ArticleId, 2);
            Assert.AreEqual(f.ArticleBody, "Great beauty Mvc Applicationns");
            Assert.AreEqual(f.ArticleTitle, "abuut B?");
            Assert.AreEqual(f.WhatSpeciality, "Make up artist");
            Assert.AreEqual(f.WhoAdd, "artem@gmail.com");

        }
    }
}