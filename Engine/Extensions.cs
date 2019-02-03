using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Input.InputListeners;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Engine {
    static class Extensions {
        public static T GetKey<T, K>(this Dictionary<T, K> dictionary, K value) {
            return dictionary.FirstOrDefault(x => x.Value.Equals(value)).Key;
        }

        public static bool IsButtonDown(this MouseState mouseState, MouseButton mouseButton) {
            switch(mouseButton) {
                case MouseButton.Left:
                    return mouseState.LeftButton == ButtonState.Pressed;
                case MouseButton.Middle:
                    return mouseState.MiddleButton == ButtonState.Pressed;
                case MouseButton.Right:
                    return mouseState.RightButton == ButtonState.Pressed;
                case MouseButton.XButton1:
                    return mouseState.XButton1 == ButtonState.Pressed;
                case MouseButton.XButton2:
                    return mouseState.XButton2 == ButtonState.Pressed;
            }

            return false;
        }
    }
}
