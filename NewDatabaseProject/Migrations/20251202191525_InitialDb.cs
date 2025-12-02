using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NewDatabaseProject.Migrations
{
    /// <inheritdoc />
    public partial class InitialDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Classes",
                columns: table => new
                {
                    name = table.Column<string>(type: "TEXT", nullable: false),
                    date = table.Column<string>(type: "TEXT", nullable: false),
                    time = table.Column<string>(type: "TEXT", nullable: false),
                    minimum_membership_access = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Classes", x => new { x.name, x.date, x.time });
                });

            migrationBuilder.CreateTable(
                name: "Members",
                columns: table => new
                {
                    email = table.Column<string>(type: "TEXT", nullable: false),
                    membership_type = table.Column<string>(type: "TEXT", nullable: false),
                    name = table.Column<string>(type: "TEXT", nullable: false),
                    birthday = table.Column<string>(type: "TEXT", nullable: false),
                    picture = table.Column<string>(type: "TEXT", nullable: false),
                    date_signed_up = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Members", x => x.email);
                });

            migrationBuilder.CreateTable(
                name: "MembershipTypes",
                columns: table => new
                {
                    Access_Level = table.Column<string>(type: "TEXT", nullable: false),
                    price = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MembershipTypes", x => x.Access_Level);
                });

            migrationBuilder.CreateTable(
                name: "Trainers",
                columns: table => new
                {
                    employee_id = table.Column<string>(type: "TEXT", nullable: false),
                    membership_type = table.Column<string>(type: "TEXT", nullable: false),
                    name = table.Column<string>(type: "TEXT", nullable: false),
                    birthday = table.Column<string>(type: "TEXT", nullable: false),
                    picture = table.Column<string>(type: "TEXT", nullable: false),
                    date_started_working = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trainers", x => x.employee_id);
                });

            migrationBuilder.CreateTable(
                name: "ClassModelsMemberModel",
                columns: table => new
                {
                    membersemail = table.Column<string>(type: "TEXT", nullable: false),
                    Classesname = table.Column<string>(type: "TEXT", nullable: false),
                    Classesdate = table.Column<string>(type: "TEXT", nullable: false),
                    Classestime = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClassModelsMemberModel", x => new { x.membersemail, x.Classesname, x.Classesdate, x.Classestime });
                    table.ForeignKey(
                        name: "FK_ClassModelsMemberModel_Classes_Classesname_Classesdate_Classestime",
                        columns: x => new { x.Classesname, x.Classesdate, x.Classestime },
                        principalTable: "Classes",
                        principalColumns: new[] { "name", "date", "time" },
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ClassModelsMemberModel_Members_membersemail",
                        column: x => x.membersemail,
                        principalTable: "Members",
                        principalColumn: "email",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ClassModelsTrainerModel",
                columns: table => new
                {
                    traineremployee_id = table.Column<string>(type: "TEXT", nullable: false),
                    Classes_Teachingname = table.Column<string>(type: "TEXT", nullable: false),
                    Classes_Teachingdate = table.Column<string>(type: "TEXT", nullable: false),
                    Classes_Teachingtime = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClassModelsTrainerModel", x => new { x.traineremployee_id, x.Classes_Teachingname, x.Classes_Teachingdate, x.Classes_Teachingtime });
                    table.ForeignKey(
                        name: "FK_ClassModelsTrainerModel_Classes_Classes_Teachingname_Classes_Teachingdate_Classes_Teachingtime",
                        columns: x => new { x.Classes_Teachingname, x.Classes_Teachingdate, x.Classes_Teachingtime },
                        principalTable: "Classes",
                        principalColumns: new[] { "name", "date", "time" },
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ClassModelsTrainerModel_Trainers_traineremployee_id",
                        column: x => x.traineremployee_id,
                        principalTable: "Trainers",
                        principalColumn: "employee_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ClassModelsMemberModel_Classesname_Classesdate_Classestime",
                table: "ClassModelsMemberModel",
                columns: new[] { "Classesname", "Classesdate", "Classestime" });

            migrationBuilder.CreateIndex(
                name: "IX_ClassModelsTrainerModel_Classes_Teachingname_Classes_Teachingdate_Classes_Teachingtime",
                table: "ClassModelsTrainerModel",
                columns: new[] { "Classes_Teachingname", "Classes_Teachingdate", "Classes_Teachingtime" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ClassModelsMemberModel");

            migrationBuilder.DropTable(
                name: "ClassModelsTrainerModel");

            migrationBuilder.DropTable(
                name: "MembershipTypes");

            migrationBuilder.DropTable(
                name: "Members");

            migrationBuilder.DropTable(
                name: "Classes");

            migrationBuilder.DropTable(
                name: "Trainers");
        }
    }
}
