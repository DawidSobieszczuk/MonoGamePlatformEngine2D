using MonoGame.Extended.Sprites;
using MonoGame.Extended.Tiled;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Engine.Components {
    /// <summary>
    /// RenderSystem Renderuje pole które nie jest nullem. Jeśli oba spełniają warunek Renderuje Sprite.
    /// </summary>
    class RendererComponent : ECS.Component {
        [JsonIgnore()]
        public Sprite Sprite { get => MainGame.Instance.Assets.Get<Sprite>(SpriteName); set => SpriteName = MainGame.Instance.Assets.GetName(value); }
        [JsonIgnore()]
        public TiledMap TiledMap { get => MainGame.Instance.Assets.Get<TiledMap>(TiledMapName); set => MainGame.Instance.Assets.GetName(value); }

        public string SpriteName;
        public string TiledMapName;
    }
}
