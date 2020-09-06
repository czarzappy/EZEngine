using System.Collections.Generic;
using System.IO;
using System.Numerics;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Noho.Parsing.Models;
using Noho.Parsing.Parsers;
using ZEngine.UnitTesting;

namespace Noho.Parsing.Tests.Surgery
{
    [TestClass]
    public class TestParseScene
    {
        [TestMethod]
        public void TestToStream()
        {
            using (var stringWriter = new StringWriter())
            {
                SceneConvert.ToXmlStream(stringWriter, new SceneDef
                {
                    Name = "hand_left",
                    BgType = BackgroundType.External,
                    Entities = new List<Entity>
                    {
                        new Entity
                        {
                            Tag = "WOUND_GLASS",
                            Position = new Vector2(1.4f, 1.4f),
                            Rotation = 1.4f
                        }
                    }
                });

                string result = stringWriter.ToString();
            
                ZLog.Info(result);

                SceneDef sceneDef = SceneConvert.FromXmlText(result);
                
                Assert.AreEqual(BackgroundType.External, sceneDef.BgType);
            }
        }
        
        [TestMethod]
        public void TestFromXmlStream()
        {
            FileStream fs = new FileStream("test.xml", FileMode.Open, FileAccess.Read);

            var scene = SceneConvert.FromXmlStream(fs);
            
            Assert.AreEqual("hand_left", scene.Name);
            Assert.AreEqual(BackgroundType.External, scene.BgType);

            ZAssert.IsValidItems(scene.Entities, 
                entity =>
                {
                    Assert.AreEqual("WOUND_GLASS", entity.Tag);
                    Assert.AreEqual(new Vector2(1.4f, 1.4f), entity.Position);
                    Assert.AreEqual(1.4f, entity.Rotation);
                }, 
                entity =>
                {
                    Assert.AreEqual("WOUND_GLASS", entity.Tag);
                    Assert.AreEqual(new Vector2(1.4f, 1.4f), entity.Position);
                    Assert.AreEqual(1.4f, entity.Rotation);
                }, 
                entity =>
                {
                    Assert.AreEqual("WOUND_GLASS", entity.Tag);
                    Assert.AreEqual(new Vector2(1.4f, 1.4f), entity.Position);
                    Assert.AreEqual(1.4f, entity.Rotation);
                }, 
                entity =>
                {
                    Assert.AreEqual("HEMORRHAGE", entity.Tag);
                    Assert.AreEqual(new Vector2(1.4f, 1.4f), entity.Position);
                    Assert.AreEqual(1.4f, entity.Rotation);
                }, 
                entity =>
                {
                    Assert.AreEqual("HEMORRHAGE", entity.Tag);
                    Assert.AreEqual(new Vector2(1.4f, 1.4f), entity.Position);
                    Assert.AreEqual(1.4f, entity.Rotation);
                });
        }
    }
}