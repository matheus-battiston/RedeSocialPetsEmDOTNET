using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Instapet.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialProject : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tb_usuario",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "TEXT", nullable: false),
                    nome = table.Column<string>(type: "varchar(255)", nullable: false),
                    email = table.Column<string>(type: "varchar(255)", nullable: false),
                    dataNascimento = table.Column<DateOnly>(type: "date", nullable: false),
                    cpf = table.Column<string>(type: "varchar(8)", nullable: false),
                    urlFotoPerfil = table.Column<string>(type: "varchar(512)", nullable: false),
                    apelido = table.Column<string>(type: "varchar(50)", nullable: true),
                    ativo = table.Column<bool>(type: "INTEGER", nullable: false),
                    senha = table.Column<string>(type: "varchar(128)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_usuario", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "tb_pedido_amizade",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "TEXT", nullable: false),
                    requerente_id = table.Column<Guid>(type: "TEXT", nullable: false),
                    requisitado_id = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_pedido_amizade", x => x.id);
                    table.ForeignKey(
                        name: "FK_tb_pedido_amizade_tb_usuario_requerente_id",
                        column: x => x.requerente_id,
                        principalTable: "tb_usuario",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tb_pedido_amizade_tb_usuario_requisitado_id",
                        column: x => x.requisitado_id,
                        principalTable: "tb_usuario",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tb_post",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "TEXT", nullable: false),
                    urlImagem = table.Column<string>(type: "varchar(512)", nullable: false),
                    legenda = table.Column<string>(type: "varchar(2000)", nullable: true),
                    privado = table.Column<bool>(type: "bool", nullable: false),
                    horario = table.Column<DateTime>(type: "timestamp", nullable: false),
                    UsuarioId = table.Column<Guid>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_post", x => x.id);
                    table.ForeignKey(
                        name: "FK_tb_post_tb_usuario_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "tb_usuario",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "UsuarioAmigos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    UsuarioId = table.Column<Guid>(type: "TEXT", nullable: false),
                    AmigoId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsuarioAmigos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UsuarioAmigos_tb_usuario_AmigoId",
                        column: x => x.AmigoId,
                        principalTable: "tb_usuario",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UsuarioAmigos_tb_usuario_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "tb_usuario",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tb_comentario",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "TEXT", nullable: false),
                    id_usuario = table.Column<Guid>(type: "TEXT", nullable: false),
                    id_post = table.Column<Guid>(type: "TEXT", nullable: false),
                    mensagem = table.Column<string>(type: "varchar(512)", nullable: false),
                    horario = table.Column<DateTime>(type: "timestamp", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_comentario", x => x.id);
                    table.ForeignKey(
                        name: "FK_tb_comentario_tb_post_id_post",
                        column: x => x.id_post,
                        principalTable: "tb_post",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tb_comentario_tb_usuario_id_usuario",
                        column: x => x.id_usuario,
                        principalTable: "tb_usuario",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tb_curtida",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "TEXT", nullable: false),
                    IdPost = table.Column<Guid>(type: "TEXT", nullable: false),
                    IdUsuario = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_curtida", x => x.id);
                    table.ForeignKey(
                        name: "FK_tb_curtida_tb_post_IdPost",
                        column: x => x.IdPost,
                        principalTable: "tb_post",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tb_curtida_tb_usuario_IdUsuario",
                        column: x => x.IdUsuario,
                        principalTable: "tb_usuario",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tb_comentario_id_post",
                table: "tb_comentario",
                column: "id_post");

            migrationBuilder.CreateIndex(
                name: "IX_tb_comentario_id_usuario",
                table: "tb_comentario",
                column: "id_usuario");

            migrationBuilder.CreateIndex(
                name: "IX_tb_curtida_IdPost",
                table: "tb_curtida",
                column: "IdPost");

            migrationBuilder.CreateIndex(
                name: "IX_tb_curtida_IdUsuario",
                table: "tb_curtida",
                column: "IdUsuario");

            migrationBuilder.CreateIndex(
                name: "IX_tb_pedido_amizade_requerente_id",
                table: "tb_pedido_amizade",
                column: "requerente_id");

            migrationBuilder.CreateIndex(
                name: "IX_tb_pedido_amizade_requisitado_id",
                table: "tb_pedido_amizade",
                column: "requisitado_id");

            migrationBuilder.CreateIndex(
                name: "IX_tb_post_UsuarioId",
                table: "tb_post",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_tb_usuario_email",
                table: "tb_usuario",
                column: "email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UsuarioAmigos_AmigoId",
                table: "UsuarioAmigos",
                column: "AmigoId");

            migrationBuilder.CreateIndex(
                name: "IX_UsuarioAmigos_UsuarioId",
                table: "UsuarioAmigos",
                column: "UsuarioId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tb_comentario");

            migrationBuilder.DropTable(
                name: "tb_curtida");

            migrationBuilder.DropTable(
                name: "tb_pedido_amizade");

            migrationBuilder.DropTable(
                name: "UsuarioAmigos");

            migrationBuilder.DropTable(
                name: "tb_post");

            migrationBuilder.DropTable(
                name: "tb_usuario");
        }
    }
}
