using ECS;
using Engine.Components;
using Engine.Input;
using Engine.Input.Touch;
using Engine.Systems;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using MonoGame.Extended.Sprites;
using MonoGame.Extended.TextureAtlases;
using MonoGame.Extended.ViewportAdapters;
using Newtonsoft.Json;
using System.IO;

namespace Engine {
    public class MainGame : Game {
        public static MainGame Instance { get; private set; }

        public World CurrentWorld { get; set; } = new World();

        public Camera2D MainCamera { get; private set; }
        public SpriteBatch SpriteBatch { get; private set; }
        public Assets Assets { get; private set; }
        protected readonly GraphicsDeviceManager _graphics;
        protected readonly int _width;
        protected readonly int _height;

        public MainGame (int width = 480, int height = 800) {
            Instance = this;

            _graphics = new GraphicsDeviceManager(this) {
                PreferredBackBufferWidth = width,
                PreferredBackBufferHeight = height
            };
            _width = width;
            _height = height;
            Content.RootDirectory = "Content";
            Assets = new Assets(Content);
        }

        protected override void Initialize() {
            InputManager.Update();
            base.Initialize();
        }

        protected override void LoadContent() {
            BoxingViewportAdapter viewportAdapter = new BoxingViewportAdapter(Window, GraphicsDevice, _width, _height);
            MainCamera = new Camera2D(viewportAdapter);
            SpriteBatch = new SpriteBatch(_graphics.GraphicsDevice);

            base.LoadContent();
        }

        protected override void Update(GameTime gameTime) {
            CurrentWorld.Update(0); // TODO: delta
            InputManager.Update();
            TouchManager.Update();
            base.Update(gameTime);
            
        }
        protected override void Draw(GameTime gameTime) {
            GraphicsDevice.Clear(Color.Black);

            var transformMatrix = MainCamera.GetViewMatrix();
            SpriteBatch.Begin(transformMatrix: transformMatrix);
            CurrentWorld.Draw();
            SpriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
