namespace Grater.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addByWhoInt : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Comments", "ByWhoIn", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Comments", "ByWhoIn");
        }
    }
}
