// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ProfitHeal_API.Data;

#nullable disable

namespace ProfitHealAPI.Migrations
{
    [DbContext(typeof(ProfitHealContext))]
    [Migration("20221105125144_Symptom categories added")]
    partial class Symptomcategoriesadded
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("ProfitHeal_API.Models.ReportModels.Report", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Reports");
                });

            modelBuilder.Entity("ProfitHeal_API.Models.ReportModels.Symptom", b =>
                {
                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("CategoryName")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<int?>("ReportId")
                        .HasColumnType("int");

                    b.HasKey("Name");

                    b.HasIndex("CategoryName");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.HasIndex("ReportId");

                    b.ToTable("Symptoms");
                });

            modelBuilder.Entity("ProfitHeal_API.Models.ReportModels.SymptomCategory", b =>
                {
                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Name");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("SymptomCategories");

                    b.HasData(
                        new
                        {
                            Name = "Digestion"
                        },
                        new
                        {
                            Name = "Respiration"
                        },
                        new
                        {
                            Name = "Dermis"
                        },
                        new
                        {
                            Name = "Nerves"
                        },
                        new
                        {
                            Name = "Pain"
                        },
                        new
                        {
                            Name = "Senses"
                        },
                        new
                        {
                            Name = "Other"
                        });
                });

            modelBuilder.Entity("ProfitHeal_API.Models.UserModels.LoginCredentials", b =>
                {
                    b.Property<string>("Username")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Username");

                    b.HasIndex("Username")
                        .IsUnique();

                    b.ToTable("LoginCredentials");
                });

            modelBuilder.Entity("ProfitHeal_API.Models.UserModels.Role", b =>
                {
                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Name");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Roles");

                    b.HasData(
                        new
                        {
                            Name = "User"
                        },
                        new
                        {
                            Name = "Admin"
                        });
                });

            modelBuilder.Entity("ProfitHeal_API.Models.UserModels.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LoginCredentialsUsername")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("LoginCredentialsUsername");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("RoleUser", b =>
                {
                    b.Property<string>("RolesName")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("UsersId")
                        .HasColumnType("int");

                    b.HasKey("RolesName", "UsersId");

                    b.HasIndex("UsersId");

                    b.ToTable("RoleUser");
                });

            modelBuilder.Entity("ProfitHeal_API.Models.ReportModels.Report", b =>
                {
                    b.HasOne("ProfitHeal_API.Models.UserModels.User", "User")
                        .WithMany("Reports")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("ProfitHeal_API.Models.ReportModels.Symptom", b =>
                {
                    b.HasOne("ProfitHeal_API.Models.ReportModels.SymptomCategory", "Category")
                        .WithMany()
                        .HasForeignKey("CategoryName")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ProfitHeal_API.Models.ReportModels.Report", null)
                        .WithMany("Symptoms")
                        .HasForeignKey("ReportId");

                    b.Navigation("Category");
                });

            modelBuilder.Entity("ProfitHeal_API.Models.UserModels.User", b =>
                {
                    b.HasOne("ProfitHeal_API.Models.UserModels.LoginCredentials", "LoginCredentials")
                        .WithMany()
                        .HasForeignKey("LoginCredentialsUsername")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("LoginCredentials");
                });

            modelBuilder.Entity("RoleUser", b =>
                {
                    b.HasOne("ProfitHeal_API.Models.UserModels.Role", null)
                        .WithMany()
                        .HasForeignKey("RolesName")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ProfitHeal_API.Models.UserModels.User", null)
                        .WithMany()
                        .HasForeignKey("UsersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ProfitHeal_API.Models.ReportModels.Report", b =>
                {
                    b.Navigation("Symptoms");
                });

            modelBuilder.Entity("ProfitHeal_API.Models.UserModels.User", b =>
                {
                    b.Navigation("Reports");
                });
#pragma warning restore 612, 618
        }
    }
}
