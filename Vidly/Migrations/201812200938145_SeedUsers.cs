namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class SeedUsers : DbMigration
    {
        public override void Up()
        {

            Sql(@"
INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'78298da2-9393-4742-bf14-053675a5b143', N'admin@vidly.com', 0, N'AO99mQMoCi0ZJ2UlS/GhBd3v8i0qnhjcRlj+gwEeMJZvXTvJew20/fuwIrk9E4pyYQ==', N'141f51e4-9e98-4c42-bbbe-17221ebcf9f6', NULL, 0, 0, NULL, 1, 0, N'admin@vidly.com')
INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'f9ef75e4-4c56-419b-9846-2b6853b87715', N'guest@Vidly.com', 0, N'ADdssbK0+dgC115Sjaq0ojnk5uPWkQTZ9vgxXiKfZrzmAU6+GyF6s/UpMLHQapy4uw==', N'6e86d78c-cc7b-4db4-b3ad-c75de03acd8e', NULL, 0, 0, NULL, 1, 0, N'guest@Vidly.com')

INSERT INTO [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'1b34564e-6d6d-4639-afef-c2d0cc1549e0', N'CanManageMovies')


INSERT INTO [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'78298da2-9393-4742-bf14-053675a5b143', N'1b34564e-6d6d-4639-afef-c2d0cc1549e0')

");
        }

        public override void Down()
        {
        }
    }
}
