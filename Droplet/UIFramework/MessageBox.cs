using Droplet.Extensions;
using Droplet.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.BitmapFonts;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Droplet.UIFramework
{
    public class MessageBox : UIComponentBase
    {
        private string[] _textLines;        
        public string[] TextLines { get => this._textLines; set => this._textLines = value; }


        private BitmapFont _font;
        public BitmapFont Font { get => this._font; set => this._font = value; }

        private float _padding;
        public float Padding { get => this._padding; set => this._padding = value; }

        private int _index;
        public int Index { get => this._index; set => this._index = value; }


        KeyboardState _prevKeyboardState;
        MouseState _prevMouseState;

        //------------------------------------------------------------
        //  Constructor

        public MessageBox(string id, Vector2 position, Size<int> size, Texture2D texture, string text, BitmapFont font, float padding, UIComponentBase container = null)
            : base(id, position, size, texture, container)
        {
            _font = font;
            _index = -1;
            _padding = padding;

            ChangeText(text);
        }

        //------------------------------------------------------------
        //  SplitText
        //  Splits the text up into lines that can be displayed in
        //  the messgae box with word wrapping

        private void SplitText(string text)
        {
            //_textList = new List<List<string>>();
            //List<string> textList = new List<string>();

            int charsPerLine = (int)Math.Floor((Size.Width - _padding) / _font.MeasureString("A").Width);
            

            string[] words = text.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);

            string line = string.Empty;
            List<string> lines = new List<string>();
            foreach (string word in words)
            {
                if (_font.MeasureString(line).Width + _font.MeasureString(word).Width < (Size.Width - (_padding * 2)))
                {
                    line += word + " ";
                }
                else
                {
                    lines.Add(line);
                    line = word + " ";
                }
            }

            if (line.Length > 0)
            {
                lines.Add(line);
            }

            _textLines = lines.ToArray<string>();

            ////int numLines = (int)Math.Floor(Size.Height / _font.MeasureString("A").Height);
            ////List<string> display = new List<string>();
            ////foreach (string fullLine in lines)
            ////{
            ////    if (display.Count() < numLines)
            ////    {
            ////        display.Add(fullLine);
            ////    }
            ////    else
            ////    {
            ////        //_textList.Add(new List<string>(display));
            ////        textList.Add(new List<string>(display));
            ////        display.Clear();
            ////    }
            ////}
            ////if (display.Count > 0)
            ////{
            ////    _textList.Add(new List<string>(display));
            ////}   
        }

        //------------------------------------------------------------

        //------------------------------------------------------------
        //  ChangeText
        //  Used to change the text displayed in the messgae box
        public void ChangeText(string text)
        {
            SplitText(text);
            int height = 0;
            foreach (string line in _textLines)
            {
                height += (int)Math.Ceiling(_font.MeasureString(line).Height);
            }

            Size.Height = height + (int)_font.MeasureString("A").Height;
            BoundingBox = new Rectangle
            {
                X = (int)Position.X,
                Y = (int)Position.Y,
                Width = Size.Width + (int)_padding,
                Height = Size.Height
            };

            GenerateNineSlice();
        }

        //------------------------------------------------------------


        //------------------------------------------------------------
        //  Initialize
        //  Initializes the message box

        public override void Initialize()
        {
            base.Initialize();

        }

        //------------------------------------------------------------

        //------------------------------------------------------------
        //  Dispose
        //  Used by the IDisposable Interface

        public override void Dispose()
        {
            base.Dispose();
        }

        //------------------------------------------------------------

        //------------------------------------------------------------
        //  Update
        //  Used to update each frame

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            KeyboardState keyboardState = Keyboard.GetState();
            MouseState mouseState = Mouse.GetState();

            if((keyboardState.IsKeyDown(Keys.Enter) && _prevKeyboardState.IsKeyUp(Keys.Enter)) ||
                (mouseState.LeftButton == ButtonState.Pressed && _prevMouseState.LeftButton == ButtonState.Released))
            {
                NotifyOnClick();
            }



            _prevKeyboardState = keyboardState;
            _prevMouseState = mouseState;

        }

        //------------------------------------------------------------

        //------------------------------------------------------------
        //  OnClick
        //  Event callback used to signel when the message box has detected
        //  a click event

        public delegate void OnClickHandler(MessageBox sender);
        public event OnClickHandler OnClick;
        private void NotifyOnClick()
        {
            OnClick?.Invoke(this);
        }

        //------------------------------------------------------------


        //------------------------------------------------------------
        //  Draw
        //  Draws to the screen

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            base.Draw(gameTime, spriteBatch);

            if (Container == null)
            {
                var transformMatrix = CacheManager.Camera.GetViewMatrix(Vector2.Zero);
                spriteBatch.Begin(transformMatrix: transformMatrix);
            }

            {

                if (Texture != null && NineSlices != null)
                {
                    foreach (NineSlice slice in NineSlices)
                    {
                        spriteBatch.Draw(
                            texture: Texture,
                            position: slice.Position,
                            sourceRectangle: slice.SourceRectangle,
                            color: Color.White,
                            rotation: 0.0f,
                            origin: Vector2.Zero,
                            scale: slice.Scale,
                            effects: SpriteEffects.None,
                            layerDepth: 0.0f);
                    }
                }



                for (int i = 0; i < _textLines.Count(); i++)
                {
                    spriteBatch.DrawString(
                        font: _font,
                        text: _textLines[i],
                        position: new Vector2(Position.X + _padding, Position.Y + (_font.MeasureString("A").Height * i)),
                        color: Color.White);

                }
            }


            if (Container == null)
            {
                spriteBatch.End();
            }
        }

        //------------------------------------------------------------

    }
}
