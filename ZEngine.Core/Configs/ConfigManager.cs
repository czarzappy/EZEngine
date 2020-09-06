using ZEngine.Core.Serialization;

namespace ZEngine.Core.Configs
{
    public class ConfigManager<T>
    {
        private readonly IConverter mConverter;

        public ConfigManager(IConverter converter)
        {
            mConverter = converter;
        }

        public void Load()
        {
            
        }
    }
}