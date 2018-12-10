namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PopulateMovie : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO Movies (Name, Genre, ReleaseDate, DateAdded, NumberInStock) VALUES ('Hangover', 'Comedy', '2001-01-15 10:10:10', '2005-01-15 10:10:10', 10)");
            Sql("INSERT INTO Movies (Name, Genre, ReleaseDate, DateAdded, NumberInStock) VALUES ('Die Hard', 'Action', '2002-01-15 10:10:10', '2006-01-15 10:10:10', 15)");
            Sql("INSERT INTO Movies (Name, Genre, ReleaseDate, DateAdded, NumberInStock) VALUES ('The Terminator', 'Action', '2003-01-15 10:10:10', '2004-01-15 10:10:10', 12)");
            Sql("INSERT INTO Movies (Name, Genre, ReleaseDate, DateAdded, NumberInStock) VALUES ('Toy Story', 'Family', '2005-01-15 10:10:10', '2006-01-15 10:10:10', 22)");
            Sql("INSERT INTO Movies (Name, Genre, ReleaseDate, DateAdded, NumberInStock) VALUES ('Titanic', 'Romance', '2007-01-15 10:10:10', '2008-01-15 10:10:10', 66)");
            Sql("INSERT INTO Movies (Name, Genre, ReleaseDate, DateAdded, NumberInStock) VALUES ('Dumbo', 'Family', '2004-01-15 10:10:10', '2005-01-15 10:10:10', 44)");

        }

        public override void Down()
        {
        }
    }
}
