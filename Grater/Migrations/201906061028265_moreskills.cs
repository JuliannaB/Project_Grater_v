namespace Grater.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class moreskills : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO SKILLS (SkillName) VALUES('Face Treatments')");
            Sql("INSERT INTO SKILLS (SkillName) VALUES('Waxing')");
            Sql("INSERT INTO SKILLS (SkillName) VALUES('Eyebrow Artist')");
            Sql("INSERT INTO SKILLS (SkillName) VALUES('Body Treatments')");

        }
        
        public override void Down()
        {
        }
    }
}
