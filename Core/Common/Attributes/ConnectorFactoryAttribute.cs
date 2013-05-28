using System;

namespace eigenein.SkypeNinja.Core.Common.Attributes
{
    /// <summary>
    /// Describes the connector.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public class ConnectorFactoryAttribute : Attribute
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
