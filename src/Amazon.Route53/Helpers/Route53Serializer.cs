﻿using System.IO;
using System.Text;
using System.Xml.Serialization;

namespace Amazon.Route53
{
    public static class Route53Serializer<T>
    {
        private static readonly XmlSerializer serializer = new XmlSerializer(typeof(T), Route53Client.Namespace);

        public static byte[] SerializeToUtf8Bytes(T instance)
        {
            using (var stream = new MemoryStream())
            using (var writer = new StreamWriter(stream, Encoding.UTF8))
            {
                serializer.Serialize(writer, instance, XmlSerializerNamespacesCache.Get());

                return stream.ToArray();
            }
        }

        public static T DeserializeXml(string xml)
        {
            using (var reader = new StringReader(xml))
            {
                return (T)serializer.Deserialize(reader);
            }
        }
    }
}