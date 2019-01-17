using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Engine;
using Engine.Components;
using Microsoft.Xna.Framework;
using MonoGame.Extended.Sprites;
using MonoGame.Extended.TextureAtlases;

namespace WorldEdit {
    class Program {
        static readonly string _sequreKye = "Abc";
        static void Main(string[] args) {
            Menu.MenuCollection menuOptions = new Menu.MenuCollection("Wordls", false) {
                TestLevel
            };
            menuOptions.Run();
        }

        #region TestLevel
        [Description("TestLevel")]
        private static void TestLevel() {
            ECS.World world = new ECS.World();

            //TextureRegion2D

            //TextureRegion2D textureRegion2D = new TextureRegion2D(;
            //world.CreateEntity("Player", new ECS.Component[] { new PositionComponent() { Vector2 = Vector2.Zero }, new RendererComponent() { Sprite = new Sprite(textureRegion2D) } });

            Console.WriteLine("Saving...");
            world.SaveToFile("TestLevel.dat", _sequreKye);
            Console.WriteLine("Done");
        }
        #endregion
    }
}
