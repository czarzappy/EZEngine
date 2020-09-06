using System;
using System.Collections.Generic;

namespace Noho.Parsing.Models
{
    public class EpisodeDef
    {
        public string Name;
        public string Desc;

        public string Thumbnail;

        public readonly List<object> SceneItems = new List<object>();
    }
}