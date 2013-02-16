using System;
using eigenein.SkypeNinja.Core.Extensions;
using eigenein.SkypeNinja.Core.Helpers;
using eigenein.SkypeNinja.Core.Interfaces;

namespace eigenein.SkypeNinja.Core.Connectors.Source
{
    internal class SkypeSourceConnectorFactory : ISourceConnectorFactory
    {
        #region Implementation of ISourceConnectorFactory

        public ISourceConnector CreateConnector(Uri uri)
        {
            QueryParameters parameters = uri.GetQueryParameters();

            string isPathString;
            bool isPath;
            if (!parameters.TryGetValue("isPath", out isPathString) ||
                !Boolean.TryParse(isPathString, out isPath) ||
                !isPath)
            {
                string skypeId = uri.LocalPath;
                string userName;
                if (!parameters.TryGetValue("userName", out userName))
                {
                    userName = Environment.UserName;
                }

                throw new NotImplementedException(
                    "TODO: Search for the database and open SkypeSourceConnector on it.");
            }

            return new SkypeSourceConnector(uri);
        }

        #endregion
    }
}
