using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Grater.Models;


namespace Grater.Controllers.API
{
   /* public class UsersController : ApiController
    {
        private ApplicationDbContext _context = new ApplicationDbContext();

    /*    public UsersController()
        {
            _context = new ApplicationDbContext();
        }   */  /*
        
        //Get / api/users
        public IEnumerable<User> GetUsers()
        {
            return _context.Seekers.ToList();
        }

        //Get/api/users/1
        public User GetUser(int id)
        {
            var user = _context.Seekers.SingleOrDefault(c => c.Id == id);

            if (user == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);
            return user;
        }

        // POST /api/users
        [HttpPost]
        public User CreateUser(User user)
        {
            if (!ModelState.IsValid)
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            _context.Seekers.Add(user);
            _context.SaveChanges();

            return user;
        }

        //PUT /api/user/1
        public void UpdateUser (int id, User user)
        {
            if (!ModelState.IsValid)
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            var userInDb = _context.Seekers.SingleOrDefault(c => c.Id == id);
            if (userInDb == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);
            userInDb.UserName = user.UserName;
            userInDb.UserDescription = user.UserDescription;
            userInDb.UserCity = user.UserCity;

            _context.SaveChanges();

        }

        //DELETE /api/
        [HttpDelete]
        public void DeleteUser(int id)
        {
            var userInDb = _context.Seekers.SingleOrDefault(c => c.Id == id);
            if (userInDb == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);
            _context.Seekers.Remove(userInDb);
            _context.SaveChanges();

        }
    }   */
}
