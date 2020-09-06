using System;
using System.Collections.Generic;
using ZEngine.Core.Collections;

namespace ZEngine.Core.Localization
{
    public class DictionaryLocIdResolver : LocIdResolver
    {
        private readonly Dictionary<Enum, LangLocMapping> mMappings = new Dictionary<Enum, LangLocMapping>();

        private LangLocMapping CurrentMapping => mMappings[mCurrentLang];

        private int mId = 0;

        private Enum mCurrentLang;

        /// <summary>
        /// Use default language
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public override LocId Register(string value)
        {
            var locId = new LocId
            {
                Id = mId
            };

            CurrentMapping[locId] = value;

            mId++;

            return locId;
        }

        public override void Register(Enum langEnum, LocId locId, string value)
        {
            var result = mMappings.GetOrAdd(langEnum, Factory);

            result[locId] = value;
        }

        private static LangLocMapping Factory()
        {
            return new LangLocMapping();
        }
        
        public override void SetLanguage(Enum langEnum)
        {
            mMappings.GetOrAdd(langEnum, Factory);
            
            mCurrentLang = langEnum;
        }

        public override string Resolve(LocId locId)
        {
            return CurrentMapping[locId];
        }
    }
}