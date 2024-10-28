using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ToDoApi.Migrations
{
    /// <inheritdoc />
    public partial class spGetTodos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var sp = @"
    CREATE PROCEDURE GetTodos
    AS
    BEGIN
        SET NOCOUNT ON;
        SELECT * FROM Todos;
    END;
";
                migrationBuilder.Sql(sp);
         }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
