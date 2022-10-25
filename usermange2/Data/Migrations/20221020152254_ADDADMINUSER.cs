using Microsoft.EntityFrameworkCore.Migrations;

namespace usermange2.Data.Migrations
{
    public partial class ADDADMINUSER : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO [security].[Users] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount], [Firsrname], [LastName], [profilepicture]) VALUES (N'46cca472-ab72-49ea-9458-8354aec1c224', N'ADMIN', N'ADMIN', N'ADMIN@gmail.com', N'ADMIN@GMAIL.COM', 0, N'AQAAAAEAACcQAAAAEN594WsXPEr3j6qt2q3qezeumfQ8ZP1x0W/1/JxYsex2ZRKUH6f6U7OAdsFW+5Ry1w==', N'YGGMRELJOKS5E3NQHP3CBVHYJTAKNTNZ', N'80948628-784d-4fbd-8cab-b93c99c9f838', NULL, 0, 0, NULL, 1, 0, N'AHMEDSALMAN', N'SALMAN', NULL)");

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM [security].[Users] WHERE Id='f321269f-f30d-4465-904b-1c1466702d76'");
        }
    }
}
