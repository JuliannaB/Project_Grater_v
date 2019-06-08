namespace Grater.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ratingChanges : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Therapists", "RatingCount", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Therapists", "RatingCount");
        }
    }
}
