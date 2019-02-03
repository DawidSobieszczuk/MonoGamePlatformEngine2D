using Microsoft.Xna.Framework.Content;
using MonoGame.Extended.Collections;
using System;
using System.Collections.Generic;
using System.Text;

namespace Engine {
    /// <summary>
    /// Przechowuje wszyskie asssety. Wczytaj asssety przed inicjalizacją świata
    /// </summary>
    public class Assets {
        ContentManager _content;
        Dictionary<string, object> _assets;

        public Assets(ContentManager content) {
            _content = content;
            _assets = new Dictionary<string, object>();
        }


        public T Add<T>(string name, T item) {
            _assets.Add(name, item);
            return item;
        }

        /// <summary>
        /// Load from Content File
        /// </summary>
        public T Load<T>(string assetName) {
            T item = _content.Load<T>(assetName);
            _assets.Add(assetName, item);
            return item;
        }

        public object Get(string assetName) => _assets[assetName];
        public string GetName(object asset) => _assets.GetKey(asset);

        public T Get<T>(string assetName) => (T)_assets[assetName];

        public object this[string assetName] {
            get => Get(assetName);
        }
    }
}
