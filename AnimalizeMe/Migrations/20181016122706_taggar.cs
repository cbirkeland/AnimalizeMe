using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace AnimalizeMe.Migrations
{
    public partial class taggar : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Creatures_Type_TypeId",
                table: "Creatures");

            migrationBuilder.DropTable(
                name: "Type");

            migrationBuilder.DropIndex(
                name: "IX_Creatures_TypeId",
                table: "Creatures");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Tag");

            migrationBuilder.DropColumn(
                name: "TypeId",
                table: "Creatures");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Tag",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImagePath",
                table: "Creatures",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "Tag");

            migrationBuilder.DropColumn(
                name: "ImagePath",
                table: "Creatures");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Tag",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TypeId",
                table: "Creatures",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Type",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatureType = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Type", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Creatures_TypeId",
                table: "Creatures",
                column: "TypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Creatures_Type_TypeId",
                table: "Creatures",
                column: "TypeId",
                principalTable: "Type",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
