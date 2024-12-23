using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Instapet.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Posts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tb_post_tb_usuario_UsuarioId",
                table: "tb_post");

            migrationBuilder.DropIndex(
                name: "IX_tb_post_UsuarioId",
                table: "tb_post");

            migrationBuilder.DropColumn(
                name: "UsuarioId",
                table: "tb_post");

            migrationBuilder.RenameColumn(
                name: "ativo",
                table: "tb_usuario",
                newName: "Ativo");

            migrationBuilder.AddColumn<Guid>(
                name: "id_usuario",
                table: "tb_post",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_tb_post_id_usuario",
                table: "tb_post",
                column: "id_usuario");

            migrationBuilder.AddForeignKey(
                name: "FK_tb_post_tb_usuario_id_usuario",
                table: "tb_post",
                column: "id_usuario",
                principalTable: "tb_usuario",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tb_post_tb_usuario_id_usuario",
                table: "tb_post");

            migrationBuilder.DropIndex(
                name: "IX_tb_post_id_usuario",
                table: "tb_post");

            migrationBuilder.DropColumn(
                name: "id_usuario",
                table: "tb_post");

            migrationBuilder.RenameColumn(
                name: "Ativo",
                table: "tb_usuario",
                newName: "ativo");

            migrationBuilder.AddColumn<Guid>(
                name: "UsuarioId",
                table: "tb_post",
                type: "TEXT",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_tb_post_UsuarioId",
                table: "tb_post",
                column: "UsuarioId");

            migrationBuilder.AddForeignKey(
                name: "FK_tb_post_tb_usuario_UsuarioId",
                table: "tb_post",
                column: "UsuarioId",
                principalTable: "tb_usuario",
                principalColumn: "id");
        }
    }
}
