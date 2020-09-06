using System;

namespace ZEngine.Core.Configs
{
    public abstract class ConfigIdResolver
    {
        private static ConfigIdResolver gInstance;

        public static ConfigIdResolver Instance
        {
            get
            {
                if (gInstance == null)
                {
                    throw new InvalidOperationException("");
                }
                
                return gInstance;
            }
            set => gInstance = value;
        }

        public abstract TConfig Resolve<TConfig>(ConfigId<TConfig> configId) where TConfig : MasterConfigItem;
    }
}