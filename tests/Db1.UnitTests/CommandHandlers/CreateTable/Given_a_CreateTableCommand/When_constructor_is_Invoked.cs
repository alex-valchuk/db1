using System.Linq;
using Db1.BuildingBlocks.Columns;
using Db1.CommandHandlers;
using Xunit;

namespace Db1.UnitTests.Given_a_CreateTableCommand
{
    public class When_constructor_is_Invoked
    {
        [Fact]
        public void Should_parse_tokens_properly()
        {
            // Arrange
            var commandText = "CREATE TABLE User with Columns (id integer, name varchar(100));";

            // Act
            var sut = new CreateTableCommand(commandText.Split(' '));
            
            // Assert
            var tableDef = sut.TableDefinition;
            Assert.Equal("user", tableDef.TableName.ToLower());

            var columns = tableDef.Columns.ToArray();
            Assert.Equal(2, columns.Length);
            
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