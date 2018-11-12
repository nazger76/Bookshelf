﻿// <auto-generated />
using System;
using Bookshelf.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Bookshelf.Migrations
{
    [DbContext(typeof(BookshelfContext))]
    [Migration("20181112015754_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.4-rtm-31024");

            modelBuilder.Entity("Bookshelf.Models.Book", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Author")
                        .IsRequired();

                    b.Property<string>("ISBN")
                        .IsRequired();

                    b.Property<int?>("LoanedTo");

                    b.Property<string>("Title")
                        .IsRequired();

                    b.HasKey("ID");

                    b.HasIndex("LoanedTo");

                    b.ToTable("Book");
                });

            modelBuilder.Entity("Bookshelf.Models.Borrower", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("BirthDate");

                    b.Property<string>("FirstName")
                        .IsRequired();

                    b.Property<string>("LastName")
                        .IsRequired();

                    b.HasKey("ID");

                    b.ToTable("Borrower");
                });

            modelBuilder.Entity("Bookshelf.Models.Book", b =>
                {
                    b.HasOne("Bookshelf.Models.Borrower", "Borrower")
                        .WithMany()
                        .HasForeignKey("LoanedTo");
                });
#pragma warning restore 612, 618
        }
    }
}
