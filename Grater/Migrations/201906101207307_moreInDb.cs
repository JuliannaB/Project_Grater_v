namespace Grater.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class moreInDb : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO SKILLS (SkillName) VALUES('Microblade')");
            Sql("INSERT INTO SKILLS (SkillName) VALUES('Permanent MakeUp')");
            Sql("INSERT INTO SKILLS (SkillName) VALUES('Indian Hopi candles')");

            Sql("INSERT INTO SALONS (City, SalonName, Address) VALUES('Dublin', 'Havana', 'Phitsborow 893, D')");
            Sql("INSERT INTO SALONS (City, SalonName, Address) VALUES('Galway', 'Oasis', 'St. Patric Avenue, 45D')");
            Sql("INSERT INTO SALONS (City, SalonName, Address) VALUES('Cork', 'Cosines', 'Marion Sqr, 99k')");
        }

        public override void Down()
        {
        }
    }
}
