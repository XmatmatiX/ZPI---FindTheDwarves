using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FindTheDwarves.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddNewDbSets : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AchievementDwarf_Achievements_AchievementID",
                table: "AchievementDwarf");

            migrationBuilder.DropForeignKey(
                name: "FK_AchievementDwarf_Dwarves_DwarfID",
                table: "AchievementDwarf");

            migrationBuilder.DropForeignKey(
                name: "FK_UserAchievement_Achievements_AchievementID",
                table: "UserAchievement");

            migrationBuilder.DropForeignKey(
                name: "FK_UserAchievement_Users_UserID",
                table: "UserAchievement");

            migrationBuilder.DropForeignKey(
                name: "FK_UserDwarf_Dwarves_DwarfID",
                table: "UserDwarf");

            migrationBuilder.DropForeignKey(
                name: "FK_UserDwarf_Users_UserID",
                table: "UserDwarf");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserDwarf",
                table: "UserDwarf");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserAchievement",
                table: "UserAchievement");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AchievementDwarf",
                table: "AchievementDwarf");

            migrationBuilder.RenameTable(
                name: "UserDwarf",
                newName: "UserDwarves");

            migrationBuilder.RenameTable(
                name: "UserAchievement",
                newName: "UserAchievements");

            migrationBuilder.RenameTable(
                name: "AchievementDwarf",
                newName: "AchievementDwarves");

            migrationBuilder.RenameIndex(
                name: "IX_UserDwarf_DwarfID",
                table: "UserDwarves",
                newName: "IX_UserDwarves_DwarfID");

            migrationBuilder.RenameIndex(
                name: "IX_UserAchievement_AchievementID",
                table: "UserAchievements",
                newName: "IX_UserAchievements_AchievementID");

            migrationBuilder.RenameIndex(
                name: "IX_AchievementDwarf_AchievementID",
                table: "AchievementDwarves",
                newName: "IX_AchievementDwarves_AchievementID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserDwarves",
                table: "UserDwarves",
                columns: new[] { "UserID", "DwarfID" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserAchievements",
                table: "UserAchievements",
                columns: new[] { "UserID", "AchievementID" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_AchievementDwarves",
                table: "AchievementDwarves",
                columns: new[] { "DwarfID", "AchievementID" });

            migrationBuilder.AddForeignKey(
                name: "FK_AchievementDwarves_Achievements_AchievementID",
                table: "AchievementDwarves",
                column: "AchievementID",
                principalTable: "Achievements",
                principalColumn: "AchievementID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AchievementDwarves_Dwarves_DwarfID",
                table: "AchievementDwarves",
                column: "DwarfID",
                principalTable: "Dwarves",
                principalColumn: "DwarfID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserAchievements_Achievements_AchievementID",
                table: "UserAchievements",
                column: "AchievementID",
                principalTable: "Achievements",
                principalColumn: "AchievementID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserAchievements_Users_UserID",
                table: "UserAchievements",
                column: "UserID",
                principalTable: "Users",
                principalColumn: "UserID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserDwarves_Dwarves_DwarfID",
                table: "UserDwarves",
                column: "DwarfID",
                principalTable: "Dwarves",
                principalColumn: "DwarfID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserDwarves_Users_UserID",
                table: "UserDwarves",
                column: "UserID",
                principalTable: "Users",
                principalColumn: "UserID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AchievementDwarves_Achievements_AchievementID",
                table: "AchievementDwarves");

            migrationBuilder.DropForeignKey(
                name: "FK_AchievementDwarves_Dwarves_DwarfID",
                table: "AchievementDwarves");

            migrationBuilder.DropForeignKey(
                name: "FK_UserAchievements_Achievements_AchievementID",
                table: "UserAchievements");

            migrationBuilder.DropForeignKey(
                name: "FK_UserAchievements_Users_UserID",
                table: "UserAchievements");

            migrationBuilder.DropForeignKey(
                name: "FK_UserDwarves_Dwarves_DwarfID",
                table: "UserDwarves");

            migrationBuilder.DropForeignKey(
                name: "FK_UserDwarves_Users_UserID",
                table: "UserDwarves");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserDwarves",
                table: "UserDwarves");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserAchievements",
                table: "UserAchievements");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AchievementDwarves",
                table: "AchievementDwarves");

            migrationBuilder.RenameTable(
                name: "UserDwarves",
                newName: "UserDwarf");

            migrationBuilder.RenameTable(
                name: "UserAchievements",
                newName: "UserAchievement");

            migrationBuilder.RenameTable(
                name: "AchievementDwarves",
                newName: "AchievementDwarf");

            migrationBuilder.RenameIndex(
                name: "IX_UserDwarves_DwarfID",
                table: "UserDwarf",
                newName: "IX_UserDwarf_DwarfID");

            migrationBuilder.RenameIndex(
                name: "IX_UserAchievements_AchievementID",
                table: "UserAchievement",
                newName: "IX_UserAchievement_AchievementID");

            migrationBuilder.RenameIndex(
                name: "IX_AchievementDwarves_AchievementID",
                table: "AchievementDwarf",
                newName: "IX_AchievementDwarf_AchievementID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserDwarf",
                table: "UserDwarf",
                columns: new[] { "UserID", "DwarfID" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserAchievement",
                table: "UserAchievement",
                columns: new[] { "UserID", "AchievementID" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_AchievementDwarf",
                table: "AchievementDwarf",
                columns: new[] { "DwarfID", "AchievementID" });

            migrationBuilder.AddForeignKey(
                name: "FK_AchievementDwarf_Achievements_AchievementID",
                table: "AchievementDwarf",
                column: "AchievementID",
                principalTable: "Achievements",
                principalColumn: "AchievementID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AchievementDwarf_Dwarves_DwarfID",
                table: "AchievementDwarf",
                column: "DwarfID",
                principalTable: "Dwarves",
                principalColumn: "DwarfID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserAchievement_Achievements_AchievementID",
                table: "UserAchievement",
                column: "AchievementID",
                principalTable: "Achievements",
                principalColumn: "AchievementID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserAchievement_Users_UserID",
                table: "UserAchievement",
                column: "UserID",
                principalTable: "Users",
                principalColumn: "UserID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserDwarf_Dwarves_DwarfID",
                table: "UserDwarf",
                column: "DwarfID",
                principalTable: "Dwarves",
                principalColumn: "DwarfID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserDwarf_Users_UserID",
                table: "UserDwarf",
                column: "UserID",
                principalTable: "Users",
                principalColumn: "UserID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
