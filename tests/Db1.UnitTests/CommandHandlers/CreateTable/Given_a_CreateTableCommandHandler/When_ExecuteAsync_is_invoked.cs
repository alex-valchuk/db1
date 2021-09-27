using System;
using System.Threading.Tasks;
using Db1.BuildingBlocks;
using Db1.BuildingBlocks.Columns;
using Db1.CommandHandlers;
using Db1.CommandHandlers.Abstractions;
using Db1.Exceptions;
using Db1.FileSystem.Abstractions;
using FakeItEasy;
using Newtonsoft.Json;
using Xunit;

namespace Db1.UnitTests.CommandHandlers.CreateTable.Given_a_CreateTableCommandHandler
{
    public class When_ExecuteAsync_is_invoked
    {
        private readonly IFileSystemHelper _fileSystemHelper;
        private readonly CreateTableCommandHandler _sut;
        private readonly JsonSerializerSettings _serializerSettings = new JsonSerializerSettings
        {
            TypeNameHandling = TypeNameHandling.Auto
        };

        public When_ExecuteAsync_is_invoked()
        {
            _fileSystemHelper = A.Fake<IFileSystemHelper>();
            _sut = new CreateTableCommandHandler(_fileSystemHelper);
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
        public async Task Should_throw_InvalidOperationException_when_command_is_not_of_type_CreateTableCommand()
        {
            // Arrange
            var commandFake = A.Fake<IDb1Command>();

            Task Action() => _sut.ExecuteAsync(commandFake);

            // Assert
            var ex = await Assert.ThrowsAsync<InvalidOperationException>(Action);
            Assert.Equal($"This handler works only with commands of type '{nameof(CreateTableCommand)}'.", ex.Message);
        }

        [Fact]
        public async Task Should_throw_DuplicationException_when_table_with_specified_name_already_exists()
        {
            // Arrange
            var createTableDefinition = new TableDefinition("some_table");
            var command = new CreateTableCommand(createTableDefinition);

            A
                .CallTo(() => _fileSystemHelper.Exists($"{createTableDefinition.TableName}.tbl"))
                .Returns(true);

            Task Action() => _sut.ExecuteAsync(command);

            // Assert
            var ex = await Assert.ThrowsAsync<DuplicationException>(Action);
            Assert.Equal($"Table with name '{createTableDefinition.TableName}' already exists.", ex.Message);
        }

        [Fact]
        public async Task Should_create_table_when_everything_is_well()
        {
            // Arrange
            var createTableDefinition = new TableDefinition("some_table");
            createTableDefinition.AddColumn(new IntegerColumn("id"));
            createTableDefinition.AddColumn(new VarcharColumn("Name", 255));

            var command = new CreateTableCommand(createTableDefinition);
            var fileName = $"{createTableDefinition.TableName}.tbl";
            
            A
                .CallTo(() => _fileSystemHelper.Exists(fileName))
                .Returns(false);
            
            // Act
            await _sut.ExecuteAsync(command);
            
            // Assert
            A
                .CallTo(() => _fileSystemHelper.WriteAllTextAsync(fileName, JsonConvert.SerializeObject(createTableDefinition, _serializerSettings)))
                .MustHaveHappenedOnceExactly();
        }
    }
}