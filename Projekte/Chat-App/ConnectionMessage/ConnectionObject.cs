using System.Xml.Serialization;
using System.Xml;


namespace ConnectionObject
{
    [Serializable]
    public class ConnectionObject
    {
        /// <summary>
        /// serializes an object into an (xml) string
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public string Serialize<T>(T obj)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
            using (var stringWriter = new StringWriter())
            {
                using (XmlTextWriter xmlTextWriter = new XmlTextWriter(stringWriter) { Formatting = Formatting.Indented })
                {
                    xmlSerializer.Serialize(xmlTextWriter, obj);
                    return stringWriter.ToString();
                }
            }
        }
        /// <summary>
        /// deserialize an Object from an (xml) string
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="xmlString"></param>
        /// <returns></returns>
        public T Deserialize<T>(string xmlString) where T : class
        {
            using (StringReader stringReader = new StringReader(xmlString))
            using (XmlTextReader xmlTextReader = new XmlTextReader(stringReader) { Normalization = false })
            {
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
                return (T)xmlSerializer.Deserialize(xmlTextReader);
            }
        }
        /// <summary>
        /// returns the XML Root aka the serialized classname
        /// </summary>
        /// <param name="xmlString">The complete xml as string</param>
        /// <returns></returns>
        public string GetTypeFromXML(string xmlString)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(xmlString);
            XmlElement root = xmlDoc.DocumentElement;
            return root.Name;
        }

    }
   /// <summary>
   /// Represents a chat message
   /// </summary>
    [Serializable]
    public class ChatMessage 
    {
        public string message { get; set; }
        public string sender { get; set; }
        public string receiver { get; set; }
        public string senderUUID { get; set; }

    }
    /// <summary>
    /// represents a question / request / Query
    /// </summary>
    [Serializable]
    public class Query
    {
        public string question { get; set; }
        public string sender { get; set; }
    }
    /// <summary>
    /// represents a string / reply / message
    /// </summary>
    [Serializable]
    public class Response
    {
        public string stringToTransfer{ get; set; }
        public string purpose { get; set; }

        public Response(string String,string Purpose) 
        {
            stringToTransfer = String;
            purpose = Purpose;
        }
        public Response() { }
    }
    /// <summary>
    /// represents a Userlist
    /// </summary>
    [Serializable]
    public class Userlist
    {
        public List<string> userlist { get; set; }      
    }


}
