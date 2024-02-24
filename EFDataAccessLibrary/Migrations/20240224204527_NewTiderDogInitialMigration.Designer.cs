﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using NewDogTinder.EFDataAccessLibrary.DataAccess;

#nullable disable

namespace EFDataAccessLibrary.Migrations
{
    [DbContext(typeof(NewDogTinderContext))]
    [Migration("20240224204527_NewTiderDogInitialMigration")]
    partial class NewTiderDogInitialMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "6.0.27");

            modelBuilder.Entity("NewDogTinder.EFDataAccessLibrary.Models.Appointment", b =>
                {
                    b.Property<int>("AppointmentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("DogId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("PlaceId")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("Time")
                        .HasColumnType("TEXT");

                    b.HasKey("AppointmentId");

                    b.HasIndex("DogId");

                    b.HasIndex("PlaceId");

                    b.ToTable("Appointments");
                });

            modelBuilder.Entity("NewDogTinder.EFDataAccessLibrary.Models.Dog", b =>
                {
                    b.Property<int>("DogId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Breed")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("TEXT");

                    b.Property<int>("OwnerId")
                        .HasColumnType("INTEGER");

                    b.HasKey("DogId");

                    b.HasIndex("OwnerId");

                    b.ToTable("Dogs");
                });

            modelBuilder.Entity("NewDogTinder.EFDataAccessLibrary.Models.Owner", b =>
                {
                    b.Property<int>("OwnerId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("TEXT");

                    b.HasKey("OwnerId");

                    b.ToTable("Owners");
                });

            modelBuilder.Entity("NewDogTinder.EFDataAccessLibrary.Models.Place", b =>
                {
                    b.Property<int>("PlaceId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("TEXT");

                    b.HasKey("PlaceId");

                    b.ToTable("Places");
                });

            modelBuilder.Entity("NewDogTinder.EFDataAccessLibrary.Models.Appointment", b =>
                {
                    b.HasOne("NewDogTinder.EFDataAccessLibrary.Models.Dog", "Dog")
                        .WithMany("Appointments")
                        .HasForeignKey("DogId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("NewDogTinder.EFDataAccessLibrary.Models.Place", "Place")
                        .WithMany()
                        .HasForeignKey("PlaceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Dog");

                    b.Navigation("Place");
                });

            modelBuilder.Entity("NewDogTinder.EFDataAccessLibrary.Models.Dog", b =>
                {
                    b.HasOne("NewDogTinder.EFDataAccessLibrary.Models.Owner", "Owner")
                        .WithMany()
                        .HasForeignKey("OwnerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Owner");
                });

            modelBuilder.Entity("NewDogTinder.EFDataAccessLibrary.Models.Dog", b =>
                {
                    b.Navigation("Appointments");
                });
#pragma warning restore 612, 618
        }
    }
}
