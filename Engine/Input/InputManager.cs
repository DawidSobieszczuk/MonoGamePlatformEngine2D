using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using MonoGame.Extended.Collections;
using MonoGame.Extended.Input.InputListeners;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace Engine.Input {
    /// <summary>
    /// Keyboard, Mouse and Gamepad. Touch ma swój manager: TouchManager.
    /// </summary>
    public static class InputManager {
        public const float MOUSE_MODIFIER = 0.02f;

        static Bag<Axis> _axes;
        static Bag<Button> _buttons;

        static int _gamePadCounts = 0;
        public static GamePadState[] GamePadStates { get; set; }
        public static KeyboardState KeyboardState { get; set; }
        public static MouseState MouseState { get; set; }
        public static MouseState PrevMouseState { get; set; }


        public static Vector2 MouseDelta { get; set; }
        public static float MouseScrollDelta { get; set; }
        public static float MouseHorizontalScrollDelta { get; set; }
        public static bool IsMouseVisible { get => MainGame.Instance.IsMouseVisible; set => MainGame.Instance.IsMouseVisible = value; }


        static InputManager() {
            _axes = new Bag<Axis>();
            _buttons = new Bag<Button>();
            
            for(int i = 0; i < GamePad.MaximumGamePadCount; i++) {
                if(GamePad.GetCapabilities(i).IsConnected) {
                    _gamePadCounts++;
                }
            }
            GamePadStates = new GamePadState[_gamePadCounts];
        }

        public static void StateUpdate() {
            for(int i = 0; i < _gamePadCounts; i++) {
                GamePadStates[i] = GamePad.GetState(i);
            }
            KeyboardState = Keyboard.GetState();
            PrevMouseState = MouseState;
            MouseState = Mouse.GetState();
        }

        public static void ActionUpdate() {
            MouseDelta = new Vector2(MouseState.X - PrevMouseState.X, MouseState.Y - PrevMouseState.Y);
            MouseScrollDelta = MouseState.ScrollWheelValue - PrevMouseState.ScrollWheelValue;
            MouseHorizontalScrollDelta = MouseState.HorizontalScrollWheelValue - PrevMouseState.HorizontalScrollWheelValue;

            foreach(Axis axis in _axes) {
                axis.Update();
            }
            foreach(Button button in _buttons) {
                button.Update();
            }
        }

        public static void Update() {
            StateUpdate();
            ActionUpdate();
        }

        public static void CreateAction(string name, PlayerIndex playerIndex, Buttons button) => _buttons.Add(new Button(name, playerIndex, button));
        public static void CreateAction(string name, int playerIndex, Buttons button) => _buttons.Add(new Button(name, playerIndex, button));
        public static void CreateAction(string name, MouseButton button) => _buttons.Add(new Button(name, button));
        public static void CreateAction(string name, Keys button) => _buttons.Add(new Button(name, button));
        public static void CreateAction(string name, PlayerIndex playerIndex, GamePadAxes axis) => _axes.Add(new Axis(name, playerIndex, axis));
        public static void CreateAction(string name, int playerIndex, GamePadAxes axis) => _axes.Add(new Axis(name, playerIndex, axis));
        public static void CreateAction(string name, MouseAxes axis) => _axes.Add(new Axis(name, axis));
        public static void CreateAction(string name, Keys left, Keys right) => _axes.Add(new Axis(name, left, right));

        /// <summary>
        /// Jest Clapowane od -1 do 1; Użyj Mouse Delta jeśli potrzebujesz pełnego odzwierdziedlenie.
        /// </summary>
        public static float GetAxis(string actionName) {
            float value = 0;
            foreach(Axis axis in _axes) {
                if(axis.Name == actionName) {
                    if(value == 0)
                        value = axis.Value;
                }
                
            }
            return MathHelper.Clamp(value, -1, 1);
        }

        public static bool GetDown(string actionName) {
            bool value = false;
            foreach(Button button in _buttons) {
                if(button.Name == actionName) {
                    if(button.Value)
                        value = true;
                }
            }
            return value;
        }

        public static bool GetUp(string actionName) {
            return !GetDown(actionName);
        }
    }
}
