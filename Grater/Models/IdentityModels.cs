using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Grater.Models

{
    public class CustomUserRole : IdentityUserRole<int> { }
    public class CustomUserClaim : IdentityUserClaim<int> { }
    public class CustomUserLogin : IdentityUserLogin<int> { }

    public class CustomRole : IdentityRole<int, CustomUserRole>
    {
        public CustomRole() { }
        public CustomRole(string name) { Name = name; }
    }

    public class CustomUserStore : UserStore<ApplicationUser, CustomRole, int,
        CustomUserLogin, CustomUserRole, CustomUserClaim>
    {
        public CustomUserStore(ApplicationDbContext context)
            : base(context)
        {
        }
    }

    public class CustomRoleStore : RoleStore<CustomRole, int, CustomUserRole>
    {
        public CustomRoleStore(ApplicationDbContext context)
            : base(context)
        {
        }
    }
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser<int, CustomUserLogin, CustomUserRole, CustomUserClaim>
    {
        public int ExId { get; internal set; }
    
    //    public int TherapistId { get; set; }
        public virtual Therapist Therapist { get; set; }
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser, int> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, CustomRole,int, CustomUserLogin, CustomUserRole, CustomUserClaim>
    {
        public DbSet<Therapist> Therapists { get; set; }
    //    public DbSet <User> Seekers { get; set; }   //proba, poniewaz przy user dziealy sie dziwne rzeczy
        public DbSet <Skill> Skills { get; set; }
        public DbSet <Comment> Comments { get; set; }
        public DbSet <Salon> Salons { get; set; }
        public DbSet<ArticleComment> ArticleComments { get; set; }
        public object Images { get; internal set; }
      
        public ApplicationDbContext()
            : base("AzureTest11")
        {
            
        }

        /* bylo, zmienine 3 czerwca
                public ApplicationDbContext()
            : base("ChangedId", throwIfV1Schema: false)
        {
            
        }
        */

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }


        public System.Data.Entity.DbSet<Grater.Models.Article> Articles { get; set; }

        public System.Data.Entity.DbSet<Grater.Models.Job> Jobs { get; set; }

        public System.Data.Entity.DbSet<Grater.Models.Event> Events { get; set; }

        public System.Data.Entity.DbSet<Grater.Models.Finder> Finders { get; set; }

        public System.Data.Entity.DbSet<Grater.Models.BeautyArticle> BeautyArticles { get; set; }
    }
}
