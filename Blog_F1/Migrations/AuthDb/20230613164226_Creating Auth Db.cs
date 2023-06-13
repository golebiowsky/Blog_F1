using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Blog_F1.Migrations.AuthDb
{
    /// <inheritdoc />
    public partial class CreatingAuthDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "0e046456-e88a-4355-a1b2-741d526f849c",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "49c27b9a-2dfd-4dcb-8b0a-e2cd364b7cab", "AQAAAAIAAYagAAAAEO6OLwzccjJuiRgoxJpeCgSoNRQincdzh5Mveh4tXEGukJ5Q+PMI468daIbWpsTLHg==", "954dbb39-43fc-442c-83a0-f781ce153a56" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "0e046456-e88a-4355-a1b2-741d526f849c",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "0f242359-21dc-4853-91a8-f83297fa426e", "AQAAAAIAAYagAAAAEGs8csddYnNhjoMX5qY6JybzLahaMo7bJ9jJL5I3WzahqYgFIc0hkqUbygvzXr57+w==", "4f421490-3e8a-4e01-b137-4a9554f0c07b" });
        }
    }
}
