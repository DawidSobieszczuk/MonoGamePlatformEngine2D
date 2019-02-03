using System;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Input.InputListeners;

namespace Engine.Input {
    internal class Button {
        internal InputTypes _type;
        public string Name;
        public bool Value { get; private set; }

        internal int _playerIndex;
        internal Buttons _gamePad;
        internal MouseButton _mouse;
        internal Keys _keyboard;

        internal Button(string name, PlayerIndex playerIndex, Buttons gamePadButton) : this(name, (int)playerIndex, gamePadButton) { }
        internal Button(string name, int playerIndex, Buttons gamePadButton) {
            Name = name;
            _type = InputTypes.GamePad;
            _playerIndex = playerIndex;
            _gamePad = gamePadButton;
        }

        internal Button(string name, MouseButton mouseButton) {
            Name = name;
            _type = InputTypes.Mouse;
            _mouse = mouseButton;
        }

        internal Button(string name, Keys keyboardKey) {
            Name = name;
            _type = InputTypes.Keyboard;
            _keyboard = keyboardKey;
        }        

        public void Update() {
            switch(_type) {
                case InputTypes.GamePad:
                    GamePadUpdate();
                    break;
                case InputTypes.Keyboard:
                    KeyboardUpdate();
                    break;
                case InputTypes.Mouse:
                    MouseUpdate();
                    break;
            }
        }

        private void MouseUpdate() {
            Value = InputManager.MouseState.IsButtonDown(_mouse);            
        }

        private void KeyboardUpdate() {
            Value = InputManager.KeyboardState.IsKeyDown(_keyboard);
        }

        private void GamePadUpdate() {
            if(InputManager.GamePadStates.Length < _playerIndex) {
                Debug.WriteLine("GamePad<" + _playerIndex + "> not connected");
                return;
            }

            Value = InputManager.GamePadStates[_playerIndex].IsButtonDown(_gamePad);
        }
    }
}
