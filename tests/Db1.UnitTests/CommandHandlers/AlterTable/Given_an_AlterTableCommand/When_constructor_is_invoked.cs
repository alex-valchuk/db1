using System.Linq;
using Db1.BuildingBlocks.Columns;
using Db1.CommandHandlers.AlterTable;
using Xunit;

namespace Db1.UnitTests.CommandHandlers.AlterTable.Given_an_AlterTableCommand
{
    public class When_constructor_is_invoked
    {
        [Fact]
        public void Should_parse_tokens_properly_for_add_columns()
        {
            // Arrange
            var commandText = "ALTER TABLE User ADD COLUMNS (RoleId integer);";

            // Act
            
            var sut = new AlterTableCommand(commandText.Split(' '));

            // Assert
            var tableDef = sut.TableDefinition;
            Assert.Equal("user", tableDef.TableName.ToLower());
            Assert.Equal("add", sut.Action.ToLower());

            var columns = tableDef.Columns.ToArray();
            Assert.Single(columns);
            
            var roleIdColumn = columns[0] as IntegerColumn;
            Assert.NotNull(roleIdColumn);
            Assert.Equal("roleid", roleIdColumn.Name.ToLower());
        }

        [Fact]
        public void Should_parse_tokens_properly_for_remove_columns()
        {
            // Arrange
            var commandText = "ALTER TABLE User REMOVE COLUMNS (RoleId integer);";

            // Act
            var sut = new AlterTableCommand(commandText.Split(' '));

            // Assert
            var tableDef = sut.TableDefinition;
            Assert.Equal("user", tableDef.TableName.ToLower());
            Assert.Equal("remove", sut.Action.ToLower());

            var columns = tableDef.Columns.ToArray();
            Assert.Single(columns);
            
            var roleIdColumn = columns[0] as IntegerColumn;
            Assert.NotNull(roleIdColumn);
            Assert.Equal("roleid", roleIdColumn.Name);
        }
    }
}