namespace Grater.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatedalon : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Salons", "SalonName", c => c.String());
            DropColumn("dbo.Salons", "Name");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Salons", "Name", c => c.String());
            DropColumn("dbo.Salons", "SalonName");
        }
    }
}
