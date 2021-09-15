using System.Linq;
using Db1.Exceptions;

namespace Db1.Validators
{
    public static class TokenValidator
    {
        public static void ValidateTokenExistence(string actualToken, string expectedToken)
        {
            if (actualToken.ToLower() != expectedToken)
            {
                throw new InvalidCommandFormatException($"Invalid command string: '{expectedToken}' is expected.");
            }
        }

        public static void ValidateTokenExistence(string actualToken, params string[] expectedTokens)
        {
            if (!expectedTokens.Contains(actualToken.ToLower()))
            {
                throw new InvalidCommandFormatException($"Invalid command string: at least one from the '{string.Join(",", expectedTokens)}' tokens is expected.");
            }
        }
    }
}