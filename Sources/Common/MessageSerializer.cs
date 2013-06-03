using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Xml;
using System.Xml.Serialization;

namespace Common
{
    // XmlSerializer wrapper.
    // Provides simple way to serialize & deserialize service broker messages.
    public static class MessageSerializer
    {
        // serializers settings
        private static readonly XmlWriterSettings writerSettings;
        private static readonly XmlSerializerNamespaces writerNamespaces;

        // serializers cache
        private static readonly ConditionalWeakTable<Type, XmlSerializer> serializers;


        static MessageSerializer()
        {
            writerSettings = new XmlWriterSettings
            {
                // omit new lines
                NewLineHandling = NewLineHandling.None, 

                //omit xml header
                OmitXmlDeclaration = true
            };

            // omit namespaces
            writerNamespaces = new XmlSerializerNamespaces(new[] { XmlQualifiedName.Empty });
            serializers = new ConditionalWeakTable<Type, XmlSerializer>();
        }


        // Creates XmlSerializer with given root element name settings.
        private static XmlSerializer CreateSerializer(Type type, string rootName = null)
        {
            return string.IsNullOrWhiteSpace(rootName) ? 
                new XmlSerializer(type) : 
                new XmlSerializer(type, new XmlRootAttribute(rootName));
        }

        // Serializes source object with given root element name into xml string representation.
        public static string Serialize<T>(T source, string rootName = null)
        {
            var type = typeof(T);
            var serializer = serializers.GetValue(type, t => CreateSerializer(t, rootName));

            using (var stringWriter = new StringWriter())
            {
                using (var xmlWriter = XmlWriter.Create(stringWriter, writerSettings))
                {
                    serializer.Serialize(xmlWriter, source, writerNamespaces);
                }
                return stringWriter.ToString();
            }
        }

        // Deserializes source xml string with given root element name into object.
        public static T Deserialize<T>(string source, string rootName = null)
        {
            var type = typeof(T);
            var serializer = serializers.GetValue(type, t => CreateSerializer(t, rootName));

            using (var stringReader = new StringReader(source))
            {
                return (T)serializer.Deserialize(stringReader);
            }
        }
    }
}
