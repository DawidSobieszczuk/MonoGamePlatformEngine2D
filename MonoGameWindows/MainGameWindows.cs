using Engine.Input;
using Engine.Input.Touch;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input.Touch;
using MonoGame.Extended;
using MonoGame.Extended.Sprites;
using MonoGame.Extended.TextureAtlases;
using MonoGame.Extended.ViewportAdapters;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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

            //CurrentWorld = ECS.World.LoadFromFile("testW");
            //CurrentWorld.Init();
            //CurrentWorld.SaveToFile("testW");

            
        }

        protected override void Update(GameTime gameTime) {
            TouchCollection touchLocations = TouchManager.Touches;

            if(touchLocations.Count > 0) {
                Debug.WriteLine("ok");
            }

            InputManager.Update();
            Console.WriteLine(InputManager.MouseState.ScrollWheelValue);
            Console.WriteLine(InputManager.MouseState.HorizontalScrollWheelValue);

            base.Update(gameTime);
        }
    }
}
