using System;

namespace eigenein.SkypeNinja.Core.Attributes
{
    /// <summary>
    /// Describes the connector.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    internal class ConnectorFactoryAttribute : Attribute
    {
        public ConnectorFactoryAttribute(string help)
        {
            this.Help = help;
        }

        public string Help
        {
            get;
            private set;
        }
    }
}
