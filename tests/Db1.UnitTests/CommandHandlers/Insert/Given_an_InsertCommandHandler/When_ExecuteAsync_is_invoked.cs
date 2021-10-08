using System;
using System.Threading.Tasks;
using Db1.CommandHandlers;
using Db1.CommandHandlers.Abstractions;
using Db1.FileSystem.Abstractions;
using FakeItEasy;
using Newtonsoft.Json;
using Xunit;

namespace Db1.UnitTests.CommandHandlers.Insert.Given_an_InsertCommandHandler
{
    public class When_ExecuteAsync_is_invoked
    {
        private readonly IFileSystemHelper _fileSystemHelper;
        private readonly InsertCommandHandler _sut;
        private readonly JsonSerializerSettings _serializerSettings = new JsonSerializerSettings
        {
            TypeNameHandling = TypeNameHandling.Auto
        };

        public When_ExecuteAsync_is_invoked()
        {
            _fileSystemHelper = A.Fake<IFileSystemHelper>();
            _sut = new InsertCommandHandler(_fileSystemHelper);
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
        public async Task Should_throw_InvalidOperationException_when_command_is_not_of_type_InsertCommand()
        {
            // Arrange
            var commandFake = A.Fake<IDb1Command>();

            Task Action() => _sut.ExecuteAsync(commandFake);

            // Assert
            var ex = await Assert.ThrowsAsync<InvalidOperationException>(Action);
            Assert.Equal($"This handler works only with commands of type '{nameof(InsertCommand)}'.", ex.Message);
        }
    }
}