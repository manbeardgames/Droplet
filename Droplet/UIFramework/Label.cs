﻿//================================================================================ 
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
using MonoGame.Extended.BitmapFonts;

namespace Droplet.UIFramework
{
    public class Label : UIComponentBase
    {
        

        private BitmapFont _font;
        public BitmapFont Font { get => this._font; set => this._font = value; }

        private string _text;
        public string Text { get => this._text; set => this._text = value; }

        private Color _color;
        public Color Color { get => this._color; set => this._color = value; }

        private Vector2 _textPosition;
        public Vector2 TextPosition { get => this._textPosition; set => this._textPosition = value; }

        private TextAlignment _textAlignment;
        public TextAlignment TextAlignment { get => this._textAlignment; set => this._textAlignment = value; }





        //---------------------------------------------------
        //  Constructor

        public Label(string id, Vector2 position, Size<int> size, Texture2D texture, UIComponentBase container, BitmapFont font, string text, TextAlignment alignment, Color color)
            : base(id, position, size, texture, container)
        {
            UIType = UIType.Label;
            _font = font;
            _text = text;
            _color = color;

            BoundingBox = new Rectangle
            {
                X = (int)Position.X,
                Y = (int)Position.Y,
                Width = (int)_font.MeasureString(_text).Width,
                Height = (int)_font.MeasureString(_text).Height
            };

            SetPosition(alignment);

        }

        //---------------------------------------------------

        //---------------------------------------------------
        //  Update

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        //---------------------------------------------------

        //---------------------------------------------------
        //  Draw

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if (Container == null)
            {
                var transformMatrix = CacheManager.Camera.GetViewMatrix(Vector2.Zero);
                spriteBatch.Begin(transformMatrix: transformMatrix);
                {
                    spriteBatch.DrawString(_font, _text, this.Position + _textPosition, _color);
                }
                spriteBatch.End();
            }
            else
            {
                spriteBatch.DrawString(_font, _text, this.Position + _textPosition, _color);
            }
            
            base.Draw(gameTime, spriteBatch);
        }

        //---------------------------------------------------

        //---------------------------------------------------
        //  SetPostion
        //  Calcuates position based on the alignment given

        private void SetPosition(TextAlignment textAlignment)
        {
            _textAlignment = textAlignment;

            switch (_textAlignment)
            {
                case TextAlignment.TopLeft:
                    _textPosition = Vector2.Zero;
                    break;
                case TextAlignment.TopCenter:
                    _textPosition = new Vector2((this.Size.Width / 2.0f) - (_font.MeasureString(_text).Width / 2.0f), 0);
                    break;
                case TextAlignment.TopRight:
                    _textPosition = new Vector2(this.Size.Width - _font.MeasureString(_text).Width, 0);
                    break;
                case TextAlignment.MiddleLeft:
                    _textPosition = new Vector2(0, (this.Size.Height / 2.0f) - (_font.MeasureString(_text).Height / 2.0f));
                    break;
                case TextAlignment.Middle:
                    _textPosition = new Vector2((this.Size.Width / 2.0f) - (_font.MeasureString(_text).Width / 2.0f), (this.Size.Height / 2.0f) - (_font.MeasureString(_text).Height / 2.0f));
                    break;
                case TextAlignment.MiddleRight:
                    _textPosition = new Vector2(this.Size.Width - _font.MeasureString(_text).Width, (this.Size.Height / 2.0f) - (_font.MeasureString(_text).Height / 2.0f));
                    break;
                case TextAlignment.BottomLeft:
                    _textPosition = new Vector2(0, this.Size.Height - _font.MeasureString(_text).Height);
                    break;
                case TextAlignment.BottomCenter:
                    _textPosition = new Vector2((this.Size.Width / 2.0f) - (_font.MeasureString(_text).Width / 2.0f), this.Size.Height - _font.MeasureString(_text).Height);
                    break;
                case TextAlignment.BottomrRight:
                    _textPosition = new Vector2(this.Size.Width - _font.MeasureString(_text).Width, this.Size.Height - _font.MeasureString(_text).Height);
                    break;
                default:
                    break;
            }

        }

        //---------------------------------------------------

        //---------------------------------------------------
        //  UpdateText
        //  Updates the text string and recauluates position for alignment

        public void UpdateText(string newText)
        {
            _text = newText;
            SetPosition(_textAlignment);
        }

        //---------------------------------------------------
    }
}
