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
        /*[TestMethod()]
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