using System;
using System.Threading.Tasks;
using Db1.BuildingBlocks;
using Db1.CommandHandlers.Abstractions;
using Db1.CommandHandlers.AlterTable;
using Db1.Exceptions;
using Db1.FileSystem.Abstractions;
using FakeItEasy;
using Xunit;

namespace Db1.UnitTests.CommandHandlers.AlterTable.Given_an_AlterTableCommandHandler
{
    public class When_ExecuteAsync_is_invoked
    {
        private readonly IFileSystemHelper _fileSystemHelper;
        private readonly AlterTableCommandHandler _sut;

        public When_ExecuteAsync_is_invoked()
        {
            _fileSystemHelper = A.Fake<IFileSystemHelper>();
            _sut = new AlterTableCommandHandler(_fileSystemHelper);
        }

        [Fact]
        public async Task Should_throw_ArgumentNullException_when_command_is_null()
        {
            // Arrange
            IDb1Command command = null;

            Task Action() => _sut.ExecuteAsync(command);

            // Assert
            var ex = await Assert.ThrowsAsync<ArgumentNullException>(Action);
            Assert.Equal(nameof(command), ex.ParamName);
        }

        [Fact]
        public async Task Should_throw_InvalidOperationException_when_command_is_not_of_type_AlterTableCommand()
        {
            // Arrange
            var commandFake = A.Fake<IDb1Command>();

            Task Action() => _sut.ExecuteAsync(commandFake);

            // Assert
            var ex = await Assert.ThrowsAsync<InvalidOperationException>(Action);
            Assert.Equal($"This handler works only with commands of type '{nameof(AlterTableCommand)}'.", ex.Message);
        }

        [Fact]
        public async Task Should_throw_NonExistingResourceException_when_table_with_specified_name_does_not_exist()
        {
            // Arrange
            var tableDefinition = new TableDefinition
            {
                TableName = "some_table"
            };
            var command = new AlterTableCommand("remove", tableDefinition);

            A.CallTo(() => _fileSystemHelper.Exists($"{tableDefinition.TableName}.tbl")).Returns(false);

            Task Action() => _sut.ExecuteAsync(command);

            // Assert
            var ex = await Assert.ThrowsAsync<NonExistingResourceException>(Action);
            Assert.Equal($"Table with name '{tableDefinition.TableName}' does not exists.", ex.Message);
        }

        [Fact]
        public async Task Should_throw_NotSupportedException_when_action_is_not_from_supported()
        {
            // Arrange
            var tableDefinition = new TableDefinition
            {
                TableName = "some_table"
            };
            var command = new AlterTableCommand("some_action", tableDefinition);

            A.CallTo(() => _fileSystemHelper.Exists($"{tableDefinition.TableName}.tbl")).Returns(true);

            Task Action() => _sut.ExecuteAsync(command);

            // Assert
            var ex = await Assert.ThrowsAsync<NotSupportedException>(Action);
            Assert.Equal($"The action '{command.Action}' is not supported by '{nameof(AlterTableCommandHandler)}'.", ex.Message);
        }
    }
}