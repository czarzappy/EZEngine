using System;
using System.IO;
using ZEngine.Core.Localization;

namespace Noho.Configs
{
    public class NohoLocIdResolver
    {
        private static NohoLocIdResolver mInstance;
        public static NohoLocIdResolver Instance
        {
            get
            {
                if (mInstance == null)
                {
                    mInstance = new NohoLocIdResolver();
                }

                return mInstance;
            }
        }

        public LocId Register(string text)
        {
            var locId = LocIdResolver.Instance.Register(text);

            string astericks = text.ToAsterisks();

            LocIdResolver.Instance.Register(NohoLang.ASTERISKS, locId, astericks);
            
            return locId;
        }
    }
}