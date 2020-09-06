using System;

namespace ZEngine.Core.Localization
{
    public abstract class LocIdResolver
    {
        private static LocIdResolver gInstance;

        public static LocIdResolver Instance
        {
            get
            {
                if (gInstance == null)
                {
                    throw new InvalidOperationException("LocIdResolver has not been initialized");
                }
                return gInstance;
            }
            set => gInstance = value;
        }

        public abstract string Resolve(LocId locId);

        public abstract LocId Register(string value);

        public abstract void SetLanguage(Enum langEnum);
        public abstract void Register(Enum langEnum, LocId locId, string value);
    }
}