namespace ZEngine.Core.Configs
{
    public struct ConfigId<T> where T : MasterConfigItem
    {
        public int Id;

        public static implicit operator T(ConfigId<T> configId)
        {
            return configId.Config;
        }

        public T Config => ConfigIdResolver.Instance.Resolve(this);
    }
}