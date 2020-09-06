using System.IO;

namespace ZEngine.Core.IO
{
    public static class StreamExt
    {
        public static void WriteToFile(this MemoryStream stream, string path)
        {
            using FileStream file = new FileStream(path, FileMode.Create, FileAccess.Write);
            
            stream.WriteTo(file);
        }
    }
}