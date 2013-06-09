using System;
using System.IO;

using eigenein.SkypeNinja.Core.Interfaces;

namespace eigenein.SkypeNinja.Core.Connectors.Common.Skype
{
    /// <summary>
    /// Default Skype database locator implementation.
    /// https://support.skype.com/en/faq/FA413/how-do-i-back-up-my-configuration-and-instant-message-history
    /// </summary>
    internal class SkypeDatabaseLocator : ISkypeDatabaseLocator
    {
        /// <summary>
        /// Finds the location of the main Skype database file.
        /// </summary>
        public bool FindDatabase(string skypeId, out string databasePath)
        {
            string applicationDataPath = Environment.GetFolderPath(
                Environment.SpecialFolder.ApplicationData);
            databasePath = Path.Combine(applicationDataPath, "Skype", skypeId, "main.db");
            return File.Exists(databasePath);
        }
    }
}
