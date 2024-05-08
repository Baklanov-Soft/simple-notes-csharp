using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SimpleNotes.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddLabel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Labels",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: false),
                    Color = table.Column<string>(type: "character varying(7)", maxLength: 7, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Labels", x => x.Id);
                    table.CheckConstraint("CK_Notes_ColorRegEx", "\"Color\" ~* '^#[a-f0-9]{6}$'");
                });

            migrationBuilder.CreateTable(
                name: "TreeNodeLabels",
                columns: table => new
                {
                    TreeNodeId = table.Column<Guid>(type: "uuid", nullable: false),
                    LabelId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TreeNodeLabels", x => new { x.LabelId, x.TreeNodeId });
                    table.ForeignKey(
                        name: "FK_TreeNodeLabels_Labels_LabelId",
                        column: x => x.LabelId,
                        principalTable: "Labels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TreeNodeLabels_TreeNodes_TreeNodeId",
                        column: x => x.TreeNodeId,
                        principalTable: "TreeNodes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TreeNodeLabels");

            migrationBuilder.DropTable(
                name: "Labels");
        }
    }
}
