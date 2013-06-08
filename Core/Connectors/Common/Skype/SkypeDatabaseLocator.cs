using System;

using eigenein.SkypeNinja.Core.Interfaces;

namespace eigenein.SkypeNinja.Core.Connectors.Common.Skype
{
    /// <summary>
    /// Skype database locator for Windows 7/Vista and Windows 8.
    /// </summary>
    internal class SkypeDatabaseLocator : ISkypeDatabaseLocator
    {
        /// <summary>
        /// Finds the location of the main Skype database file.
        /// </summary>
        public string FindDatabase(string userName, string skypeId)
        {
            throw new NotImplementedException();
        }
    }
}
