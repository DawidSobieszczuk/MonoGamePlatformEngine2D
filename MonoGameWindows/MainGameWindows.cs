using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using MonoGame.Extended.Sprites;
using MonoGame.Extended.TextureAtlases;
using MonoGame.Extended.ViewportAdapters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoGameWindows {
    class MainGameWindows : Engine.MainGame {
        protected override void LoadContent() {
            base.LoadContent();

            TextureAtlas atlas = new TextureAtlas("shapes", Assets.Load<Texture2D>("shapes"));
            atlas.CreateRegion("box", 10, 10, 100, 100);

            Sprite sprite = Assets.Add("PlayerSprite", new Sprite(atlas["box"]));

            //CurrentWorld = new World();
            //CurrentWorld.AddSystems(new RenderSystem());

            //CurrentWorld.CreateEntity("Player", new Component[] { new PositionComponent() { }, new RendererComponent() { Sprite = sprite}});

            CurrentWorld = ECS.World.LoadFromFile("testW");
            CurrentWorld.Init();
            //CurrentWorld.SaveToFile("testW");
        }
    }
}
