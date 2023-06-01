using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataLayer.Migrations
{
    /// <inheritdoc />
    public partial class Moderating : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Communities_Users_ModeratorId",
                table: "Communities");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Communities_CommunityId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_CommunityId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Communities_ModeratorId",
                table: "Communities");

            migrationBuilder.DropColumn(
                name: "CommunityId",
                table: "Users");

            migrationBuilder.AddColumn<int>(
                name: "ModeratedCommunityId",
                table: "Users",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Communities_ModeratorId",
                table: "Communities",
                column: "ModeratorId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Communities_Users_ModeratorId",
                table: "Communities",
                column: "ModeratorId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Communities_Users_ModeratorId",
                table: "Communities");

            migrationBuilder.DropIndex(
                name: "IX_Communities_ModeratorId",
                table: "Communities");

            migrationBuilder.DropColumn(
                name: "ModeratedCommunityId",
                table: "Users");

            migrationBuilder.AddColumn<int>(
                name: "CommunityId",
                table: "Users",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_CommunityId",
                table: "Users",
                column: "CommunityId");

            migrationBuilder.CreateIndex(
                name: "IX_Communities_ModeratorId",
                table: "Communities",
                column: "ModeratorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Communities_Users_ModeratorId",
                table: "Communities",
                column: "ModeratorId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Communities_CommunityId",
                table: "Users",
                column: "CommunityId",
                principalTable: "Communities",
                principalColumn: "Id");
        }
    }
}
