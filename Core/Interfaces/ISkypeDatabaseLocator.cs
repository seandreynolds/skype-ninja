using System;

namespace eigenein.SkypeNinja.Core.Interfaces
{
    /// <summary>
    /// Used to find a location of the Skype database.
    /// </summary>
    internal interface ISkypeDatabaseLocator
    {
        /// <summary>
        /// Finds the location of the main Skype database file.
        /// </summary>
        string FindDatabase(string userName, string skypeId);
    }
}
