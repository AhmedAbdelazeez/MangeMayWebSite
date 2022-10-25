using Microsoft.EntityFrameworkCore.Migrations;

namespace usermange2.Data.Migrations
{
    public partial class ASSIGNADMINTOALLROLELS : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO [security].[userRoles] (UserId,RoleId) SELECT '46cca472-ab72-49ea-9458-8354aec1c224',Id FROM [security].[Roles]");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM [security].[userRoles] WHERE UserId='46cca472-ab72-49ea-9458-8354aec1c224'");

        }
    }
}
