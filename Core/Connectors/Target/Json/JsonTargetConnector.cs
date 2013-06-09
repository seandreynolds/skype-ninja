using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

using Newtonsoft.Json;

using eigenein.SkypeNinja.Core.Common.Attributes;
using eigenein.SkypeNinja.Core.Common.Caches;
using eigenein.SkypeNinja.Core.Connectors.Common;
using eigenein.SkypeNinja.Core.Enums;
using eigenein.SkypeNinja.Core.Interfaces;

namespace eigenein.SkypeNinja.Core.Connectors.Target.Json
{
    /// <summary>
    /// Represents the JSON target connector.
    /// </summary>
    internal class JsonTargetConnector : TargetConnector
    {
        private delegate void SerializerMethod(JsonTextWriter writer, IList<object> objects);

        private static readonly IDictionary<PropertyType, string> PropertyNames =
            new Dictionary<PropertyType, string>()
            {
                {PropertyType.Id, "id"},
                {PropertyType.Class, "class"},
                {PropertyType.Body, "body"},
                {PropertyType.Group, "group"},
                {PropertyType.Timestamp, "timestamp"},
                {PropertyType.Author, "author"},
                {PropertyType.FromDisplayName, "from_display_name"},
                {PropertyType.SkypeMessageStatus, "skype_message_status"},
                {PropertyType.SkypeMessageType, "skype_message_type"},
            };

        private static readonly IDictionary<PropertyType, SerializerMethod> Serializers =
            new Dictionary<PropertyType, SerializerMethod>()
            {
                {PropertyType.Group, SerializeGroup},
            };

        private readonly JsonTextWriter jsonTextWriter;

        private static void SerializeGroup(JsonTextWriter writer, IList<object> objects)
        {
            MessageGroup group = (MessageGroup)objects.First();
            // Serialize as an array.
            writer.WriteStartArray();
            try
            {
                foreach (string part in group)
                {
                    writer.WriteValue(part);
                }
            }
            finally
            {
                writer.WriteEnd();
            }
        }

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
            // Write the properties.
            foreach (KeyValuePair<PropertyType, IList<object>> property in message.Properties)
            {
                string propertyName;
                if (!PropertyNames.TryGetValue(property.Key, out propertyName))
                {
                    // Do not write the property.
                    continue;
                }
                // Write the property name.
                jsonTextWriter.WritePropertyName(propertyName);
                // Check for the custom serializer method.
                SerializerMethod serialize;
                if (Serializers.TryGetValue(property.Key, out serialize))
                {
                    serialize(jsonTextWriter, property.Value);
                }
                else
                {
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
                        try
                        {
                            foreach (object value in property.Value)
                            {
                                jsonTextWriter.WriteValue(value);
                            }
                        }
                        finally
                        {
                            jsonTextWriter.WriteEnd();
                        }
                    }
                }
            }
            // End the message.
            jsonTextWriter.WriteEnd();
        }
    }
}
