using Db1.CommandHandlers;
using Xunit;

namespace Db1.UnitTests.Given_a_CreateTableCommand
{
    public class When_constructor_is_Invoked
    {
        [Fact]
        public void Should_parse_columns_properly()
        {
            // Arrange
            var commandText = "CREATE TABLE User with Columns (id integer, name varchar(100))";

            // Act
            var command = new CreateTableCommand(commandText);
            
            // Assert
            Assert.NotNull(command);
            Assert.Equal(2, command.Columns.Length);
            
            var idColumn = command.Columns[0] as IntegerColumn;
            Assert.NotNull(idColumn);
            Assert.Equal("id", idColumn.Name);

            var nameColumn = command.Columns[1] as VarcharColumn;
            Assert.NotNull(nameColumn);
            Assert.Equal("name", nameColumn.Name);
            Assert.Equal(100, nameColumn.Size);
        }
    }
}