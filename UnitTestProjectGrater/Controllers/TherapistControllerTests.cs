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
    public class TherapistControllerTests
    {
        [TestMethod()]
        public void CreateTest()
        {
            Therapist t = new Therapist() { TherapistId = 1, TherapistName = "Aleksandra", TherapistSurname = "McCartney" };
            Assert.AreEqual(t.TherapistId, 1);
            Assert.AreEqual(t.TherapistName, "Aleksandra");
            Assert.AreEqual(t.TherapistSurname, "McCartney");
         //   Assert.Fail();
        }


    }
}