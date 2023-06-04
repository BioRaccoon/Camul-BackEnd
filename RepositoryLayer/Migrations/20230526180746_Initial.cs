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
                    { new Guid("aa2f10e2-a65b-4ef8-87b2-66b37b971872"), 21, "ku", "thatsHowMafia7$Works", "adminer@gmail.com", false, "thatsHowMafia7$Works", "admin" },
                    { new Guid("f3f97f86-f1de-4d67-a62b-21e60bb3f5ec"), 18, "hg", "itsTheEndOfThe$Pasta1", "mariopasta@gmail.com", false, "itsTheEndOfThe$Pasta1", "Mario" }
                });

            migrationBuilder.InsertData(
                table: "GPSCoordinates",
                columns: new[] { "LocationID", "Latitude", "Longitude" },
                values: new object[,]
                {
                    { new Guid("01f4542f-73f0-4b47-9111-c6f2a4e9d61d"), 41.179084789896223, -8.6087289902487836 },
                    { new Guid("1631324d-5da4-4c04-93a2-a320f5bdb14d"), 41.179469643235471, -8.6059597348474224 },
                    { new Guid("1dcccab2-7df4-420b-9978-55943542df46"), 41.1794081572219, -8.6066880570955586 },
                    { new Guid("257246ee-7845-439d-b51a-7fb3e7136ee4"), 41.17944250803648, -8.6086616434248153 },
                    { new Guid("32323b97-e58d-49ee-98f2-2f10f1be7111"), 41.178843863356043, -8.60702514650362 },
                    { new Guid("3aef4f84-dc03-4a24-91c3-8936921907d6"), 41.177967145422194, -8.6085062262311105 },
                    { new Guid("3bfcb259-34bb-4feb-ad61-3654097adb62"), 41.179435815023893, -8.6070977092565961 },
                    { new Guid("44fa0f16-c58d-430c-ab5a-97ef4d9c6f61"), 41.177711788665363, -8.6077586936904744 },
                    { new Guid("5313e5aa-6aea-4262-8a44-b5e1d01c8962"), 0.0, 0.0 },
                    { new Guid("54ab0c02-5de2-41ff-8915-4e03e01f09c3"), 41.178602169673589, -8.6089645852372154 },
                    { new Guid("6e723e95-6a99-4ae1-aea2-64c4442c3ce9"), 41.178984721995306, -8.6083692036936128 },
                    { new Guid("7b2f6f93-7b9d-4812-abce-a6c49ea5a345"), 41.17833004000633, -8.6068877514979683 },
                    { new Guid("9218dee3-812a-44f7-a117-0456eb888a82"), 41.178104599027193, -8.6079171398224492 },
                    { new Guid("98489606-551b-420c-8096-462e0deadcac"), 41.177841263041209, -8.6079983063935739 },
                    { new Guid("a3283abe-4d51-41e7-b3f0-fc13dd773c25"), 41.178472726585674, -8.6087807216442869 },
                    { new Guid("b8513d2c-9be7-4906-850d-4299056c438e"), 41.179110097095872, -8.6070945594407782 },
                    { new Guid("d69dfd7e-9d29-4ea4-a057-65d1d7d4f581"), 41.179276075812993, -8.6078482087685995 },
                    { new Guid("dbced685-8e74-4f2b-9178-c0fbc04cf564"), 41.177564994929561, -8.6083047981405159 },
                    { new Guid("dfc50880-0c57-4d33-8b7b-0b5d99ba4d7c"), 41.178155377732104, -8.6083497177966759 },
                    { new Guid("eebf553f-a4ae-4047-a969-a9096bbb24f6"), 41.178773264549697, -8.608947821523687 },
                    { new Guid("feec28de-b0e1-4ed6-97b4-b3964aa45f1c"), 41.178429896021797, -8.6075151873977322 }
                });

            migrationBuilder.InsertData(
                table: "Clients",
                columns: new[] { "UserID", "Age", "Avatar", "ConfirmPassword", "Email", "Incapacity", "IsActive", "Password", "UserLocationID", "Username" },
                values: new object[,]
                {
                    { new Guid("6b7310d0-42e7-4498-9aec-4260e10fa080"), 22, "yh", "sirvaPureComArroz$1", "ceftigas@gmail.com", "", true, "sirvaPureComArroz$1", new Guid("5313e5aa-6aea-4262-8a44-b5e1d01c8962"), "Tiago" },
                    { new Guid("83bb0eaf-5dca-40ac-9960-1d7de79435c0"), 16, "bh", "queijo123ComBacon$", "diogomorfador@gmail.com", "", true, "queijo123ComBacon$", new Guid("5313e5aa-6aea-4262-8a44-b5e1d01c8962"), "Diogo" },
                    { new Guid("9d064a8e-ebb2-4d40-97b6-bb79a5d8452d"), 30, "gv", "euGostoDeCamul123$", "carlitosbritos@gmail.com", "wheelChair", true, "euGostoDeCamul123$", new Guid("a3283abe-4d51-41e7-b3f0-fc13dd773c25"), "Carlos" },
                    { new Guid("f3dd50ce-bb1e-4c88-9e9a-2e51f75c4434"), 22, "as", "forTheMotherFoca@7", "josefino@gmail.com", "", true, "forTheMotherFoca@7", new Guid("5313e5aa-6aea-4262-8a44-b5e1d01c8962"), "Joseph" }
                });

            migrationBuilder.InsertData(
                table: "Points",
                columns: new[] { "PointID", "Description", "Image360", "PointLocationID", "PointName" },
                values: new object[,]
                {
                    { new Guid("3a40db35-7852-40cd-ae6d-83c87ff9851c"), "Beacon from Building F", "bj", new Guid("d69dfd7e-9d29-4ea4-a057-65d1d7d4f581"), "Beacon F" },
                    { new Guid("4ed93ab5-319c-473e-a04d-c04ea9e237ee"), "Auditorio Magno Building", "hb", new Guid("54ab0c02-5de2-41ff-8915-4e03e01f09c3"), "Auditório Magno" },
                    { new Guid("624e7d79-c54a-4905-b377-f32e137a4cb6"), "Beacon from Building G", "rh", new Guid("dbced685-8e74-4f2b-9178-c0fbc04cf564"), "Beacon G" },
                    { new Guid("67efb852-f501-4755-a6e9-c5c83b05b941"), "Beacon from Building I", "gb", new Guid("9218dee3-812a-44f7-a117-0456eb888a82"), "Beacon I" },
                    { new Guid("6f7fc315-cd70-4914-b205-e99d731eb858"), "Beacon from Building E", "uh", new Guid("1631324d-5da4-4c04-93a2-a320f5bdb14d"), "Beacon E" },
                    { new Guid("70e264fb-b93c-4870-acb9-9027cedfe93e"), "Beacon from Building L", "cd", new Guid("98489606-551b-420c-8096-462e0deadcac"), "Beacon L" },
                    { new Guid("946e4a21-bc98-4bb0-b8e6-e2ea7e1fec6a"), "Beacon from Building H", "jg", new Guid("3aef4f84-dc03-4a24-91c3-8936921907d6"), "Beacon H" },
                    { new Guid("98d31708-cc3f-4701-b51a-ffaf87a67a42"), "Entry from Building H", "rt", new Guid("dfc50880-0c57-4d33-8b7b-0b5d99ba4d7c"), "Beacon Entry Main Gate" },
                    { new Guid("9aa08875-8921-4c23-b67f-1881a2e32782"), "Beacon from Building A", "sd", new Guid("a3283abe-4d51-41e7-b3f0-fc13dd773c25"), "Beacon A" },
                    { new Guid("a47c8ec8-f04c-48ff-b9ac-13ac1f760e4a"), "Beacon from Building C", "jy", new Guid("32323b97-e58d-49ee-98f2-2f10f1be7111"), "Beacon C" },
                    { new Guid("b9040891-d05d-4d45-b074-b9f229df5136"), "Beacon from Building B", "ng", new Guid("44fa0f16-c58d-430c-ab5a-97ef4d9c6f61"), "Beacon B" },
                    { new Guid("ba33e56c-240e-414a-b73b-077827c0b6b8"), "Entry from parking car next to Auditorio Magno", "ws", new Guid("eebf553f-a4ae-4047-a969-a9096bbb24f6"), "Beacon Entry Backs" },
                    { new Guid("d7ccb306-cc59-455a-8c98-3c9f35f8a4b9"), "Entry from building E", "gf", new Guid("7b2f6f93-7b9d-4812-abce-a6c49ea5a345"), "Beacon Front Entry" },
                    { new Guid("d9e82cd6-5a83-47c7-a12e-cf50ae3e041d"), "Beacon from Building K", "db", new Guid("6e723e95-6a99-4ae1-aea2-64c4442c3ce9"), "Beacon K" },
                    { new Guid("dbc624be-5f60-4886-a01a-7676708a0313"), "Beacon from Building J", "ad", new Guid("feec28de-b0e1-4ed6-97b4-b3964aa45f1c"), "Beacon J" },
                    { new Guid("f9c54552-dd35-41e2-81ca-3e6b161e036d"), "Beacon from Building D", "gv", new Guid("b8513d2c-9be7-4906-850d-4299056c438e"), "Beacon D" }
                });

            migrationBuilder.InsertData(
                table: "Courses",
                columns: new[] { "CourseID", "Description", "EndPointID", "IncapacityAcessible", "InitialPointID" },
                values: new object[,]
                {
                    { new Guid("097ad3c5-5bcb-48c4-bba8-538a1b61cc6c"), "Path from I to A", new Guid("9aa08875-8921-4c23-b67f-1881a2e32782"), true, new Guid("67efb852-f501-4755-a6e9-c5c83b05b941") },
                    { new Guid("12bf1d60-d833-414f-832f-49db5b58e591"), "Path from G to H", new Guid("946e4a21-bc98-4bb0-b8e6-e2ea7e1fec6a"), true, new Guid("624e7d79-c54a-4905-b377-f32e137a4cb6") },
                    { new Guid("1f9e0996-4764-4cb3-8bd6-fc1f19fceea2"), "Path from B to G", new Guid("624e7d79-c54a-4905-b377-f32e137a4cb6"), true, new Guid("b9040891-d05d-4d45-b074-b9f229df5136") },
                    { new Guid("3505fb67-2834-44ad-84c4-1089e77e8c10"), "Path from H to A", new Guid("9aa08875-8921-4c23-b67f-1881a2e32782"), true, new Guid("946e4a21-bc98-4bb0-b8e6-e2ea7e1fec6a") },
                    { new Guid("471a79f4-ef9c-48c7-aa39-7e8f3dd05227"), "Path from H to G", new Guid("624e7d79-c54a-4905-b377-f32e137a4cb6"), true, new Guid("946e4a21-bc98-4bb0-b8e6-e2ea7e1fec6a") },
                    { new Guid("4b758ac8-a8dc-4e80-85dc-efee63ee69da"), "Path from B to I", new Guid("67efb852-f501-4755-a6e9-c5c83b05b941"), true, new Guid("b9040891-d05d-4d45-b074-b9f229df5136") },
                    { new Guid("4ba06619-5907-404a-9bb2-65524aefd01e"), "Path from A to AuditorioMagno", new Guid("4ed93ab5-319c-473e-a04d-c04ea9e237ee"), true, new Guid("9aa08875-8921-4c23-b67f-1881a2e32782") },
                    { new Guid("7b3e78c3-814f-4b97-b647-6834783ebada"), "Path from I to B", new Guid("b9040891-d05d-4d45-b074-b9f229df5136"), true, new Guid("67efb852-f501-4755-a6e9-c5c83b05b941") },
                    { new Guid("99b6dfce-cf03-4fb1-b153-2d85a5c978c7"), "Path from A to I", new Guid("67efb852-f501-4755-a6e9-c5c83b05b941"), true, new Guid("9aa08875-8921-4c23-b67f-1881a2e32782") },
                    { new Guid("a0304656-1c57-45d7-9f2f-4dea821fdc69"), "Path from AuditorioMagno to A ", new Guid("9aa08875-8921-4c23-b67f-1881a2e32782"), true, new Guid("4ed93ab5-319c-473e-a04d-c04ea9e237ee") },
                    { new Guid("a5e9c37e-3c8f-482b-bdf7-2e6f6a52706e"), "Path from G to B", new Guid("b9040891-d05d-4d45-b074-b9f229df5136"), true, new Guid("624e7d79-c54a-4905-b377-f32e137a4cb6") },
                    { new Guid("b5570534-f022-4277-879d-26ec309d2e1a"), "Path from A to H", new Guid("946e4a21-bc98-4bb0-b8e6-e2ea7e1fec6a"), true, new Guid("9aa08875-8921-4c23-b67f-1881a2e32782") },
                    { new Guid("b5b8b886-2c3c-4287-bcf8-a541a1936446"), "Path from H to AuditorioMagno", new Guid("4ed93ab5-319c-473e-a04d-c04ea9e237ee"), true, new Guid("946e4a21-bc98-4bb0-b8e6-e2ea7e1fec6a") },
                    { new Guid("e35ca02f-a0bc-4f98-91f3-ba78c55fe2bd"), "Path from AuditorioMagno to H", new Guid("946e4a21-bc98-4bb0-b8e6-e2ea7e1fec6a"), true, new Guid("4ed93ab5-319c-473e-a04d-c04ea9e237ee") }
                });

            migrationBuilder.InsertData(
                table: "Feedbacks",
                columns: new[] { "FeedbackID", "ClientID", "CloudFolderURL", "FeedbackDate", "FeedbackDescription", "FeedbackLocationID" },
                values: new object[,]
                {
                    { new Guid("72ff822d-fbf1-48b4-b364-c138b6c1a914"), new Guid("9d064a8e-ebb2-4d40-97b6-bb79a5d8452d"), "https://www.google.com", new DateTime(2023, 5, 26, 18, 7, 46, 323, DateTimeKind.Utc).AddTicks(6378), "Good path with awesome view!", new Guid("5313e5aa-6aea-4262-8a44-b5e1d01c8962") },
                    { new Guid("797fe6df-1828-41ba-a5df-9d8e0a3809da"), new Guid("9d064a8e-ebb2-4d40-97b6-bb79a5d8452d"), "https://www.google.com", new DateTime(2023, 5, 26, 18, 7, 46, 323, DateTimeKind.Utc).AddTicks(6363), "It is in construction work!", new Guid("a3283abe-4d51-41e7-b3f0-fc13dd773c25") },
                    { new Guid("7c0524a8-0f24-4270-bf75-b7ce63feb5a9"), new Guid("83bb0eaf-5dca-40ac-9960-1d7de79435c0"), "https://www.google.com", new DateTime(2023, 5, 26, 18, 7, 46, 323, DateTimeKind.Utc).AddTicks(6375), "The place smells bad!", new Guid("5313e5aa-6aea-4262-8a44-b5e1d01c8962") },
                    { new Guid("8d02939f-63fa-464a-a0ca-cb09b6bebeb2"), new Guid("6b7310d0-42e7-4498-9aec-4260e10fa080"), "https://www.google.com", new DateTime(2023, 5, 26, 18, 7, 46, 323, DateTimeKind.Utc).AddTicks(6371), "The sight is awful!", new Guid("5313e5aa-6aea-4262-8a44-b5e1d01c8962") },
                    { new Guid("9b7cd70b-9276-4278-acce-59b12bbd7a68"), new Guid("f3dd50ce-bb1e-4c88-9e9a-2e51f75c4434"), "https://www.google.com", new DateTime(2023, 5, 26, 18, 7, 46, 323, DateTimeKind.Utc).AddTicks(6353), "I cant see the building!", new Guid("5313e5aa-6aea-4262-8a44-b5e1d01c8962") },
                    { new Guid("ab003761-232c-4c97-acf5-826065e56c56"), new Guid("f3dd50ce-bb1e-4c88-9e9a-2e51f75c4434"), "https://www.google.com", new DateTime(2023, 5, 26, 18, 7, 46, 323, DateTimeKind.Utc).AddTicks(6381), "Thank you for the reception from ISEP!", new Guid("5313e5aa-6aea-4262-8a44-b5e1d01c8962") },
                    { new Guid("e0e94b6c-a75e-4a69-8c1e-b2f7885151b2"), new Guid("83bb0eaf-5dca-40ac-9960-1d7de79435c0"), "https://www.google.com", new DateTime(2023, 5, 26, 18, 7, 46, 323, DateTimeKind.Utc).AddTicks(6367), "The building is different.", new Guid("5313e5aa-6aea-4262-8a44-b5e1d01c8962") }
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
