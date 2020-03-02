using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace JobApply.Migrations
{
    public partial class seed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "SalaryDescription",
                table: "JobOffers",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Location",
                table: "JobOffers",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "JobTitle",
                table: "JobOffers",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "JobDescription",
                table: "JobOffers",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ContractLength",
                table: "JobOffers",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CompanyName",
                table: "JobOffers",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "PhoneNumber",
                table: "JobApplications",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "LastName",
                table: "JobApplications",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "FirstName",
                table: "JobApplications",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "EmailAddress",
                table: "JobApplications",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Companies",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Country",
                table: "Companies",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ContactEmail",
                table: "Companies",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "City",
                table: "Companies",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "Companies",
                columns: new[] { "Id", "City", "ContactEmail", "Country", "FoundationDate", "Name" },
                values: new object[,]
                {
                    { 2551, "Washington", "nasa@mail.com", "United States", new DateTime(1958, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "NASA" },
                    { 3551, "Warsaw", "it@company.com", "Poland", new DateTime(2015, 1, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "IT Company" },
                    { 4551, "Warsaw", "job@reserved.pl", "Poland", new DateTime(1958, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Reserved" }
                });

            migrationBuilder.InsertData(
                table: "JobOffers",
                columns: new[] { "Id", "ApplicationDeadline", "CompanyName", "ContractLength", "Created", "JobDescription", "JobTitle", "Location", "SalaryDescription", "SalaryFrom", "SalaryTo", "WorkStartDate" },
                values: new object[,]
                {
                    { 2546, new DateTime(2020, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Kitchen", "1 year", new DateTime(2020, 1, 14, 23, 46, 14, 431, DateTimeKind.Local), "Polish cuisine.", "Cook", "Warsaw, Poland", "PLN/hour", 100, 200, new DateTime(2020, 3, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 3549, new DateTime(2020, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "NASA", "1 year", new DateTime(2020, 1, 14, 23, 46, 14, 431, DateTimeKind.Local), "Space travel.", "Astronaut", "Washington D.C.,United States", "US/travel", 10000, 20000, new DateTime(2020, 3, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 4549, new DateTime(2020, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "IT Company", "2 years", new DateTime(2020, 1, 14, 23, 46, 14, 431, DateTimeKind.Local), "ASP.NET core web applications.", "C# developer", "Cracow, Poland", "PLN/month", 4000, 5000, new DateTime(2020, 3, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.CreateIndex(
                name: "IX_JobOffers_Id",
                table: "JobOffers",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Companies_Id",
                table: "Companies",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_JobOffers_Id",
                table: "JobOffers");

            migrationBuilder.DropIndex(
                name: "IX_Companies_Id",
                table: "Companies");

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "Id",
                keyValue: 2551);

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "Id",
                keyValue: 3551);

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "Id",
                keyValue: 4551);

            migrationBuilder.DeleteData(
                table: "JobOffers",
                keyColumn: "Id",
                keyValue: 2546);

            migrationBuilder.DeleteData(
                table: "JobOffers",
                keyColumn: "Id",
                keyValue: 3549);

            migrationBuilder.DeleteData(
                table: "JobOffers",
                keyColumn: "Id",
                keyValue: 4549);

            migrationBuilder.AlterColumn<string>(
                name: "SalaryDescription",
                table: "JobOffers",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "Location",
                table: "JobOffers",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "JobTitle",
                table: "JobOffers",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "JobDescription",
                table: "JobOffers",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "ContractLength",
                table: "JobOffers",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "CompanyName",
                table: "JobOffers",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "PhoneNumber",
                table: "JobApplications",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "LastName",
                table: "JobApplications",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "FirstName",
                table: "JobApplications",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "EmailAddress",
                table: "JobApplications",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Companies",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "Country",
                table: "Companies",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "ContactEmail",
                table: "Companies",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "City",
                table: "Companies",
                nullable: true,
                oldClrType: typeof(string));
        }
    }
}
