﻿// <auto-generated />
using Markitos.Server.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Markitos.Server.Migrations
{
    [DbContext(typeof(StoryDB))]
    partial class StoryDBModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.1");

            modelBuilder.Entity("Markitos.Server.Models.DBStoryModel", b =>
                {
                    b.Property<string>("PostID")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("ShareAnon")
                        .HasColumnType("bit");

                    b.Property<bool>("ShareWithFamOnly")
                        .HasColumnType("bit");

                    b.Property<string>("Story")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("PostID");

                    b.ToTable("Stories");
                });
#pragma warning restore 612, 618
        }
    }
}