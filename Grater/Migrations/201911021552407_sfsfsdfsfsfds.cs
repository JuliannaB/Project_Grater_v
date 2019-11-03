namespace Grater.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class sfsfsdfsfsfds : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO SALONS (City,SalonName, Address) VALUES('Dublin', 'Heaven Day Spa', '189 Rathmines Rd Lower')");
            Sql("INSERT INTO SALONS (City,SalonName, Address) VALUES('Dublin', 'The Buff Day Spa', '52 King St S, Dublin 2, D02 HP68')");
        }
        
        public override void Down()
        {
        }
    }
}
