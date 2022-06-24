using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Promart.API.Migrations
{
    public partial class InsertWorkshops : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@$"INSERT INTO Workshops (Guid, Name, IsActivated)
                VALUES ('{Guid.NewGuid()}', 'Reforço Escolar', 1)");

            migrationBuilder.Sql(@$"INSERT INTO Workshops (Guid, Name, IsActivated)
                VALUES ('{Guid.NewGuid()}', 'Balé', 1)");

            migrationBuilder.Sql(@$"INSERT INTO Workshops (Guid, Name, IsActivated)
                VALUES ('{Guid.NewGuid()}', 'Informática', 1)");

            migrationBuilder.Sql(@$"INSERT INTO Workshops (Guid, Name, IsActivated)
                VALUES ('{Guid.NewGuid()}', 'Libras', 1)");

            migrationBuilder.Sql(@$"INSERT INTO Workshops (Guid, Name, IsActivated)
                VALUES ('{Guid.NewGuid()}', 'Espanhol', 1)");

            migrationBuilder.Sql(@$"INSERT INTO Workshops (Guid, Name, IsActivated)
                VALUES ('{Guid.NewGuid()}', 'Violão', 1)");

            migrationBuilder.Sql(@$"INSERT INTO Workshops (Guid, Name, IsActivated)
                VALUES ('{Guid.NewGuid()}', 'Flauta', 1)");

            migrationBuilder.Sql(@$"INSERT INTO Workshops (Guid, Name, IsActivated)
                VALUES ('{Guid.NewGuid()}', 'Orientaão para a vida', 1)");
            
            migrationBuilder.Sql(@$"INSERT INTO Workshops (Guid, Name, IsActivated)
                VALUES ('{Guid.NewGuid()}', 'Educação Física', 1)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"DELETE FROM Workshops");
        }
    }
}
