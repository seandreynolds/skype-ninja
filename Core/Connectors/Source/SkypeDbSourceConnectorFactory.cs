﻿using System;

using eigenein.SkypeNinja.Core.Common.Attributes;
using eigenein.SkypeNinja.Core.Interfaces;

namespace eigenein.SkypeNinja.Core.Connectors.Source
{
    [ConnectorFactory("Core.Connectors.Source.SkypeDbSourceConnectorFactory.Help")]
    internal class SkypeDbSourceConnectorFactory : ISourceConnectorFactory
    {
        #region Implementation of ISourceConnectorFactory

        public ISourceConnector CreateConnector(Uri uri)
        {
            if (String.IsNullOrWhiteSpace(uri.LocalPath))
            {
                throw new ArgumentException("Skype database path is expected.");
            }
            return new SkypeSourceConnector(uri);
        }

        #endregion
    }
}
