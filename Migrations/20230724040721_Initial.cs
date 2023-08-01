using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Practise.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Employee",
                columns: table => new
                {
                    EmpID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EmpName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EmpAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EmpPhone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmpSalary = table.Column<int>(type: "int", nullable: false),
                    EmpDesignation = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employee", x => x.EmpID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Employee");
        }
    }
}
