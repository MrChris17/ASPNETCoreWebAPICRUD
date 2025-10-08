using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ASPNETCoreWebAPICRUD.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tbl_course",
                columns: table => new
                {
                    id = table.Column<int>(type: "INT", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    name = table.Column<string>(type: "VARCHAR(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_course", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "tbl_student",
                columns: table => new
                {
                    id = table.Column<int>(type: "INT", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    name = table.Column<string>(type: "VARCHAR(70)", nullable: false),
                    address = table.Column<string>(type: "VARCHAR(250)", nullable: false),
                    email_address = table.Column<string>(type: "VARCHAR(100)", nullable: false),
                    phone_number = table.Column<string>(type: "VARCHAR(11)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_student", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "tbl_subject",
                columns: table => new
                {
                    id = table.Column<int>(type: "INT", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    name = table.Column<string>(type: "VARCHAR(50)", nullable: false),
                    description = table.Column<string>(type: "TEXT", nullable: true),
                    course_id = table.Column<int>(type: "INT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_subject", x => x.id);
                    table.ForeignKey(
                        name: "FK_tbl_subject_tbl_course_course_id",
                        column: x => x.course_id,
                        principalTable: "tbl_course",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tbl_student_subject",
                columns: table => new
                {
                    student_id = table.Column<int>(type: "INT", nullable: false),
                    subject_id = table.Column<int>(type: "INT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_student_subject", x => new { x.student_id, x.subject_id });
                    table.ForeignKey(
                        name: "FK_tbl_student_subject_tbl_student_student_id",
                        column: x => x.student_id,
                        principalTable: "tbl_student",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tbl_student_subject_tbl_subject_subject_id",
                        column: x => x.subject_id,
                        principalTable: "tbl_subject",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tbl_student_subject_subject_id",
                table: "tbl_student_subject",
                column: "subject_id");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_subject_course_id",
                table: "tbl_subject",
                column: "course_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tbl_student_subject");

            migrationBuilder.DropTable(
                name: "tbl_student");

            migrationBuilder.DropTable(
                name: "tbl_subject");

            migrationBuilder.DropTable(
                name: "tbl_course");
        }
    }
}
