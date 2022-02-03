using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Migrations
{
    public partial class createRoleeeaakaaaaa : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TB_M_AccountRole_TB_M_Account_AccountNIK",
                table: "TB_M_AccountRole");

            migrationBuilder.DropForeignKey(
                name: "FK_TB_M_AccountRole_TB_M_Role_RoleID",
                table: "TB_M_AccountRole");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TB_M_AccountRole",
                table: "TB_M_AccountRole");

            migrationBuilder.DropIndex(
                name: "IX_TB_M_AccountRole_AccountNIK",
                table: "TB_M_AccountRole");

            migrationBuilder.DropColumn(
                name: "AccountNIK",
                table: "TB_M_AccountRole");

            migrationBuilder.AlterColumn<string>(
                name: "RoleID",
                table: "TB_M_AccountRole",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_TB_M_AccountRole",
                table: "TB_M_AccountRole",
                columns: new[] { "NIK", "RoleID" });

            migrationBuilder.AddForeignKey(
                name: "FK_TB_M_AccountRole_TB_M_Account_NIK",
                table: "TB_M_AccountRole",
                column: "NIK",
                principalTable: "TB_M_Account",
                principalColumn: "NIK",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TB_M_AccountRole_TB_M_Role_RoleID",
                table: "TB_M_AccountRole",
                column: "RoleID",
                principalTable: "TB_M_Role",
                principalColumn: "RoleId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TB_M_AccountRole_TB_M_Account_NIK",
                table: "TB_M_AccountRole");

            migrationBuilder.DropForeignKey(
                name: "FK_TB_M_AccountRole_TB_M_Role_RoleID",
                table: "TB_M_AccountRole");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TB_M_AccountRole",
                table: "TB_M_AccountRole");

            migrationBuilder.AlterColumn<string>(
                name: "RoleID",
                table: "TB_M_AccountRole",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<string>(
                name: "AccountNIK",
                table: "TB_M_AccountRole",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_TB_M_AccountRole",
                table: "TB_M_AccountRole",
                column: "NIK");

            migrationBuilder.CreateIndex(
                name: "IX_TB_M_AccountRole_AccountNIK",
                table: "TB_M_AccountRole",
                column: "AccountNIK");

            migrationBuilder.AddForeignKey(
                name: "FK_TB_M_AccountRole_TB_M_Account_AccountNIK",
                table: "TB_M_AccountRole",
                column: "AccountNIK",
                principalTable: "TB_M_Account",
                principalColumn: "NIK",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TB_M_AccountRole_TB_M_Role_RoleID",
                table: "TB_M_AccountRole",
                column: "RoleID",
                principalTable: "TB_M_Role",
                principalColumn: "RoleId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
