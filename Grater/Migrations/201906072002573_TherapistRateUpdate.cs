namespace Grater.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TherapistRateUpdate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Therapists", "RatingAvg", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Therapists", "RatingAvg");
        }
    }
}
