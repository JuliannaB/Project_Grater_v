using Microsoft.VisualStudio.TestTools.UnitTesting;
using Grater.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Grater.Models;
using System.Web.Mvc;

namespace Grater.Controllers.Tests
{
    [TestClass()]
    public class CommentControllerTests
    {
        [TestMethod()]
        public void AddTest()
        { 
        Comment c = new Comment() { TherapistId = 1, CommentContent = "to test comment content", Rating = 5, CommentTitle = "Title for test" };
            Assert.AreEqual(c.TherapistId, 1);
            Assert.AreEqual(c.CommentContent, "to test comment content");
            Assert.AreEqual(c.Rating, 5);
            Assert.AreEqual(c.CommentTitle, "Title for test");
        }
        [TestMethod()]
        public void IndexTest()
        {

            // Arrange
            TherapistController controller = new TherapistController();

            // Act
            var editorViewModel = new TherapistController();


            // Assert
            Assert.IsNotNull(editorViewModel);


        }
        [TestMethod()]

        public void AddSalonTest()
        {
            Salon s = new Salon() { Id = 1, City = "Dublin", Address = "Atlantic City", SalonName = "Beuty you" };
            Assert.AreEqual(s.Id, 1);
            Assert.AreEqual(s.City, "Dublin");
            Assert.AreEqual(s.Address, "Atlantic City");
            Assert.AreEqual(s.SalonName, "Beuty you");
        }
        /*[TestMethod()]
         * }
         public void AddTestFakeData()  //Fake data, supposed to failed
        {
            Comment c = new Comment() { TherapistId = 1, CommentContent = "to test comment content", Rating = 5, CommentTitle = "Title for test" };
            Assert.AreEqual(c.TherapistId, 2);
            Assert.AreEqual(c.CommentContent, "Fake data test");
            Assert.AreEqual(c.Rating, 5);
            Assert.AreEqual(c.CommentTitle, "Title for test");
        } */
    }
}