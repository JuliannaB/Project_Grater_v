namespace Grater.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class salonchange : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.SalonTherapists",
                c => new
                    {
                        Salon_Id = c.Int(nullable: false),
                        Therapist_TherapistId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Salon_Id, t.Therapist_TherapistId })
                .ForeignKey("dbo.Salons", t => t.Salon_Id, cascadeDelete: true)
                .ForeignKey("dbo.Therapists", t => t.Therapist_TherapistId, cascadeDelete: true)
                .Index(t => t.Salon_Id)
                .Index(t => t.Therapist_TherapistId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SalonTherapists", "Therapist_TherapistId", "dbo.Therapists");
            DropForeignKey("dbo.SalonTherapists", "Salon_Id", "dbo.Salons");
            DropIndex("dbo.SalonTherapists", new[] { "Therapist_TherapistId" });
            DropIndex("dbo.SalonTherapists", new[] { "Salon_Id" });
            DropTable("dbo.SalonTherapists");
        }
    }
}
