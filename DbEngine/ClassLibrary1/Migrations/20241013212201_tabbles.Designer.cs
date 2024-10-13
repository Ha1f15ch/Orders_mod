﻿// <auto-generated />
using System;
using DatabaseContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace DatabaseContext.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20241013212201_tabbles")]
    partial class tabbles
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Models.AssignersRequests", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime?>("DateDeleted")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("OrderId")
                        .HasColumnType("integer");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("OrderId");

                    b.HasIndex("UserId");

                    b.ToTable("AssignersRequests", "dbo");
                });

            modelBuilder.Entity("Models.Order", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Adress")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime?>("DateDeleted")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("DateUpdated")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("OrderPriorityId")
                        .HasColumnType("text");

                    b.Property<string>("OrderStatusId")
                        .HasColumnType("text");

                    b.Property<int>("TimeForWork")
                        .HasColumnType("integer");

                    b.Property<string>("TitleName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int?>("UserIdAssigner")
                        .HasColumnType("integer");

                    b.Property<int>("UserIdCreated")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("OrderPriorityId");

                    b.HasIndex("OrderStatusId");

                    b.HasIndex("UserIdAssigner");

                    b.HasIndex("UserIdCreated");

                    b.ToTable("Order", "dbo");
                });

            modelBuilder.Entity("Models.OrderPriority", b =>
                {
                    b.Property<string>("OrderPriorityId")
                        .HasColumnType("text");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.HasKey("OrderPriorityId");

                    b.ToTable("OrderPriority", "meta");

                    b.HasData(
                        new
                        {
                            OrderPriorityId = "L",
                            Description = "Низкий"
                        },
                        new
                        {
                            OrderPriorityId = "M",
                            Description = "Средний"
                        },
                        new
                        {
                            OrderPriorityId = "H",
                            Description = "Высокий"
                        });
                });

            modelBuilder.Entity("Models.OrderScores", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Comment")
                        .HasColumnType("text");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime?>("DateDeleted")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("DateUpdated")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("OrderId")
                        .HasColumnType("integer");

                    b.Property<int>("Score")
                        .HasColumnType("integer");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("OrderId");

                    b.HasIndex("UserId");

                    b.ToTable("OrderScores", "dbo");
                });

            modelBuilder.Entity("Models.OrderStatus", b =>
                {
                    b.Property<string>("OrderStatusId")
                        .HasColumnType("text");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.HasKey("OrderStatusId");

                    b.ToTable("OrderStatus", "meta");

                    b.HasData(
                        new
                        {
                            OrderStatusId = "N",
                            Description = "Новый"
                        },
                        new
                        {
                            OrderStatusId = "D",
                            Description = "Утверждение иполнителя"
                        },
                        new
                        {
                            OrderStatusId = "S",
                            Description = "Начат"
                        },
                        new
                        {
                            OrderStatusId = "C",
                            Description = "Отменен"
                        },
                        new
                        {
                            OrderStatusId = "X",
                            Description = "Удален"
                        },
                        new
                        {
                            OrderStatusId = "F",
                            Description = "Завершен"
                        });
                });

            modelBuilder.Entity("Models.RequestsToCancellation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("ConfirmedUserId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime?>("DateDeleted")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("InitiatorUserId")
                        .HasColumnType("integer");

                    b.Property<int>("OrderId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("ConfirmedUserId");

                    b.HasIndex("InitiatorUserId");

                    b.HasIndex("OrderId");

                    b.ToTable("RequestsToCancellation", "dbo");
                });

            modelBuilder.Entity("Models.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("RoleName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Role", "dict");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            RoleName = "USER"
                        },
                        new
                        {
                            Id = 2,
                            RoleName = "ADMIN"
                        },
                        new
                        {
                            Id = 3,
                            RoleName = "MODERATOR"
                        },
                        new
                        {
                            Id = 4,
                            RoleName = "GUEST"
                        });
                });

            modelBuilder.Entity("Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("User", "dbo");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Email = "alexy.alexy98@mail.ru",
                            Name = "admin",
                            Password = "admin",
                            PhoneNumber = "79518306637"
                        },
                        new
                        {
                            Id = 2,
                            Email = "alex.alexy98@mail.ru",
                            Name = "user",
                            Password = "user",
                            PhoneNumber = "88005553535"
                        });
                });

            modelBuilder.Entity("Models.UserProfile", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("Birthday")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("DateCreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime?>("DateDelete")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("DateUpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("IsActived")
                        .HasColumnType("boolean");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("MiddleName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("UserProfile", "dbo");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Birthday = new DateTime(1998, 10, 27, 0, 0, 0, 0, DateTimeKind.Utc),
                            DateCreatedAt = new DateTime(2024, 10, 13, 21, 21, 59, 822, DateTimeKind.Utc).AddTicks(4899),
                            DateUpdatedAt = new DateTime(2024, 10, 13, 21, 21, 59, 822, DateTimeKind.Utc).AddTicks(4900),
                            FirstName = "Alexey",
                            IsActived = true,
                            LastName = "Franchuk",
                            MiddleName = "Dmitrievich",
                            UserId = 1
                        },
                        new
                        {
                            Id = 2,
                            Birthday = new DateTime(1970, 12, 10, 0, 0, 0, 0, DateTimeKind.Utc),
                            DateCreatedAt = new DateTime(2024, 10, 13, 21, 21, 59, 822, DateTimeKind.Utc).AddTicks(4902),
                            DateUpdatedAt = new DateTime(2024, 10, 13, 21, 21, 59, 822, DateTimeKind.Utc).AddTicks(4903),
                            FirstName = "Igor",
                            IsActived = true,
                            LastName = "Menschin",
                            MiddleName = "Vasilievich",
                            UserId = 2
                        });
                });

            modelBuilder.Entity("Models.UserRole", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("RoleId")
                        .HasColumnType("integer");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.HasIndex("UserId");

                    b.ToTable("UserRole", "dbo");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            RoleId = 1,
                            UserId = 1
                        },
                        new
                        {
                            Id = 2,
                            RoleId = 2,
                            UserId = 1
                        },
                        new
                        {
                            Id = 3,
                            RoleId = 3,
                            UserId = 1
                        },
                        new
                        {
                            Id = 4,
                            RoleId = 4,
                            UserId = 1
                        },
                        new
                        {
                            Id = 5,
                            RoleId = 1,
                            UserId = 2
                        },
                        new
                        {
                            Id = 6,
                            RoleId = 4,
                            UserId = 2
                        });
                });

            modelBuilder.Entity("RoleUser", b =>
                {
                    b.Property<int>("RolesId")
                        .HasColumnType("integer");

                    b.Property<int>("UsersId")
                        .HasColumnType("integer");

                    b.HasKey("RolesId", "UsersId");

                    b.HasIndex("UsersId");

                    b.ToTable("RoleUser");
                });

            modelBuilder.Entity("Models.AssignersRequests", b =>
                {
                    b.HasOne("Models.Order", "Order")
                        .WithMany("AssignersRequests")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Models.User", "User")
                        .WithMany("AssignersRequests")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Order");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Models.Order", b =>
                {
                    b.HasOne("Models.OrderPriority", "OrderPriority")
                        .WithMany("Orders")
                        .HasForeignKey("OrderPriorityId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("Models.OrderStatus", "OrderStatus")
                        .WithMany("Orders")
                        .HasForeignKey("OrderStatusId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("Models.User", "UserAssigner")
                        .WithMany("OrdersAssigned")
                        .HasForeignKey("UserIdAssigner")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("Models.User", "UserCreator")
                        .WithMany("OrdersCreated")
                        .HasForeignKey("UserIdCreated")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("OrderPriority");

                    b.Navigation("OrderStatus");

                    b.Navigation("UserAssigner");

                    b.Navigation("UserCreator");
                });

            modelBuilder.Entity("Models.OrderScores", b =>
                {
                    b.HasOne("Models.Order", "Order")
                        .WithMany("OrderScores")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Models.User", "User")
                        .WithMany("OrderScores")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Order");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Models.RequestsToCancellation", b =>
                {
                    b.HasOne("Models.User", "ConfirmedUser")
                        .WithMany("RequestsToCancellationsConfirmer")
                        .HasForeignKey("ConfirmedUserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Models.User", "Initiator")
                        .WithMany("RequestsToCancellationsInitiator")
                        .HasForeignKey("InitiatorUserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Models.Order", "Order")
                        .WithMany("RequestsToCancellations")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("ConfirmedUser");

                    b.Navigation("Initiator");

                    b.Navigation("Order");
                });

            modelBuilder.Entity("Models.UserProfile", b =>
                {
                    b.HasOne("Models.User", "User")
                        .WithOne("UserProfile")
                        .HasForeignKey("Models.UserProfile", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Models.UserRole", b =>
                {
                    b.HasOne("Models.Role", "Role")
                        .WithMany("UserRoles")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Models.User", "User")
                        .WithMany("UserRoles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");

                    b.Navigation("User");
                });

            modelBuilder.Entity("RoleUser", b =>
                {
                    b.HasOne("Models.Role", null)
                        .WithMany()
                        .HasForeignKey("RolesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Models.User", null)
                        .WithMany()
                        .HasForeignKey("UsersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Models.Order", b =>
                {
                    b.Navigation("AssignersRequests");

                    b.Navigation("OrderScores");

                    b.Navigation("RequestsToCancellations");
                });

            modelBuilder.Entity("Models.OrderPriority", b =>
                {
                    b.Navigation("Orders");
                });

            modelBuilder.Entity("Models.OrderStatus", b =>
                {
                    b.Navigation("Orders");
                });

            modelBuilder.Entity("Models.Role", b =>
                {
                    b.Navigation("UserRoles");
                });

            modelBuilder.Entity("Models.User", b =>
                {
                    b.Navigation("AssignersRequests");

                    b.Navigation("OrderScores");

                    b.Navigation("OrdersAssigned");

                    b.Navigation("OrdersCreated");

                    b.Navigation("RequestsToCancellationsConfirmer");

                    b.Navigation("RequestsToCancellationsInitiator");

                    b.Navigation("UserProfile")
                        .IsRequired();

                    b.Navigation("UserRoles");
                });
#pragma warning restore 612, 618
        }
    }
}
