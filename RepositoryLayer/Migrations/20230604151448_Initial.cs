using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace RepositoryLayer.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Administrators",
                columns: table => new
                {
                    UserID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Username = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ConfirmPassword = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Age = table.Column<int>(type: "int", nullable: false),
                    Avatar = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Administrators", x => x.UserID);
                });

            migrationBuilder.CreateTable(
                name: "GPSCoordinates",
                columns: table => new
                {
                    LocationID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Latitude = table.Column<double>(type: "float", nullable: false),
                    Longitude = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GPSCoordinates", x => x.LocationID);
                });

            migrationBuilder.CreateTable(
                name: "Clients",
                columns: table => new
                {
                    UserID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Incapacity = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserLocationID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Username = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ConfirmPassword = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Age = table.Column<int>(type: "int", nullable: false),
                    Avatar = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clients", x => x.UserID);
                    table.ForeignKey(
                        name: "FK_Clients_GPSCoordinates_UserLocationID",
                        column: x => x.UserLocationID,
                        principalTable: "GPSCoordinates",
                        principalColumn: "LocationID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Points",
                columns: table => new
                {
                    PointID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PointName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Image360 = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    PointLocationID = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Points", x => x.PointID);
                    table.ForeignKey(
                        name: "FK_Points_GPSCoordinates_PointLocationID",
                        column: x => x.PointLocationID,
                        principalTable: "GPSCoordinates",
                        principalColumn: "LocationID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Feedbacks",
                columns: table => new
                {
                    FeedbackID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClientID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FeedbackLocationID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FeedbackDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FeedbackDescription = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    CloudFolderURL = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Feedbacks", x => x.FeedbackID);
                    table.ForeignKey(
                        name: "FK_Feedbacks_Clients_ClientID",
                        column: x => x.ClientID,
                        principalTable: "Clients",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Feedbacks_GPSCoordinates_FeedbackLocationID",
                        column: x => x.FeedbackLocationID,
                        principalTable: "GPSCoordinates",
                        principalColumn: "LocationID");
                });

            migrationBuilder.CreateTable(
                name: "Courses",
                columns: table => new
                {
                    CourseID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    InitialPointID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EndPointID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    IncapacityAcessible = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Courses", x => x.CourseID);
                    table.ForeignKey(
                        name: "FK_Courses_Points_EndPointID",
                        column: x => x.EndPointID,
                        principalTable: "Points",
                        principalColumn: "PointID");
                    table.ForeignKey(
                        name: "FK_Courses_Points_InitialPointID",
                        column: x => x.InitialPointID,
                        principalTable: "Points",
                        principalColumn: "PointID");
                });

            migrationBuilder.InsertData(
                table: "Administrators",
                columns: new[] { "UserID", "Age", "Avatar", "ConfirmPassword", "Email", "IsActive", "Password", "Username" },
                values: new object[,]
                {
                    { new Guid("eed7c5fb-992a-4dab-bf79-20ec21fbaad5"), 21, "ku", "thatsHowMafia7$Works", "adminer@gmail.com", false, "thatsHowMafia7$Works", "admin" },
                    { new Guid("f15b79b8-d471-4139-8fa2-6db4900d80f7"), 18, "hg", "itsTheEndOfThe$Pasta1", "mariopasta@gmail.com", false, "itsTheEndOfThe$Pasta1", "Mario" }
                });

            migrationBuilder.InsertData(
                table: "GPSCoordinates",
                columns: new[] { "LocationID", "Latitude", "Longitude" },
                values: new object[,]
                {
                    { new Guid("1cc040df-f013-47e0-b42b-f3044863f155"), 41.17833004000633, -8.6068877514979683 },
                    { new Guid("2a671978-6c7a-4ba2-a81d-2b98f7f3c8f2"), 41.177967145422194, -8.6085062262311105 },
                    { new Guid("36ab6d27-20a4-4937-ae3a-ddbd35cc1e27"), 41.178984721995306, -8.6083692036936128 },
                    { new Guid("49b88907-af06-4c33-9276-94c32d3cd3f2"), 41.178472726585674, -8.6087807216442869 },
                    { new Guid("537448be-b920-4541-91a8-dc2e5460dd43"), 41.178773264549697, -8.608947821523687 },
                    { new Guid("595b51cc-9116-4cf3-86bc-c151c686629a"), 41.177841263041209, -8.6079983063935739 },
                    { new Guid("753a2d07-ad1c-43be-9474-0e38704f8658"), 41.179110097095872, -8.6070945594407782 },
                    { new Guid("7816051a-4970-4590-9e9a-ef543ca1ad27"), 41.179276075812993, -8.6078482087685995 },
                    { new Guid("8481df56-1007-46c0-a3e9-c66b200ba356"), 41.178104599027193, -8.6079171398224492 },
                    { new Guid("88994499-fd98-46e0-bee9-66af8fe6a9be"), 41.17944250803648, -8.6086616434248153 },
                    { new Guid("8cf8447e-e9e0-46d9-983c-3ea7d5c7ef47"), 41.177711788665363, -8.6077586936904744 },
                    { new Guid("932316af-16b6-41bf-8ef1-9f202f735f37"), 0.0, 0.0 },
                    { new Guid("9871553d-0d82-4fe5-9374-b8786156710c"), 41.177564994929561, -8.6083047981405159 },
                    { new Guid("9d26ba2b-452b-4858-8691-008d9139142a"), 41.179084789896223, -8.6087289902487836 },
                    { new Guid("9ddf7080-ac80-42dd-9c34-f5dab4cb6790"), 41.178843863356043, -8.60702514650362 },
                    { new Guid("ae12188b-ebc9-4c64-87db-946020c31908"), 41.1794081572219, -8.6066880570955586 },
                    { new Guid("c41b3aa5-d285-4986-8d48-3e54ab5376f8"), 41.178155377732104, -8.6083497177966759 },
                    { new Guid("c59fe3f2-6682-4ec9-9a2e-13fe229eff1b"), 41.178602169673589, -8.6089645852372154 },
                    { new Guid("e22a5415-d4af-40a0-9bc3-34ccbc2c5540"), 41.179435815023893, -8.6070977092565961 },
                    { new Guid("e6f8a262-1514-42c8-bb9a-bb7b857072ac"), 41.179469643235471, -8.6059597348474224 },
                    { new Guid("f7e0c983-c9af-4036-a4a2-a2b5e2de9032"), 41.178429896021797, -8.6075151873977322 }
                });

            migrationBuilder.InsertData(
                table: "Clients",
                columns: new[] { "UserID", "Age", "Avatar", "ConfirmPassword", "Email", "Incapacity", "IsActive", "Password", "UserLocationID", "Username" },
                values: new object[,]
                {
                    { new Guid("128cdce3-b7c3-4ebd-8b4e-9e2fba9c30a4"), 16, "bh", "queijo123ComBacon$", "diogomorfador@gmail.com", "", true, "queijo123ComBacon$", new Guid("932316af-16b6-41bf-8ef1-9f202f735f37"), "Diogo" },
                    { new Guid("44a6aac2-20b8-44cf-b283-f80895b8768c"), 22, "as", "forTheMotherFoca@7", "josefino@gmail.com", "", true, "forTheMotherFoca@7", new Guid("932316af-16b6-41bf-8ef1-9f202f735f37"), "Joseph" },
                    { new Guid("464b1ff3-7df8-4dbf-a80a-a164c6ed02b0"), 22, "yh", "sirvaPureComArroz$1", "ceftigas@gmail.com", "", true, "sirvaPureComArroz$1", new Guid("932316af-16b6-41bf-8ef1-9f202f735f37"), "Tiago" },
                    { new Guid("cfe6ffcb-ea08-4bb5-9f9d-76ae099ebd27"), 30, "gv", "euGostoDeCamul123$", "carlitosbritos@gmail.com", "wheelChair", true, "euGostoDeCamul123$", new Guid("49b88907-af06-4c33-9276-94c32d3cd3f2"), "Carlos" }
                });

            migrationBuilder.InsertData(
                table: "Points",
                columns: new[] { "PointID", "Description", "Image360", "PointLocationID", "PointName" },
                values: new object[,]
                {
                    { new Guid("1e29bc7c-daf3-4baf-821f-549f772fcb1b"), "Entry from parking car next to Auditorio Magno", "ws", new Guid("537448be-b920-4541-91a8-dc2e5460dd43"), "Beacon Entry Backs" },
                    { new Guid("267c58c2-e7a9-4912-a211-ccdad2e78a9c"), "Beacon from Building K", "db", new Guid("36ab6d27-20a4-4937-ae3a-ddbd35cc1e27"), "Beacon K" },
                    { new Guid("2e3eca64-e939-4f2c-93ea-0306f4e62a1c"), "Beacon from Building D", "gv", new Guid("753a2d07-ad1c-43be-9474-0e38704f8658"), "Beacon D" },
                    { new Guid("4fadedf8-9263-43b0-9695-6db3f3ff4d8e"), "Beacon from Building J", "ad", new Guid("f7e0c983-c9af-4036-a4a2-a2b5e2de9032"), "Beacon J" },
                    { new Guid("60bf9cf6-b963-42af-ae34-a559a73463da"), "Beacon from Building I", "gb", new Guid("8481df56-1007-46c0-a3e9-c66b200ba356"), "Beacon I" },
                    { new Guid("9ebe9d4d-69ce-4d11-bf41-a282981da133"), "Entry from Building H", "rt", new Guid("c41b3aa5-d285-4986-8d48-3e54ab5376f8"), "Beacon Entry Main Gate" },
                    { new Guid("a954516d-cfec-45f8-bdb7-fc9f4498e5ac"), "Beacon from Building H", "jg", new Guid("2a671978-6c7a-4ba2-a81d-2b98f7f3c8f2"), "Beacon H" },
                    { new Guid("ae1feab2-ba41-4013-a65c-a96005918e93"), "Beacon from Building C", "jy", new Guid("9ddf7080-ac80-42dd-9c34-f5dab4cb6790"), "Beacon C" },
                    { new Guid("bc9ef8d3-ed40-471a-b4b4-951b4c6f9a52"), "Auditorio Magno Building", "hb", new Guid("c59fe3f2-6682-4ec9-9a2e-13fe229eff1b"), "Auditório Magno" },
                    { new Guid("c29aae18-81eb-4be9-8b96-59f09e4c20c8"), "Beacon from Building F", "bj", new Guid("7816051a-4970-4590-9e9a-ef543ca1ad27"), "Beacon F" },
                    { new Guid("d24e559b-5582-40a1-b123-51d2c12d5c8b"), "Beacon from Building B", "ng", new Guid("8cf8447e-e9e0-46d9-983c-3ea7d5c7ef47"), "Beacon B" },
                    { new Guid("d5fb6972-4a61-4c11-9c15-8e856169c54e"), "Beacon from Building G", "rh", new Guid("9871553d-0d82-4fe5-9374-b8786156710c"), "Beacon G" },
                    { new Guid("d9ad703e-3b2e-4b8e-bfe0-1bbbc10f71ba"), "Beacon from Building E", "uh", new Guid("e6f8a262-1514-42c8-bb9a-bb7b857072ac"), "Beacon E" },
                    { new Guid("dc541192-44b7-454c-b81e-dca6119f8352"), "Beacon from Building A", "sd", new Guid("49b88907-af06-4c33-9276-94c32d3cd3f2"), "Beacon A" },
                    { new Guid("fa6e8a20-8558-4a18-ad41-7876abb95662"), "Beacon from Building L", "cd", new Guid("595b51cc-9116-4cf3-86bc-c151c686629a"), "Beacon L" },
                    { new Guid("fb2d7b4c-0f58-413b-85fc-7bb1664c0918"), "Entry from building E", "gf", new Guid("1cc040df-f013-47e0-b42b-f3044863f155"), "Beacon Front Entry" }
                });

            migrationBuilder.InsertData(
                table: "Courses",
                columns: new[] { "CourseID", "Description", "EndPointID", "IncapacityAcessible", "InitialPointID" },
                values: new object[,]
                {
                    { new Guid("1ac1bc72-16fb-45d1-99a1-37e928bf4547"), "Path from B to G", new Guid("d5fb6972-4a61-4c11-9c15-8e856169c54e"), true, new Guid("d24e559b-5582-40a1-b123-51d2c12d5c8b") },
                    { new Guid("2df6a70f-652d-480b-a62e-d98439626fe2"), "Path from AuditorioMagno to A ", new Guid("dc541192-44b7-454c-b81e-dca6119f8352"), true, new Guid("bc9ef8d3-ed40-471a-b4b4-951b4c6f9a52") },
                    { new Guid("80f2d865-9121-4405-ac2b-322d389257f9"), "Path from H to AuditorioMagno", new Guid("bc9ef8d3-ed40-471a-b4b4-951b4c6f9a52"), true, new Guid("a954516d-cfec-45f8-bdb7-fc9f4498e5ac") },
                    { new Guid("85bb7c32-32b7-4d49-90e5-dc52db2d08a4"), "Path from AuditorioMagno to H", new Guid("a954516d-cfec-45f8-bdb7-fc9f4498e5ac"), true, new Guid("bc9ef8d3-ed40-471a-b4b4-951b4c6f9a52") },
                    { new Guid("8ffa3a59-ed58-4bee-a6c9-5be30920f596"), "Path from I to A", new Guid("dc541192-44b7-454c-b81e-dca6119f8352"), true, new Guid("60bf9cf6-b963-42af-ae34-a559a73463da") },
                    { new Guid("9b3eb38a-2857-40a6-a5c8-4d36e14c7147"), "Path from A to I", new Guid("60bf9cf6-b963-42af-ae34-a559a73463da"), true, new Guid("dc541192-44b7-454c-b81e-dca6119f8352") },
                    { new Guid("a8d75a59-c3fc-4d24-8d19-35080c5e4f96"), "Path from G to B", new Guid("d24e559b-5582-40a1-b123-51d2c12d5c8b"), true, new Guid("d5fb6972-4a61-4c11-9c15-8e856169c54e") },
                    { new Guid("b1bd4c64-2e8a-42c6-bfc6-7d230660528b"), "Path from A to H", new Guid("a954516d-cfec-45f8-bdb7-fc9f4498e5ac"), true, new Guid("dc541192-44b7-454c-b81e-dca6119f8352") },
                    { new Guid("ca23019d-c3c2-4ab7-9e46-95e3d9f63b98"), "Path from A to AuditorioMagno", new Guid("bc9ef8d3-ed40-471a-b4b4-951b4c6f9a52"), true, new Guid("dc541192-44b7-454c-b81e-dca6119f8352") },
                    { new Guid("cf6429a9-0db6-4dfb-a0ff-8e89e0b61499"), "Path from H to A", new Guid("dc541192-44b7-454c-b81e-dca6119f8352"), true, new Guid("a954516d-cfec-45f8-bdb7-fc9f4498e5ac") },
                    { new Guid("e85cbe41-9061-47a4-a445-485c76d4762a"), "Path from H to G", new Guid("d5fb6972-4a61-4c11-9c15-8e856169c54e"), true, new Guid("a954516d-cfec-45f8-bdb7-fc9f4498e5ac") },
                    { new Guid("fa5ba836-c23b-4cc0-9a92-c71a788c71cb"), "Path from I to B", new Guid("d24e559b-5582-40a1-b123-51d2c12d5c8b"), true, new Guid("60bf9cf6-b963-42af-ae34-a559a73463da") },
                    { new Guid("fa9418b5-2065-48f2-b32f-86d2dfd4790d"), "Path from B to I", new Guid("60bf9cf6-b963-42af-ae34-a559a73463da"), true, new Guid("d24e559b-5582-40a1-b123-51d2c12d5c8b") },
                    { new Guid("fce83bd0-512e-409d-a6f6-bdce8971bb8b"), "Path from G to H", new Guid("a954516d-cfec-45f8-bdb7-fc9f4498e5ac"), true, new Guid("d5fb6972-4a61-4c11-9c15-8e856169c54e") }
                });

            migrationBuilder.InsertData(
                table: "Feedbacks",
                columns: new[] { "FeedbackID", "ClientID", "CloudFolderURL", "FeedbackDate", "FeedbackDescription", "FeedbackLocationID" },
                values: new object[,]
                {
                    { new Guid("14bf6c6d-45c5-4d36-ba90-dbeb07c6c5b5"), new Guid("464b1ff3-7df8-4dbf-a80a-a164c6ed02b0"), "https://www.google.com", new DateTime(2023, 6, 4, 15, 14, 48, 692, DateTimeKind.Utc).AddTicks(9321), "The sight is awful!", new Guid("932316af-16b6-41bf-8ef1-9f202f735f37") },
                    { new Guid("633f9bc1-cb28-400f-89e2-1c8a78157599"), new Guid("44a6aac2-20b8-44cf-b283-f80895b8768c"), "https://www.google.com", new DateTime(2023, 6, 4, 15, 14, 48, 692, DateTimeKind.Utc).AddTicks(9332), "Thank you for the reception from ISEP!", new Guid("932316af-16b6-41bf-8ef1-9f202f735f37") },
                    { new Guid("68f7ddc7-9c13-4360-8ec9-894fcc84dffd"), new Guid("128cdce3-b7c3-4ebd-8b4e-9e2fba9c30a4"), "https://www.google.com", new DateTime(2023, 6, 4, 15, 14, 48, 692, DateTimeKind.Utc).AddTicks(9315), "The building is different.", new Guid("932316af-16b6-41bf-8ef1-9f202f735f37") },
                    { new Guid("a6cc8939-adac-40fc-ac60-8444305639ec"), new Guid("cfe6ffcb-ea08-4bb5-9f9d-76ae099ebd27"), "https://www.google.com", new DateTime(2023, 6, 4, 15, 14, 48, 692, DateTimeKind.Utc).AddTicks(9311), "It is in construction work!", new Guid("49b88907-af06-4c33-9276-94c32d3cd3f2") },
                    { new Guid("ab0ffafb-e017-4b0e-a800-b3083c218386"), new Guid("128cdce3-b7c3-4ebd-8b4e-9e2fba9c30a4"), "https://www.google.com", new DateTime(2023, 6, 4, 15, 14, 48, 692, DateTimeKind.Utc).AddTicks(9326), "The place smells bad!", new Guid("932316af-16b6-41bf-8ef1-9f202f735f37") },
                    { new Guid("c3c40450-c6e6-4000-b03b-12178b5feef9"), new Guid("cfe6ffcb-ea08-4bb5-9f9d-76ae099ebd27"), "https://www.google.com", new DateTime(2023, 6, 4, 15, 14, 48, 692, DateTimeKind.Utc).AddTicks(9329), "Good path with awesome view!", new Guid("932316af-16b6-41bf-8ef1-9f202f735f37") },
                    { new Guid("d825b44f-89aa-4798-a2d5-573ed40d7a57"), new Guid("44a6aac2-20b8-44cf-b283-f80895b8768c"), "https://www.google.com", new DateTime(2023, 6, 4, 15, 14, 48, 692, DateTimeKind.Utc).AddTicks(9301), "I cant see the building!", new Guid("932316af-16b6-41bf-8ef1-9f202f735f37") }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Administrators_Email",
                table: "Administrators",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Clients_Email",
                table: "Clients",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Clients_UserLocationID",
                table: "Clients",
                column: "UserLocationID");

            migrationBuilder.CreateIndex(
                name: "IX_Courses_EndPointID",
                table: "Courses",
                column: "EndPointID");

            migrationBuilder.CreateIndex(
                name: "IX_Courses_InitialPointID",
                table: "Courses",
                column: "InitialPointID");

            migrationBuilder.CreateIndex(
                name: "IX_Feedbacks_ClientID",
                table: "Feedbacks",
                column: "ClientID");

            migrationBuilder.CreateIndex(
                name: "IX_Feedbacks_FeedbackLocationID",
                table: "Feedbacks",
                column: "FeedbackLocationID");

            migrationBuilder.CreateIndex(
                name: "IX_Points_PointLocationID",
                table: "Points",
                column: "PointLocationID");

            migrationBuilder.CreateIndex(
                name: "IX_Points_PointName",
                table: "Points",
                column: "PointName",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Administrators");

            migrationBuilder.DropTable(
                name: "Courses");

            migrationBuilder.DropTable(
                name: "Feedbacks");

            migrationBuilder.DropTable(
                name: "Points");

            migrationBuilder.DropTable(
                name: "Clients");

            migrationBuilder.DropTable(
                name: "GPSCoordinates");
        }
    }
}
