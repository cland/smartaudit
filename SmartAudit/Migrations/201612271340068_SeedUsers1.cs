namespace SmartAudit.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SeedUsers1 : DbMigration
    {
        public override void Up()
        {
            Sql(@"
                INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'ef37bdf3-f61b-42a7-af84-0ae1c8860f92', N'admin@smartaudit.com', 0, N'AO4KGY5ZKSNR9mcyVXXLbBIiIh4jcwjox8v1Nm2OoGJ7KAUWSzE4QXkhSdLoBuewnA==', N'27e4023c-e452-49e6-acc5-ee82869af7c0', NULL, 0, 0, NULL, 1, 0, N'admin@smartaudit.com')
                INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'f9db5f9c-9d0c-4f5d-b43e-64a66e5ca51f', N'guest@tagumisolutions.com', 0, N'AHoqsQvnF3MiOhCwomfSIuTKqgxheyKEakZbBOdVzdNzj88S/7mE2/ZhNRclzN6FfA==', N'c1e7b03c-1ea7-4372-8045-380ed912f783', NULL, 0, 0, NULL, 1, 0, N'guest@tagumisolutions.com')
                INSERT INTO [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'ef0d7bb0-ef94-4f2e-85c3-cc2d71b85454', N'CanManageDefinitions')
                INSERT INTO [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'ef37bdf3-f61b-42a7-af84-0ae1c8860f92', N'ef0d7bb0-ef94-4f2e-85c3-cc2d71b85454')

            ");
        }
        
        public override void Down()
        {
        }
    }
}
