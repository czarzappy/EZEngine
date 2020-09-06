using System;

namespace ZEngine.Core.Configs
{
    [Serializable]
    public class MasterConfigItem
    {
        public int Id;
        public string DevName;

        public virtual bool CompareDevName(string devName)
        {
            return DevName == devName;
        }
    }
}