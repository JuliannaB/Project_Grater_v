namespace Grater.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedbart : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Articles", "Genre", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Articles", "Genre");
        }
    }
}
