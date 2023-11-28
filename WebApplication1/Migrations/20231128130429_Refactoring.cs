using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace WebWeatherApi.Migrations
{
    public partial class Refactoring : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WeatherDetails");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "WeatherRecord",
                newName: "Temperature");

            migrationBuilder.AddColumn<int>(
                name: "CloudBase",
                table: "WeatherRecord",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Cloudiness",
                table: "WeatherRecord",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "Date",
                table: "WeatherRecord",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<double>(
                name: "DewPoint",
                table: "WeatherRecord",
                type: "double precision",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Humidty",
                table: "WeatherRecord",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Pressure",
                table: "WeatherRecord",
                type: "double precision",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Visibility",
                table: "WeatherRecord",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "WeatherRecordDetailsId",
                table: "WeatherRecord",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "WindDirection",
                table: "WeatherRecord",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "WindSpeed",
                table: "WeatherRecord",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "WeatherRecordDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Description = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WeatherRecordDetails", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_WeatherRecord_WeatherRecordDetailsId",
                table: "WeatherRecord",
                column: "WeatherRecordDetailsId");

            migrationBuilder.AddForeignKey(
                name: "FK_WeatherRecord_WeatherRecordDetails_WeatherRecordDetailsId",
                table: "WeatherRecord",
                column: "WeatherRecordDetailsId",
                principalTable: "WeatherRecordDetails",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WeatherRecord_WeatherRecordDetails_WeatherRecordDetailsId",
                table: "WeatherRecord");

            migrationBuilder.DropTable(
                name: "WeatherRecordDetails");

            migrationBuilder.DropIndex(
                name: "IX_WeatherRecord_WeatherRecordDetailsId",
                table: "WeatherRecord");

            migrationBuilder.DropColumn(
                name: "CloudBase",
                table: "WeatherRecord");

            migrationBuilder.DropColumn(
                name: "Cloudiness",
                table: "WeatherRecord");

            migrationBuilder.DropColumn(
                name: "Date",
                table: "WeatherRecord");

            migrationBuilder.DropColumn(
                name: "DewPoint",
                table: "WeatherRecord");

            migrationBuilder.DropColumn(
                name: "Humidty",
                table: "WeatherRecord");

            migrationBuilder.DropColumn(
                name: "Pressure",
                table: "WeatherRecord");

            migrationBuilder.DropColumn(
                name: "Visibility",
                table: "WeatherRecord");

            migrationBuilder.DropColumn(
                name: "WeatherRecordDetailsId",
                table: "WeatherRecord");

            migrationBuilder.DropColumn(
                name: "WindDirection",
                table: "WeatherRecord");

            migrationBuilder.DropColumn(
                name: "WindSpeed",
                table: "WeatherRecord");

            migrationBuilder.RenameColumn(
                name: "Temperature",
                table: "WeatherRecord",
                newName: "Description");

            migrationBuilder.CreateTable(
                name: "WeatherDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    WeatherRecordId = table.Column<int>(type: "integer", nullable: true),
                    CloudBase = table.Column<int>(type: "integer", nullable: true),
                    Cloudiness = table.Column<int>(type: "integer", nullable: true),
                    Date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DewPoint = table.Column<double>(type: "double precision", nullable: true),
                    Humidty = table.Column<int>(type: "integer", nullable: true),
                    Pressure = table.Column<double>(type: "double precision", nullable: true),
                    Temperature = table.Column<string>(type: "text", nullable: false),
                    Visibility = table.Column<int>(type: "integer", nullable: true),
                    WindDirection = table.Column<string>(type: "text", nullable: true),
                    WindSpeed = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WeatherDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WeatherDetails_WeatherRecord_WeatherRecordId",
                        column: x => x.WeatherRecordId,
                        principalTable: "WeatherRecord",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_WeatherDetails_WeatherRecordId",
                table: "WeatherDetails",
                column: "WeatherRecordId");
        }
    }
}
