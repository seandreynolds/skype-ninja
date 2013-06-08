using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

using Newtonsoft.Json;

using eigenein.SkypeNinja.Core.Common.Attributes;
using eigenein.SkypeNinja.Core.Common.Caches;
using eigenein.SkypeNinja.Core.Enums;
using eigenein.SkypeNinja.Core.Interfaces;

namespace eigenein.SkypeNinja.Core.Connectors.Target.Json
{
    /// <summary>
    /// Represents the JSON target connector.
    /// </summary>
    internal class JsonTargetConnector : TargetConnector
    {
        private static readonly IDictionary<PropertyType, string> PropertyNames =
            new Dictionary<PropertyType, string>()
            {
                {PropertyType.Body, "body"},
                {PropertyType.Path, "path"},
                {PropertyType.Timestamp, "timestamp"},
                {PropertyType.Author, "author"},
                {PropertyType.FromDisplayName, "from_display_name"},
                {PropertyType.SkypeMessageStatus, "skype_message_status"}
            };

        private readonly JsonTextWriter jsonTextWriter;

        public JsonTargetConnector(TextWriter textWriter)
        {
            // Initialize the writer.
            jsonTextWriter = new JsonTextWriter(textWriter)
                             {
                                 // TODO: make these parameters configurable.
                                 Formatting = Formatting.Indented,
                                 Indentation = 2,
                                 IndentChar = ' '
                             };
            // Write the comments.
            // TODO: include actual assembly name and version.
            // Start the message array.
            jsonTextWriter.WriteStartArray();
        }

        public override void Close()
        {
            // End the message array.
            jsonTextWriter.WriteEnd();
            // Explicitly write the new line separator.
            jsonTextWriter.WriteRaw(Environment.NewLine);
            // Close the writer.
            jsonTextWriter.Close();
        }

        /// <summary>
        /// Inserts the message into the target.
        /// </summary>
        public override void InsertMessage(IMessage message)
        {
            // Start the message.
            jsonTextWriter.WriteStartObject();
            // Write the message type.
            jsonTextWriter.WritePropertyName("message_type");
            jsonTextWriter.WriteValue(message.MessageType.ToString());
            // Write the properties.
            foreach (KeyValuePair<PropertyType, IList<object>> property in message.Properties)
            {
                // Write the property name.
                jsonTextWriter.WritePropertyName(PropertyNames[property.Key]);
                // Check whether multiple values are allowed.
                FieldValueTypeAttribute attribute;
                if (FieldAttributeCache<PropertyType, FieldValueTypeAttribute>.TryGetAttribute(
                    property.Key.ToString(),
                    out attribute) &&
                    !attribute.AllowMultiple)
                {
                    // Write the simple value.
                    jsonTextWriter.WriteValue(property.Value.First());
                }
                else
                {
                    // Write the property value array.
                    jsonTextWriter.WriteStartArray();
                    foreach (object value in property.Value)
                    {
                        jsonTextWriter.WriteValue(value.ToString());
                    }
                    jsonTextWriter.WriteEnd();
                }
            }
            // End the message.
            jsonTextWriter.WriteEnd();
        }
    }
}
