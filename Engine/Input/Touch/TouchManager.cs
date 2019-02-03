using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using System;
using System.Collections.Generic;
using System.Text;

namespace Engine.Input.Touch {
    public static class TouchManager {

        public static TouchCollection Touches { get; private set; }

        static TouchManager() {
            
        }

        public static void Update() {
            Touches = TouchPanel.GetState();
#if DEBUG
            MouseState state = InputManager.MouseState;
            if(state.LeftButton == ButtonState.Pressed)
                Touches = new TouchCollection(new TouchLocation[] { new TouchLocation(0, TouchLocationState.Pressed, state.Position.ToVector2()) } );
#endif
        }
    }
}
