namespace Grater.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addSkilllsss : DbMigration
    {
        public override void Up()
        {

            Sql("INSERT INTO SKILLS (SkillName) VALUES('Eyebrow Artist')");
            Sql("INSERT INTO SKILLS (SkillName) VALUES('Body Treatments')");
        }
        
        public override void Down()
        {
        }
    }
}
