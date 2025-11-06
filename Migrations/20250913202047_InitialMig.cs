using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Tutorial.Migrations
{
    /// <inheritdoc />
    public partial class InitialMig : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "emailVerification",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Email = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Expiry = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Code = table.Column<string>(type: "varchar(6)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_emailVerification", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "registrations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    UserName = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PasswordHash = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Email = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DateOfBirth = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    Address = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PhoneNumber = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PasswordResetToken = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ResetTokenExpiration = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_registrations", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "subjectNa",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    SubName = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Discription = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    RegistrationId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_subjectNa", x => x.Id);
                    table.ForeignKey(
                        name: "FK_subjectNa_registrations_RegistrationId",
                        column: x => x.RegistrationId,
                        principalTable: "registrations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "HwT",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Homework = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Discription = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Deadline = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    subjectsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HwT", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HwT_subjectNa_subjectsId",
                        column: x => x.subjectsId,
                        principalTable: "subjectNa",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "marks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    RegistrationId = table.Column<int>(type: "int", nullable: false),
                    subjectsId = table.Column<int>(type: "int", nullable: false),
                    OralMark = table.Column<int>(name: "Oral Mark", type: "int", nullable: false),
                    WrittenMark = table.Column<int>(name: "Written Mark", type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_marks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_marks_registrations_RegistrationId",
                        column: x => x.RegistrationId,
                        principalTable: "registrations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_marks_subjectNa_subjectsId",
                        column: x => x.subjectsId,
                        principalTable: "subjectNa",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "HwS",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    FilePath = table.Column<string>(type: "VARCHAR(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Created = table.Column<DateTime>(type: "DATETIME", nullable: false),
                    RegistrationId = table.Column<int>(type: "int", nullable: false),
                    HomeworkTId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HwS", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HwS_HwT_HomeworkTId",
                        column: x => x.HomeworkTId,
                        principalTable: "HwT",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HwS_registrations_RegistrationId",
                        column: x => x.RegistrationId,
                        principalTable: "registrations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_HwS_HomeworkTId",
                table: "HwS",
                column: "HomeworkTId");

            migrationBuilder.CreateIndex(
                name: "IX_HwS_RegistrationId",
                table: "HwS",
                column: "RegistrationId");

            migrationBuilder.CreateIndex(
                name: "IX_HwT_subjectsId",
                table: "HwT",
                column: "subjectsId");

            migrationBuilder.CreateIndex(
                name: "IX_marks_RegistrationId",
                table: "marks",
                column: "RegistrationId");

            migrationBuilder.CreateIndex(
                name: "IX_marks_subjectsId",
                table: "marks",
                column: "subjectsId");

            migrationBuilder.CreateIndex(
                name: "IX_subjectNa_RegistrationId",
                table: "subjectNa",
                column: "RegistrationId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "emailVerification");

            migrationBuilder.DropTable(
                name: "HwS");

            migrationBuilder.DropTable(
                name: "marks");

            migrationBuilder.DropTable(
                name: "HwT");

            migrationBuilder.DropTable(
                name: "subjectNa");

            migrationBuilder.DropTable(
                name: "registrations");
        }
    }
}
