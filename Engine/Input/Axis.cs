using System;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Engine.Input {
    internal class Axis {
        public string Name;
        public float Value { get; private set; }

        internal InputTypes _type;
        internal int _playerIndex;
        internal GamePadAxes _gamePadAxis;
        internal MouseAxes _mouseAxis;
        internal Keys _leftKey;
        internal Keys _rightKey;

        internal Axis(string name, PlayerIndex playerIndex, GamePadAxes gamePadAxes) : this(name, (int)playerIndex, gamePadAxes) { }
        internal Axis(string name, int playerIndex, GamePadAxes gamePadAxes) {
            Name = name;
            _type = InputTypes.GamePad;
            _playerIndex = playerIndex;
            _gamePadAxis = gamePadAxes;
        }

        internal Axis(string name, MouseAxes mouseAxis) {
            Name = name;
            _type = InputTypes.Mouse;
            _mouseAxis = mouseAxis;
        }

        internal Axis(string name, Keys left, Keys right) {
            Name = name;
            _type = InputTypes.Keyboard;
            _leftKey = left;
            _rightKey = right;
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
            switch(_mouseAxis) {
                case MouseAxes.Horizontal:
                    Value = MathHelper.Clamp(InputManager.MouseDelta.X * InputManager.MOUSE_MODIFIER, -1, 1);
                    break;
                case MouseAxes.Vertical:
                    Value = MathHelper.Clamp(InputManager.MouseDelta.Y * InputManager.MOUSE_MODIFIER, -1, 1);
                    break;
                case MouseAxes.ScrollWheel:
                    Value = MathHelper.Clamp(InputManager.MouseScrollDelta * InputManager.MOUSE_MODIFIER, -1, 1);
                    break;
                case MouseAxes.HorizontalScrollWheel:
                    Value = MathHelper.Clamp(InputManager.MouseHorizontalScrollDelta * InputManager.MOUSE_MODIFIER, -1, 1);
                    break;
            }
        }

        private void KeyboardUpdate() {
            KeyboardState state = InputManager.KeyboardState;

            Value = 0;

            if(state.IsKeyDown(_leftKey)) Value += -1;
            if(state.IsKeyDown(_rightKey)) Value += 1;
        }

        private void GamePadUpdate() {
            if(InputManager.GamePadStates.Length < _playerIndex) {
                Debug.WriteLine("GamePad<" + _playerIndex + "> not connected");
                return;
            }
            GamePadState state = InputManager.GamePadStates[_playerIndex];

            switch(_gamePadAxis) {
                case GamePadAxes.LeftThumbStickHorizontal:
                    Value = state.ThumbSticks.Left.X;
                    break;
                case GamePadAxes.LeftThumbStickVertical:
                    Value = state.ThumbSticks.Left.Y;
                    break;
                case GamePadAxes.LeftTrigger:
                    Value = state.Triggers.Left;
                    break;
                case GamePadAxes.RightThumbStickHorizontal:
                    Value = state.ThumbSticks.Right.X;
                    break;
                case GamePadAxes.RightThumbStickVertical:
                    Value = state.ThumbSticks.Right.Y;
                    break;
                case GamePadAxes.RightTrigger:
                    Value = state.Triggers.Right;
                    break;
            }
        }
    }
}
