using System;
using System.Collections.Generic;
using System.Numerics;
using System.Xml.Serialization;

namespace Noho.Parsing.Models
{
    [XmlRoot(ElementName = "Scene")]
    public class SceneDef
    {
        public string Name;

        public Vector2 BgOffset;
        public Vector2 OverrideBgSize;
        
        // [XmlText()]
        public BackgroundType BgType;

        public List<Entity> Entities;
    }

    [Serializable]
    public enum BackgroundType
    {   
        None,
        Internal,
        External
    }

    public class Entity
    {
        public string Tag;

        public Vector2 Position;
        public float Rotation;
    }
}