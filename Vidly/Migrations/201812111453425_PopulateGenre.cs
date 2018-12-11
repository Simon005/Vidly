namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PopulateGenre : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO Genre () VALUES ('Action')");
            Sql("INSERT INTO Genre () VALUES ('Comedy')");
            Sql("INSERT INTO Genre () VALUES ('Family')");
            Sql("INSERT INTO Genre () VALUES ('Action')");


        }

        public override void Down()
        {
        }
    }
}
