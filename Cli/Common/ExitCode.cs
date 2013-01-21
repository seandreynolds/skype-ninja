using System;

namespace eigenein.SkypeNinja.Cli.Common
{
    /// <summary>
    /// Represents the application exit code.
    /// </summary>
    internal static class ExitCode
    {
        /// <summary>
        /// Invalid command-line options specified.
        /// </summary>
        public const int InvalidOptions = 1;

        /// <summary>
        /// Invalid URI format.
        /// </summary>
        public const int InvalidUri = 2;
        
        /// <summary>
        /// Unknown URI scheme.
        /// </summary>
        public const int UnknownUriScheme = 3;

        /// <summary>
        /// Unable to open a connector.
        /// </summary>
        public const int ConnectorOpenFailed = 4;
    }
}
