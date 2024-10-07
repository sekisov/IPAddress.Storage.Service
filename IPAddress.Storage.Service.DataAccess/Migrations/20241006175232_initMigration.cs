using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

#nullable disable

namespace IPAddress.Storage.Service.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class initMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "USER_IP_ADRESSES",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    ADDRESS = table.Column<string>(type: "longtext", nullable: true),
                    LAST_CONNECTION = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    CREATED_AT = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    CREATED_BY = table.Column<string>(type: "longtext", nullable: true),
                    UPDATED_AT = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UPDATED_BY = table.Column<string>(type: "longtext", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_USER_IP_ADRESSES", x => x.ID);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "USERS",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Username = table.Column<string>(type: "longtext", nullable: true),
                    CREATED_AT = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    CREATED_BY = table.Column<string>(type: "longtext", nullable: true),
                    UPDATED_AT = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UPDATED_BY = table.Column<string>(type: "longtext", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_USERS", x => x.ID);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "IP_ADDRESSES_OF_USERES",
                columns: table => new
                {
                    UserIPAddressesId = table.Column<long>(type: "bigint", nullable: false),
                    UsersId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IP_ADDRESSES_OF_USERES", x => new { x.UserIPAddressesId, x.UsersId });
                    table.ForeignKey(
                        name: "FK_IP_ADDRESSES_OF_USERES_USERS_UsersId",
                        column: x => x.UsersId,
                        principalTable: "USERS",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_IP_ADDRESSES_OF_USERES_USER_IP_ADRESSES_UserIPAddressesId",
                        column: x => x.UserIPAddressesId,
                        principalTable: "USER_IP_ADRESSES",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_IP_ADDRESSES_OF_USERES_UsersId",
                table: "IP_ADDRESSES_OF_USERES",
                column: "UsersId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "IP_ADDRESSES_OF_USERES");

            migrationBuilder.DropTable(
                name: "USERS");

            migrationBuilder.DropTable(
                name: "USER_IP_ADRESSES");
        }
    }
}
