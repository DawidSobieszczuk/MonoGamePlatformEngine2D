using Engine.Components;
using Microsoft.Xna.Framework;
using MonoGame.Extended.Sprites;
using MonoGame.Extended.TextureAtlases;
using MonoGame.Extended.Tiled.Graphics;

namespace Engine.Systems {
    class RenderSystem : ECS.System {
        TiledMapRenderer _tiledMapRenderer;

        public RenderSystem() : base(new[] { "PositionComponent", "RendererComponent" }) {
            _tiledMapRenderer = new TiledMapRenderer(MainGame.Instance.GraphicsDevice);
        }

        public override void Draw() {
            if(Get<RendererComponent>().Sprite != null) {
                DrawSprite(Get<RendererComponent>().Sprite);
            } else if(Get<RendererComponent>().TiledMap != null) {
                _tiledMapRenderer.Draw(Get<RendererComponent>().TiledMap, MainGame.Instance.MainCamera.GetViewMatrix());
            }
        }

        private void DrawSprite(Sprite sprite) {
            MainGame.Instance.SpriteBatch.Draw(sprite.TextureRegion, Get<PositionComponent>().Vector2, Color.White);
        }
    }
}
