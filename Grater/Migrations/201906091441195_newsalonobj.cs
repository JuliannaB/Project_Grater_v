namespace Grater.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class newsalonobj : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO SALONS (City,Name, Address) VALUES('Dublin', 'Urban Day Spa', 'The Glasshouse, 11 Coke Ln, Smithfield, Dublin 7, D07 WNP2')");
            Sql("INSERT INTO SALONS (City,Name, Address) VALUES('Dublin', 'Heaven Day Spa', '189 Rathmines Rd Lower')");
            Sql("INSERT INTO SALONS (City,Name, Address) VALUES('Dublin', 'The Buff Day Spa', '52 King St S, Dublin 2, D02 HP68')");
        }



        public override void Down()
        {
        }

    }
}
