namespace Grater.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedWho : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Comments", "ByWho", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Comments", "ByWho");
        }
    }
}
