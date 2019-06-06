namespace Grater.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dsda : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO SKILLS (SkillName) VALUES('Nails')");
            Sql("INSERT INTO SKILLS (SkillName) VALUES('Makeup')");
            Sql("INSERT INTO SKILLS (SkillName) VALUES('Massages')");


        }

        public override void Down()
        {
        }
    }
}
