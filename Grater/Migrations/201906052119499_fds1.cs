namespace Grater.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fds1 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Skills", "Therapist_TherapistId", "dbo.Therapists");
            DropIndex("dbo.Skills", new[] { "Therapist_TherapistId" });
            CreateTable(
                "dbo.SkillTherapists",
                c => new
                    {
                        Skill_SkillId = c.Int(nullable: false),
                        Therapist_TherapistId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Skill_SkillId, t.Therapist_TherapistId })
                .ForeignKey("dbo.Skills", t => t.Skill_SkillId, cascadeDelete: true)
                .ForeignKey("dbo.Therapists", t => t.Therapist_TherapistId, cascadeDelete: true)
                .Index(t => t.Skill_SkillId)
                .Index(t => t.Therapist_TherapistId);
            
            DropColumn("dbo.Skills", "Therapist_TherapistId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Skills", "Therapist_TherapistId", c => c.Int());
            DropForeignKey("dbo.SkillTherapists", "Therapist_TherapistId", "dbo.Therapists");
            DropForeignKey("dbo.SkillTherapists", "Skill_SkillId", "dbo.Skills");
            DropIndex("dbo.SkillTherapists", new[] { "Therapist_TherapistId" });
            DropIndex("dbo.SkillTherapists", new[] { "Skill_SkillId" });
            DropTable("dbo.SkillTherapists");
            CreateIndex("dbo.Skills", "Therapist_TherapistId");
            AddForeignKey("dbo.Skills", "Therapist_TherapistId", "dbo.Therapists", "TherapistId");
        }
    }
}
