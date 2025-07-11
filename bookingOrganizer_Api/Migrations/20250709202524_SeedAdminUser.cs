using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace bookingOrganizer_Api.Migrations
{
    /// <inheritdoc />
    public partial class SeedAdminUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Groups",
                columns: table => new
                {
                    GroupId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GroupName = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Groups__149AF36AF1039B53", x => x.GroupId);
                });

            migrationBuilder.CreateTable(
                name: "TypeBooking",
                columns: table => new
                {
                    TypeBookingId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TypeName = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__TypeBook__7D5A246FF094A373", x => x.TypeBookingId);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    PasswordHash = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    Email = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    FullName = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Users__1788CC4CF252C45E", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "BookingInfo",
                columns: table => new
                {
                    BookingId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GroupId = table.Column<int>(type: "int", nullable: false),
                    TypeBookingId = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    EndDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    Address = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    PurchaseMethod = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    SeatNumber = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: true),
                    Notes = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__BookingI__73951AED45200D0B", x => x.BookingId);
                    table.ForeignKey(
                        name: "FK_BookingInfo_TypeBooking_TypeBookingId",
                        column: x => x.TypeBookingId,
                        principalTable: "TypeBooking",
                        principalColumn: "TypeBookingId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK__BookingIn__Group__440B1D61",
                        column: x => x.GroupId,
                        principalTable: "Groups",
                        principalColumn: "GroupId");
                });

            migrationBuilder.CreateTable(
                name: "GroupMembers",
                columns: table => new
                {
                    GroupMemberId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GroupId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__GroupMem__3448129267F99235", x => x.GroupMemberId);
                    table.ForeignKey(
                        name: "FK__GroupMemb__Group__403A8C7D",
                        column: x => x.GroupId,
                        principalTable: "Groups",
                        principalColumn: "GroupId");
                    table.ForeignKey(
                        name: "FK__GroupMemb__UserI__412EB0B6",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId");
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "Email", "FullName", "PasswordHash", "UserName" },
                values: new object[] { 1, "Administrator", null, "AQAAAAIAACcQAAAAEFa7VfG0Ozvhq4Zw1URzDDLwcWZmHHNOvgWe0hCeja+rG+56ms4YSQ2VrgmYyuAdMw==", "admin" });

            migrationBuilder.CreateIndex(
                name: "IX_BookingInfo_GroupId",
                table: "BookingInfo",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_BookingInfo_TypeBookingId",
                table: "BookingInfo",
                column: "TypeBookingId");

            migrationBuilder.CreateIndex(
                name: "IX_GroupMembers_GroupId",
                table: "GroupMembers",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_GroupMembers_UserId",
                table: "GroupMembers",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "UQ__TypeBook__D4E7DFA8E5EA2FA8",
                table: "TypeBooking",
                column: "TypeName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ__Users__A9D10534631987BF",
                table: "Users",
                column: "Email",
                unique: true,
                filter: "[Email] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "UQ__Users__C9F28456690B0F69",
                table: "Users",
                column: "UserName",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BookingInfo");

            migrationBuilder.DropTable(
                name: "GroupMembers");

            migrationBuilder.DropTable(
                name: "TypeBooking");

            migrationBuilder.DropTable(
                name: "Groups");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
