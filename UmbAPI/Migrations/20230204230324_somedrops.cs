using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UmbAPI.Migrations
{
    /// <inheritdoc />
    public partial class somedrops : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_EmployeeTypes_EmployeeTypeID",
                table: "Employees");

            migrationBuilder.DropTable(
                name: "EmployeeTypes");

            migrationBuilder.DropIndex(
                name: "IX_Employees_EmployeeTypeID",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "EmployeeTypeID",
                table: "Employees");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EmployeeTypeID",
                table: "Employees",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "EmployeeTypes",
                columns: table => new
                {
                    EmployeeTypeID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmpType = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeTypes", x => x.EmployeeTypeID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Employees_EmployeeTypeID",
                table: "Employees",
                column: "EmployeeTypeID");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_EmployeeTypes_EmployeeTypeID",
                table: "Employees",
                column: "EmployeeTypeID",
                principalTable: "EmployeeTypes",
                principalColumn: "EmployeeTypeID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
