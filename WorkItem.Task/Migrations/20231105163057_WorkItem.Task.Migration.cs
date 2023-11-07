using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WorkItem.Task.Migrations
{
    /// <inheritdoc />
    public partial class WorkItemTaskMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.CreateTable(
            //    name: "TaskStatuses",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        Status = table.Column<string>(type: "nvarchar(max)", nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_TaskStatuses", x => x.Id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "UserProfiles",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        UserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        EmailId = table.Column<string>(type: "nvarchar(max)", nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_UserProfiles", x => x.Id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "Tasks",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        Assigned = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        StatusId = table.Column<int>(type: "int", nullable: true),
            //        Discription = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        Comments = table.Column<string>(type: "nvarchar(max)", nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_Tasks", x => x.Id);
            //        table.ForeignKey(
            //            name: "FK_Tasks_TaskStatuses_StatusId",
            //            column: x => x.StatusId,
            //            principalTable: "TaskStatuses",
            //            principalColumn: "Id");
            //    });

            //migrationBuilder.CreateIndex(
            //    name: "IX_Tasks_StatusId",
            //    table: "Tasks",
            //    column: "StatusId");

            // TaskStatuses table Data feed
            migrationBuilder.InsertData("TaskStatuses", "Status", "To Do");
            migrationBuilder.InsertData("TaskStatuses", "Status", "Doing");
            migrationBuilder.InsertData("TaskStatuses", "Status", "Done");


            migrationBuilder.InsertData("UserProfiles", new string[] { "UserName", "EmailId" }, new object[] { "Kiran", "kiran@gmail.com" });
            migrationBuilder.InsertData("UserProfiles", new string[] { "UserName", "EmailId" }, new object[] { "Shweths", "Shweths@gmail.com" });
            migrationBuilder.InsertData("UserProfiles", new string[] { "UserName", "EmailId" }, new object[] { "John", "John@gmail.com" });
            migrationBuilder.InsertData("UserProfiles", new string[] { "UserName", "EmailId" }, new object[] { "Rama", "Rama@gmail.com" });
            migrationBuilder.InsertData("UserProfiles", new string[] { "UserName", "EmailId" }, new object[] { "Mohammed", "Mohammed@gmail.com" });

            migrationBuilder.InsertData("Tasks", new string[] { "Title", "Assigned", "StatusId", "Discription", "Comments" }, new object[] { "Task-1", "Kiran@gmail.com", 1, "Discription-1", "Comment-1" });
            migrationBuilder.InsertData("Tasks", new string[] { "Title", "Assigned", "StatusId", "Discription", "Comments" }, new object[] { "Task-2", "Shwetha@gmail.com", 2, "Discription-2", "Comment-2" });
            migrationBuilder.InsertData("Tasks", new string[] { "Title", "Assigned", "StatusId", "Discription", "Comments" }, new object[] { "Task-3", "John@gmail.com", 3, "Discription-3", "Comment-3" });
            migrationBuilder.InsertData("Tasks", new string[] { "Title", "Assigned", "StatusId", "Discription", "Comments" }, new object[] { "Task-4", "Rama@gmail.com", 2, "Discription-4", "Comment-4" });
            migrationBuilder.InsertData("Tasks", new string[] { "Title", "Assigned", "StatusId", "Discription", "Comments" }, new object[] { "Task-5", "Mohammed@gmail.com", 3, "Discription-5", "Comment-5" });

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Tasks");

            migrationBuilder.DropTable(
                name: "UserProfiles");

            migrationBuilder.DropTable(
                name: "TaskStatuses");
        }
    }
}
