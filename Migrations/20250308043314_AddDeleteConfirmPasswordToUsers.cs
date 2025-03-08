﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Multi_Level_Blogging_System.Migrations
{
    /// <inheritdoc />
    public partial class AddDeleteConfirmPasswordToUsers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ConfirmPassword",
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
        }
    }
}
