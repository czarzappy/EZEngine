using System.IO;
using System.Xml.Serialization;
using Noho.Parsing.Models;

namespace Noho.Parsing.Parsers
{
    public static class SceneConvert
    {
        private static readonly XmlSerializer XML_SERIALIZER = new XmlSerializer(typeof(SceneDef));
        public static SceneDef FromXmlStream(Stream stream)
        {
            var scene = (SceneDef) XML_SERIALIZER.Deserialize(stream);

            return scene;
        }
        public static SceneDef FromXmlText(string text)
        {
            TextReader textReader = new StringReader(text);

            return FromTextReader(textReader);
        }

        public static SceneDef FromTextReader(TextReader textReader)
        {
            var scene = (SceneDef) XML_SERIALIZER.Deserialize(textReader);

            return scene;
        }

        public static void ToXmlStream(TextWriter writer, SceneDef sceneDef)
        {
            XML_SERIALIZER.Serialize(writer, sceneDef);
        }
    }
}