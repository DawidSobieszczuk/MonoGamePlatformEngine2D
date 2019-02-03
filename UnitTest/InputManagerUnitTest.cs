using Engine.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Input.InputListeners;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace EngineTest {
    public class InputManagerUnitTest {
        static int _counter = 0;

        #region GamePadAxesTheory
        [Theory]
        [InlineData(.11f, .12f, .21f, .22f, .3f, .4f, GamePadAxes.LeftTrigger, .3f)]
        [InlineData(.11f, .12f, .21f, .22f, .3f, .4f, GamePadAxes.RightTrigger, .4f)]
        [InlineData(.11f, .12f, .21f, .22f, .3f, .4f, GamePadAxes.LeftThumbStickHorizontal, .11f)]
        [InlineData(.11f, .12f, .21f, .22f, .3f, .4f, GamePadAxes.RightThumbStickVertical, .22f)]

        [InlineData(1, 0, 0, 0, 0, 0, GamePadAxes.LeftThumbStickHorizontal, 1)]
        [InlineData(-1, 0, 0, 0, 0, 0, GamePadAxes.LeftThumbStickHorizontal, -1)]

        [InlineData(0, 1, 0, 0, 0, 0, GamePadAxes.LeftThumbStickVertical, 1)]
        [InlineData(0, -1, 0, 0, 0, 0, GamePadAxes.LeftThumbStickVertical, -1)]

        [InlineData(0, 0, 1, 0, 0, 0, GamePadAxes.RightThumbStickHorizontal, 1)]
        [InlineData(0, 0, -1, 0, 0, 0, GamePadAxes.RightThumbStickHorizontal, -1)]

        [InlineData(0, 0, 0, 1, 0, 0, GamePadAxes.RightThumbStickVertical, 1)]
        [InlineData(0, 0, 0, -1, 0, 0, GamePadAxes.RightThumbStickVertical, -1)]

        [InlineData(0, 0, 0, 0, 1, 0, GamePadAxes.LeftTrigger, 1)]
        [InlineData(0, 0, 0, 0, 0, 1, GamePadAxes.RightTrigger, 1)]
        public void GamePadAxesTheory(float leftThumbSticksX, float leftThumbSticksY, float rightThumbSticksX, float rightThumbSticksY, float leftTrigger, float rightTrigger, GamePadAxes gamePadAxes, float expected) {
            GamePadState state = new GamePadState(
                new Vector2(leftThumbSticksX, leftThumbSticksY), new Vector2(rightThumbSticksX, rightThumbSticksY),
                leftTrigger, rightTrigger,
                new Buttons[0]);

            string actionName = "Action" + _counter++;
            InputManager.CreateAction(actionName, PlayerIndex.One, gamePadAxes);
            
            InputManager.GamePadStates = new GamePadState[] { state };
            InputManager.ActionUpdate();


            Assert.Equal(expected, InputManager.GetAxis(actionName));
        }
        #endregion

        #region MouseAxesTheory
        [Theory]
        [InlineData(10, 20, 30, 40, 50, 60, 70, 80, MouseAxes.ScrollWheel)]

        [InlineData(100, 0, 0, 0, 0, 0, 0, 0, MouseAxes.Horizontal)]
        [InlineData(90, 0, 0, 0, 0, 0, 0, 0, MouseAxes.Horizontal)]
        [InlineData(80, 0, 0, 0, 0, 0, 0, 0, MouseAxes.Horizontal)]
        [InlineData(70, 0, 0, 0, 0, 0, 0, 0, MouseAxes.Horizontal)]
        [InlineData(60, 0, 0, 0, 0, 0, 0, 0, MouseAxes.Horizontal)]
        [InlineData(50, 0, 0, 0, 0, 0, 0, 0, MouseAxes.Horizontal)]
        [InlineData(40, 0, 0, 0, 0, 0, 0, 0, MouseAxes.Horizontal)]
        [InlineData(30, 0, 0, 0, 0, 0, 0, 0, MouseAxes.Horizontal)]
        [InlineData(20, 0, 0, 0, 0, 0, 0, 0, MouseAxes.Horizontal)]
        [InlineData(10, 0, 0, 0, 0, 0, 0, 0, MouseAxes.Horizontal)]
        [InlineData(0, 0, 0, 0, 0, 0, 0, 0, MouseAxes.Horizontal)]

        [InlineData(0, 20, 0, 0, 0, 0, 0, 0, MouseAxes.Horizontal)]
        [InlineData(0, 0, 20, 0, 0, 0, 0, 0, MouseAxes.Vertical)]
        [InlineData(0, 0, 0, 20, 0, 0, 0, 0, MouseAxes.Vertical)]

        [InlineData(0, 0, 0, 0, 20, 0, 0, 0, MouseAxes.ScrollWheel)]
        [InlineData(0, 0, 0, 0, 0, 20, 0, 0, MouseAxes.ScrollWheel)]
        [InlineData(0, 0, 0, 0, 20, 0, 0, 0, MouseAxes.HorizontalScrollWheel)]
        [InlineData(0, 0, 0, 0, 0, 20, 0, 0, MouseAxes.HorizontalScrollWheel)]
        public void MouseAxesTheory(int x, int prevX, int y, int prevY, int scrollWheel, int prevScrollWheel, int horizontalScrollWheel, int prevHorizontalScrollWheel, MouseAxes mouseAxes) {
            MouseState state = new MouseState(x, y, scrollWheel, ButtonState.Released, ButtonState.Released, ButtonState.Released, ButtonState.Released, ButtonState.Released, horizontalScrollWheel);
            MouseState prevState = new MouseState(prevX, prevY, prevScrollWheel, ButtonState.Released, ButtonState.Released, ButtonState.Released, ButtonState.Released, ButtonState.Released, prevHorizontalScrollWheel);

            string actionName = "Action" + _counter++;
            InputManager.CreateAction(actionName, mouseAxes);

            InputManager.PrevMouseState = prevState;
            InputManager.MouseState = state;
            InputManager.ActionUpdate();

            float expected = 0;
            switch(mouseAxes) {
                case MouseAxes.Horizontal:
                    expected = MathHelper.Clamp((x - prevX) * InputManager.MOUSE_MODIFIER, -1, 1);
                    break;
                case MouseAxes.Vertical:
                    expected = MathHelper.Clamp((y - prevY) * InputManager.MOUSE_MODIFIER, -1, 1);
                    break;
                case MouseAxes.ScrollWheel:
                    expected = MathHelper.Clamp((scrollWheel - prevScrollWheel) * InputManager.MOUSE_MODIFIER, -1, 1);
                    break;
                case MouseAxes.HorizontalScrollWheel:
                    expected = MathHelper.Clamp((horizontalScrollWheel - prevHorizontalScrollWheel) * InputManager.MOUSE_MODIFIER, -1, 1);
                    break;
            } 
            
            Assert.Equal(expected, InputManager.GetAxis(actionName), 4);
        }
        #endregion

        #region KeyboardAxesTheoty
        [Theory]
        [InlineData(new Keys[] { }, Keys.A, Keys.D, 0)]
        [InlineData(new Keys[] { Keys.A }, Keys.A, Keys.D, -1)]
        [InlineData(new Keys[] { Keys.D }, Keys.A, Keys.D, 1)]
        [InlineData(new Keys[] { Keys.W }, Keys.A, Keys.D, 0)]
        [InlineData(new Keys[] { Keys.W, Keys.A }, Keys.A, Keys.D, -1)]
        [InlineData(new Keys[] { Keys.D, Keys.A }, Keys.A, Keys.D, 0)]
        [InlineData(new Keys[] { Keys.LeftControl, Keys.W }, Keys.LeftControl, Keys.RightControl, -1)]
        [InlineData(new Keys[] { Keys.LeftControl, Keys.W }, Keys.S, Keys.W, 1)]
        public void KeyboardAxesTheoty(Keys[] keys, Keys left, Keys right, float expected) {
            KeyboardState state = new KeyboardState(keys);
            InputManager.KeyboardState = state;

            string actionName = "Action" + _counter++;
            InputManager.CreateAction(actionName, left, right);
            InputManager.ActionUpdate();

            Assert.Equal(expected, InputManager.GetAxis(actionName));
        }
        #endregion

        #region GamePadButtonsTheory
        [Theory]
        [InlineData(new Buttons[] { Buttons.A }, Buttons.A, true)]
        [InlineData(new Buttons[] { Buttons.X }, Buttons.A, false)]
        [InlineData(new Buttons[] { Buttons.A, Buttons.LeftStick, Buttons.B }, Buttons.A, true)]
        [InlineData(new Buttons[] { Buttons.A, Buttons.LeftStick, Buttons.B }, Buttons.LeftStick, true)]
        [InlineData(new Buttons[] { Buttons.A, Buttons.LeftStick, Buttons.B }, Buttons.X, false)]
        public void GamePadButtonsTheory(Buttons[] buttons, Buttons gamePadButton, bool expected) {
            GamePadState state = new GamePadState(Vector2.Zero, Vector2.Zero, 0, 0, buttons);

            string actionName = "Action" + _counter++;
            InputManager.CreateAction(actionName, PlayerIndex.One, gamePadButton);

            InputManager.GamePadStates = new GamePadState[] { state };
            InputManager.ActionUpdate();

            Assert.Equal(expected, InputManager.GetDown(actionName));
            
        }
        #endregion

        #region MouseButtonsTheory
        [Theory]
        [InlineData(ButtonState.Released, ButtonState.Released, ButtonState.Released, ButtonState.Released, ButtonState.Released, MouseButton.Left, false)]
        [InlineData(ButtonState.Pressed, ButtonState.Released, ButtonState.Released, ButtonState.Released, ButtonState.Released, MouseButton.Left, true)]
        [InlineData(ButtonState.Pressed, ButtonState.Released, ButtonState.Pressed, ButtonState.Released, ButtonState.Released, MouseButton.Left, true)]
        [InlineData(ButtonState.Pressed, ButtonState.Pressed, ButtonState.Pressed, ButtonState.Pressed, ButtonState.Pressed, MouseButton.Left, true)]
        [InlineData(ButtonState.Pressed, ButtonState.Pressed, ButtonState.Pressed, ButtonState.Pressed, ButtonState.Pressed, MouseButton.Right, true)]
        [InlineData(ButtonState.Pressed, ButtonState.Pressed, ButtonState.Released, ButtonState.Pressed, ButtonState.Pressed, MouseButton.Right, false)]
        public void MouseButtonsTheory(ButtonState leftButton, ButtonState middleButton, ButtonState rightButton, ButtonState xButton1, ButtonState xButton2, MouseButton mouseButton, bool expected) {
            MouseState state = new MouseState(0, 0, 0, leftButton, middleButton, rightButton, xButton1, xButton2);

            string actionName = "Action" + _counter++;
            InputManager.CreateAction(actionName, mouseButton);

            InputManager.MouseState = state;
            InputManager.ActionUpdate();

            Assert.Equal(expected, InputManager.GetDown(actionName));
        }
        #endregion

        #region KeyboardButtonsTheory
        [Theory]
        [InlineData(new Keys[] { }, Keys.A, false)]
        [InlineData(new Keys[] { Keys.A }, Keys.A, true)]
        [InlineData(new Keys[] { Keys.A, Keys.D }, Keys.A, true)]
        [InlineData(new Keys[] { Keys.D }, Keys.A, false)]
        public void KeyboardButtonsTheory(Keys[] keys, Keys key, bool expected) {
            KeyboardState state = new KeyboardState(keys);

            string actionName = "Action" + _counter++;
            InputManager.CreateAction(actionName, key);

            InputManager.KeyboardState = state;
            InputManager.ActionUpdate();

            Assert.Equal(expected, InputManager.GetDown(actionName));
        }
        #endregion
    }
}
