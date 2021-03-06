﻿// <auto-generated />
using JointTelegramBot.Web.Data;
using JointTelegramBot.Web.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using System;

namespace JointTelegramBot.Web.Migrations
{
    [DbContext(typeof(JointBotContext))]
    partial class JointBotContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.1-rtm-125")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("JointTelegramBot.Web.Models.Twitter", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("Follow");

                    b.Property<string>("TwitterUserName");

                    b.HasKey("Id");

                    b.ToTable("Twitter");
                });

            modelBuilder.Entity("JointTelegramBot.Web.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("FirstName");

                    b.Property<string>("LastName");

                    b.Property<int>("UserId");

                    b.Property<string>("UserName");

                    b.HasKey("Id");

                    b.ToTable("People");
                });

            modelBuilder.Entity("JointTelegramBot.Web.Models.UserStats", b =>
                {
                    b.Property<int>("StatsId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("RefLink");

                    b.Property<int>("Statu");

                    b.Property<int>("UserId");

                    b.HasKey("StatsId");

                    b.HasIndex("RefLink")
                        .IsUnique();

                    b.HasIndex("UserId");

                    b.ToTable("Stats");
                });

            modelBuilder.Entity("JointTelegramBot.Web.Models.UserStatus", b =>
                {
                    b.Property<int>("StatusId")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("Date");

                    b.Property<string>("LeftChatMember");

                    b.Property<string>("NewChatMember");

                    b.Property<int>("UserId");

                    b.HasKey("StatusId");

                    b.HasIndex("UserId");

                    b.ToTable("Status");
                });

            modelBuilder.Entity("JointTelegramBot.Web.Models.Wallet", b =>
                {
                    b.Property<int>("WalletId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("UserId");

                    b.Property<string>("WalletAddress");

                    b.HasKey("WalletId");

                    b.HasIndex("UserId");

                    b.ToTable("Wallet");
                });

            modelBuilder.Entity("JointTelegramBot.Web.Models.UserStats", b =>
                {
                    b.HasOne("JointTelegramBot.Web.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("JointTelegramBot.Web.Models.UserStatus", b =>
                {
                    b.HasOne("JointTelegramBot.Web.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("JointTelegramBot.Web.Models.Wallet", b =>
                {
                    b.HasOne("JointTelegramBot.Web.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
