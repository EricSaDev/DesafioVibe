﻿// <auto-generated />
using System;
using LoginDemo.Repositorio;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace LoginDemo.Migrations
{
    [DbContext(typeof(PersisteContext))]
    [Migration("20191218202747_LoginDemo_003")]
    partial class LoginDemo_003
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.11-servicing-32099")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("LoginDemo.Models.Cliente", b =>
                {
                    b.Property<string>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("CPF");

                    b.Property<bool>("ESPECIAL");

                    b.Property<string>("NOME");

                    b.Property<string>("USERCPF");

                    b.HasKey("ID");

                    b.HasIndex("USERCPF");

                    b.ToTable("Cliente");
                });

            modelBuilder.Entity("LoginDemo.Models.User", b =>
                {
                    b.Property<string>("CPF")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("APIACCESSTOKEN");

                    b.Property<string>("LOCALACCESSTOKEN");

                    b.Property<DateTime>("NASCIMENTO");

                    b.Property<string>("NOME");

                    b.Property<string>("PERFIL");

                    b.Property<string>("SENHA");

                    b.Property<DateTime>("VALIDADELOGIN");

                    b.HasKey("CPF");

                    b.ToTable("User");
                });

            modelBuilder.Entity("LoginDemo.Models.Cliente", b =>
                {
                    b.HasOne("LoginDemo.Models.User", "USER")
                        .WithMany()
                        .HasForeignKey("USERCPF");
                });
#pragma warning restore 612, 618
        }
    }
}
