using Db1.BuildingBlocks.Columns;
using Db1.CommandHandlers;
using Xunit;

namespace Db1.UnitTests.CommandHandlers.Insert.Given_an_InsertCommand
{
    public class When_constructor_is_invoked
    {
        [Fact]
        public void Should_parse_tokens_properly()
        {
            // Arrange
            var commandText = @"INSERT INTO User [
                id,
                name,
                roleid,
                category
            ]
            ROWS [
             ( 1, 'Alex', 1, 'creator' ),
             ( 2, 'Sam', 2, 'director' ),
             ( 3, 'Tom', 3, 'visitor' ),
            ];";

            // Act
            var sut = new InsertCommand(commandText.Split(' '));
            
            // Assert
            Assert.Equal("user", sut.TableName);
            Assert.Equal(new[] { "id", "name", "roleid", "category" }, sut.Columns);

            var rows = sut.Rows;
            Assert.Equal(3, rows.Length);
            
            Assert.Equal(, rows[0]);
            var idColumn = columns[0] as IntegerColumn;
            Assert.NotNull(idColumn);
            Assert.Equal("id", idColumn.Name);

            var nameColumn = columns[1] as VarcharColumn;
            Assert.NotNull(nameColumn);
            Assert.Equal("name", nameColumn.Name);
            Assert.Equal(100, nameColumn.Size);
        }
    }
}