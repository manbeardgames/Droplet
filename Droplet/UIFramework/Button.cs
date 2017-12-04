//================================================================================ 
/*
MIT License

Copyright(c) 2017 Christopher Whitley(ManBeardGames)

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.

*/
//================================================================================ 

using Droplet.Extensions;
using Droplet.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.BitmapFonts;

namespace Droplet.UIFramework
{
    public class Button : UIComponentBase
    {
        public static float _globalClickDelay = 0.0f;


        private Color _backgroundColor;
        public Color BackgrounColor { get => this._backgroundColor; set => this._backgroundColor = value; }

        private Color _selectedColor;
        public Color SelectedColor { get => this._selectedColor; set => this._selectedColor = value; }

        private bool _isSelected;
        public bool IsSelected { get => this._isSelected; set => this._isSelected = value; }

        private UIComponentBase _upSelection;
        public UIComponentBase UpSelection { get => this._upSelection; set => this._upSelection = value; }

        private UIComponentBase _downSelection;
        public UIComponentBase DownSelection { get => this._downSelection; set => this._downSelection = value; }

        private UIComponentBase _leftSelection;
        public UIComponentBase LeftSelection { get => this._leftSelection; set => this._leftSelection = value; }

        private UIComponentBase _rightSelection;
        public UIComponentBase RightSelection { get => this._rightSelection; set => this._rightSelection = value; }

        private Label _label;
        public Label Label { get => this._label; set => this._label = value; }

        private MouseState _prevMouseState;
        //private KeyboardState _prevKeyboardState;

        //------------------------------------------------------------
        //  Constructor

        public Button(string id, Vector2 position, Size<int> size, Texture2D texture, UIComponentBase container, BitmapFont font, string text, TextAlignment textAlignment, Color fontColor, Color backgroundColor, Color selectedColor)
            : base(id, position, size, texture, container)
        {
            this.UIType = UIType.Button;

            _backgroundColor = backgroundColor;
            _selectedColor = selectedColor;

            _label = new Label(
                id: $"{id}_label",
                position: position,
                size: size,
                texture: null,
                container: this,
                font: font,
                text: text,
                alignment: textAlignment,
                color: fontColor);
        }

        //------------------------------------------------------------
        //  Update
        //  Used to update the button each frame

        public override void Update(GameTime gameTime)
        {
            MouseState mouseState = Mouse.GetState();
            var worldPosition = CacheManager.Camera.ScreenToWorld(new Vector2(mouseState.X, mouseState.Y));

            if (BoundingBox.Contains(worldPosition))
            {
                _isSelected = true;
                NotifyOnSelected();
            }
            else
            {
                _isSelected = false;
            }

            if (_isSelected)
            {
                if (mouseState.LeftButton == ButtonState.Pressed && _prevMouseState.LeftButton == ButtonState.Released)
                {
                    if (Button._globalClickDelay <= 0.0f)
                    {
                        Button._globalClickDelay = 0.5f;
                        NotifyOnClick();
                    }

                }
            }

            _prevMouseState = mouseState;

            base.Update(gameTime);
        }
        //------------------------------------------------------------

        //------------------------------------------------------------
        //  Draw
        //  Used to draw the button

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {

            if(Container == null)
            {
                var transformMatrix = CacheManager.Camera.GetViewMatrix(Vector2.Zero);
                spriteBatch.Begin(transformMatrix: transformMatrix);
            }

            if (Texture != null && NineSlices != null)
            {
                foreach (NineSlice slice in NineSlices)
                {
                    spriteBatch.Draw(
                        texture: Texture,
                        position: slice.Position,
                        sourceRectangle: slice.SourceRectangle,
                        color: _isSelected ? _selectedColor : _backgroundColor,
                        rotation: 0.0f,
                        origin: Vector2.Zero,
                        scale: slice.Scale,
                        effects: SpriteEffects.None,
                        layerDepth: 0.0f);
                }
            }

            _label.Draw(gameTime, spriteBatch);

            if (Container == null)
            {
                spriteBatch.End();
            }

            base.Draw(gameTime, spriteBatch);
        }

        //------------------------------------------------------------

        //------------------------------------------------------------
        //  OnClick
        //  Event callback used to notify that the button has been clicked
        public delegate void OnClickHandler(Button sender);
        public event OnClickHandler OnClick;
        private void NotifyOnClick()
        {
            OnClick?.Invoke(this);
        }

        //------------------------------------------------------------

        //------------------------------------------------------------
        //  OnSelect
        //  Event callback used to notify that the button has been selected
        
        public delegate void OnSelectedHandler(Button sender);
        public event OnSelectedHandler OnSelected;
        private void NotifyOnSelected()
        {
            OnSelected?.Invoke(this);
        }

        //------------------------------------------------------------

        //------------------------------------------------------------
        //  OnDeslect
        //  Event callback used to notify that the button has been deslected

        public delegate void OnDeselectHander(Button sender);
        public event OnDeselectHander OnDeselected;
        private void NotifyOnDeselect()
        {
            OnDeselected?.Invoke(this);
        }

        //------------------------------------------------------------

        //------------------------------------------------------------
        //  Dispoase
        //  Used by the IDisposable interface

        public override void Dispose()
        {
            if (this.OnClick != null)
            {
                foreach (var d in OnClick.GetInvocationList())
                {
                    OnClick -= (d as OnClickHandler);
                }
            }

            if (this.OnSelected != null)
            {
                foreach (var d in OnSelected.GetInvocationList())
                {
                    OnSelected -= (d as OnSelectedHandler);
                }
            }

            if (this.OnDeselected != null)
            {
                foreach (var d in OnDeselected.GetInvocationList())
                {
                    OnDeselected -= (d as OnDeselectHander);
                }
            }

            base.Dispose();

        }

        //------------------------------------------------------------
    }


}

