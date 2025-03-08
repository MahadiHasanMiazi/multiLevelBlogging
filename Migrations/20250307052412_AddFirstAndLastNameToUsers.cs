using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Multi_Level_Blogging_System.Migrations
{
    /// <inheritdoc />
    public partial class AddFirstAndLastNameToUsers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Email",
                schema: "multiLevelBlogging",
                table: "Users",
                type: "character varying(256)",
                maxLength: 256,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "character varying(256)",
                oldMaxLength: 256,
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ConfirmPassword",
                schema: "multiLevelBlogging",
                table: "Users",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                schema: "multiLevelBlogging",
                table: "Users",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                schema: "multiLevelBlogging",
                table: "Users",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Password",
                schema: "multiLevelBlogging",
                table: "Users",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "UserType",
                schema: "multiLevelBlogging",
                table: "Users",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ConfirmPassword",
                schema: "multiLevelBlogging",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "FirstName",
                schema: "multiLevelBlogging",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "LastName",
                schema: "multiLevelBlogging",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Password",
                schema: "multiLevelBlogging",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "UserType",
                schema: "multiLevelBlogging",
                table: "Users");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                schema: "multiLevelBlogging",
                table: "Users",
                type: "character varying(256)",
                maxLength: 256,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(256)",
                oldMaxLength: 256);
        }
    }
}
