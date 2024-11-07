using Newtonsoft.Json;
using System.Xml;
using System.Xml.Serialization;

namespace AppHomolog.Client
{
    public static class Converter
    {
        public static T Deserialize<T>(this string value, string objectMediaType = "", JsonSerializerSettings jsonSettings = null)
        {
            var type = typeof(T);

            if (type == typeof(string))
            {
                return (T)(value as object);
            }

            switch (objectMediaType)
            {
                case "application/xml":
                    TryParseAsXml(value, out T objXml);
                    return objXml;

                case "application/json":
                default:
                    TryParseAsJson(value, out T objJson, jsonSettings);
                    return objJson;
            }
        }

        private static bool TryParseAsJson<T>(string value, out T obj, JsonSerializerSettings jsonSettings = null)
        {
            try
            {
                if (value != null)
                {
                    if (jsonSettings != null)
                    {
                        obj = JsonConvert.DeserializeObject<T>(value, jsonSettings);
                    }
                    else
                    {
                        obj = JsonConvert.DeserializeObject<T>(value);
                    }
                }
                else
                {
                    obj = default(T);
                }
                return true;
            }
            catch (Exception)
            {
                obj = default(T);
            }
            return false;
        }

        private static bool TryParseAsXml<T>(string value, out T obj)
        {
            try
            {
                if (value != null)
                {
                    using (StringReader reader = new StringReader(value))
                    {
                        XmlSerializer serializer = new XmlSerializer(typeof(T));
                        obj = (T)serializer.Deserialize(reader);
                    }
                }
                else
                {
                    obj = default(T);
                }
                return true;
            }
            catch (Exception)
            {
                obj = default(T);
            }
            return false;
        }

        public static string Serialize(this object value, string objectMediaType, JsonSerializerSettings jsonSerializerSettings = null)
        {
            switch (objectMediaType)
            {
                case "application/json":
                    if (TrySerializeJson<object>(value, out string serializedJson, jsonSerializerSettings))
                        return serializedJson;
                    break;
                case "application/xml":
                    if (TrySerializeXML<object>(value, out string serializedXML))
                        return serializedXML;
                    break;
                default:
                    throw new NotImplementedException("Tipo de dado para serealização não implementado");
            }

            return string.Empty;
        }

        private static bool TrySerializeJson<T>(object value, out string serializedObj, JsonSerializerSettings jsonSerializerSettings = null)
        {
            try
            {
                if (jsonSerializerSettings != null)
                {
                    serializedObj = JsonConvert.SerializeObject(value);
                }
                else
                {
                    serializedObj = JsonConvert.SerializeObject(value, jsonSerializerSettings);
                }

                
                return true;
            }
            catch (Exception)
            {
                serializedObj = string.Empty;
            }

            return false;
        }

        private static bool TrySerializeXML<T>(object value, out string serializedObj)
        {
            try
            {
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
                serializedObj = string.Empty;

                using (var stringWriter = new StringWriter())
                {
                    using (XmlWriter writer = XmlWriter.Create(stringWriter))
                    {
                        xmlSerializer.Serialize(writer, value);
                        serializedObj = stringWriter.ToString();
                    }
                }

                return true;
            }
            catch (Exception)
            {
                serializedObj = string.Empty;
            }

            return false;
        }

        public static T Deserialize<T>(this XmlNode xmlNode)
        {
            try
            {
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
                XmlReader xmlReader = new XmlTextReader(xmlNode.OuterXml, XmlNodeType.Document, null);

                var response = (T)xmlSerializer.Deserialize(xmlReader);

                return response;
            }
            catch (Exception)
            {
                return default(T);
            }
        }
    }
}
