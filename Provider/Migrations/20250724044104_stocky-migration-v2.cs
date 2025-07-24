using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Provider.Migrations
{
    /// <inheritdoc />
    public partial class stockymigrationv2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserRole_Roles_RoleId",
                schema: "usr",
                table: "UserRole");

            migrationBuilder.DropForeignKey(
                name: "FK_UserRole_Users_UserId",
                schema: "usr",
                table: "UserRole");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserRole",
                schema: "usr",
                table: "UserRole");

            migrationBuilder.EnsureSchema(
                name: "per");

            migrationBuilder.RenameTable(
                name: "UserRole",
                schema: "usr",
                newName: "UserRoles",
                newSchema: "usr");

            migrationBuilder.RenameIndex(
                name: "IX_UserRole_RoleId",
                schema: "usr",
                table: "UserRoles",
                newName: "IX_UserRoles_RoleId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserRoles",
                schema: "usr",
                table: "UserRoles",
                columns: new[] { "UserId", "RoleId" });

            migrationBuilder.CreateTable(
                name: "PersonTypes",
                schema: "per",
                columns: table => new
                {
                    PersonTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonTypes", x => x.PersonTypeId);
                });

            migrationBuilder.CreateTable(
                name: "Person",
                schema: "per",
                columns: table => new
                {
                    PersonId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PersonTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Person", x => x.PersonId);
                    table.ForeignKey(
                        name: "FK_Person_PersonTypes_PersonTypeId",
                        column: x => x.PersonTypeId,
                        principalSchema: "per",
                        principalTable: "PersonTypes",
                        principalColumn: "PersonTypeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Person_PersonTypeId",
                schema: "per",
                table: "Person",
                column: "PersonTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserRoles_Roles_RoleId",
                schema: "usr",
                table: "UserRoles",
                column: "RoleId",
                principalSchema: "usr",
                principalTable: "Roles",
                principalColumn: "RoleId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserRoles_Users_UserId",
                schema: "usr",
                table: "UserRoles",
                column: "UserId",
                principalSchema: "usr",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserRoles_Roles_RoleId",
                schema: "usr",
                table: "UserRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_UserRoles_Users_UserId",
                schema: "usr",
                table: "UserRoles");

            migrationBuilder.DropTable(
                name: "Person",
                schema: "per");

            migrationBuilder.DropTable(
                name: "PersonTypes",
                schema: "per");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserRoles",
                schema: "usr",
                table: "UserRoles");

            migrationBuilder.RenameTable(
                name: "UserRoles",
                schema: "usr",
                newName: "UserRole",
                newSchema: "usr");

            migrationBuilder.RenameIndex(
                name: "IX_UserRoles_RoleId",
                schema: "usr",
                table: "UserRole",
                newName: "IX_UserRole_RoleId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserRole",
                schema: "usr",
                table: "UserRole",
                columns: new[] { "UserId", "RoleId" });

            migrationBuilder.AddForeignKey(
                name: "FK_UserRole_Roles_RoleId",
                schema: "usr",
                table: "UserRole",
                column: "RoleId",
                principalSchema: "usr",
                principalTable: "Roles",
                principalColumn: "RoleId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserRole_Users_UserId",
                schema: "usr",
                table: "UserRole",
                column: "UserId",
                principalSchema: "usr",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
