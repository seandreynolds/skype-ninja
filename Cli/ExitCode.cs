using System;

namespace eigenein.SkypeNinja.Cli
{
    internal static class ExitCode
    {
        public const int InvalidOptions = 1;
        public const int InvalidUri = 2;
        public const int UnknownScheme = 3;
        public const int ConnectorOpenFailed = 4;
    }
}
