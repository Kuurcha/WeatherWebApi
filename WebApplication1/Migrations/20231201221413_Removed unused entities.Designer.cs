﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using WebWeatherApi.Entities.ModelConfiguration;

#nullable disable

namespace WebWeatherApi.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20231201221413_Removed unused entities")]
    partial class Removedunusedentities
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.25")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("WebWeatherApi.Entities.Model.WeatherRecord", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int?>("CloudBase")
                        .HasColumnType("integer");

                    b.Property<int?>("Cloudiness")
                        .HasColumnType("integer");

                    b.Property<DateTimeOffset>("Date")
                        .HasColumnType("timestamp with time zone");

                    b.Property<double?>("DewPoint")
                        .HasColumnType("double precision");

                    b.Property<int?>("Humidty")
                        .HasColumnType("integer");

                    b.Property<double?>("Pressure")
                        .HasColumnType("double precision");

                    b.Property<string>("Temperature")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int?>("Visibility")
                        .HasColumnType("integer");

                    b.Property<int?>("WeatherRecordDetailsId")
                        .HasColumnType("integer");

                    b.Property<string>("WindDirection")
                        .HasColumnType("text");

                    b.Property<int?>("WindSpeed")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("WeatherRecordDetailsId");

                    b.ToTable("WeatherRecord");
                });

            modelBuilder.Entity("WebWeatherApi.Entities.Model.WeatherRecordDetails", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("WeatherRecordDetails");
                });

            modelBuilder.Entity("WebWeatherApi.Entities.Model.WeatherRecord", b =>
                {
                    b.HasOne("WebWeatherApi.Entities.Model.WeatherRecordDetails", "WeatherRecordDetails")
                        .WithMany("WeatherRecords")
                        .HasForeignKey("WeatherRecordDetailsId");

                    b.Navigation("WeatherRecordDetails");
                });

            modelBuilder.Entity("WebWeatherApi.Entities.Model.WeatherRecordDetails", b =>
                {
                    b.Navigation("WeatherRecords");
                });
#pragma warning restore 612, 618
        }
    }
}
