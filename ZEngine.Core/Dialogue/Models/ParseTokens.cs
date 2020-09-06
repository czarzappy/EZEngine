namespace ZEngine.Core.Dialogue.Models
{
    public static class ParseTokens
    {
        public const char CUE_CHAR = '*';
        public const char CUE_SEPARATOR = ',';
        public const char CUE_OPTIONS = ':';

        public const string CUE_END_TOKEN = "END_";
            
        
        public static readonly string[] COMMENT_PREFIXES = {
            "--",
            "//",
            "__",
            "___"
        };

        public static readonly string BLOCK_COMMENT_START = "/*";
        public static readonly string BLOCK_COMMENT_END = "*/";
    }
}