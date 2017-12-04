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
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace Droplet.UIFramework
{
    public class UIComponentBase : IDisposable
    {
        //------------------------------------------------------------
        //  Properties

        private string _id;
        public string Id { get => this._id; set => this._id = value; }

        private UIType _uiType;
        public UIType UIType { get => this._uiType; set => this._uiType = value; }

        private Vector2 _position;
        public Vector2 Position { get => this._position; set => this._position = value; }

        private Size<int> _size;
        public Size<int> Size { get => this._size; set => this._size = value; }

        private Rectangle _boundingBox;
        public Rectangle BoundingBox { get => this._boundingBox; set => this._boundingBox = value; }

        private Texture2D _texture;
        public Texture2D Texture { get => this._texture; set => this._texture = value; }

        private List<NineSlice> _nineSlices;
        public List<NineSlice> NineSlices { get => this._nineSlices; set => this._nineSlices = value; }

        private UIComponentBase _container;
        public UIComponentBase Container { get => this._container; set => this._container = value; }

        //------------------------------------------------------------

        //------------------------------------------------------------
        //  Constructors

        public UIComponentBase(string id, Vector2 position, Size<int> size, Texture2D texture, UIComponentBase container = null)
        {
            _id = id;
            _uiType = UIType.Component;
            _position = position;
            _size = size;
            _container = container;

            _boundingBox = new Rectangle
            {
                X = (int)_position.X,
                Y = (int)_position.Y,
                Width = _size.Width,
                Height = _size.Height
            };

            _texture = texture;
            if(_texture != null)
            {
                GenerateNineSlice();
            }


        }
        //------------------------------------------------------------

        //------------------------------------------------------------
        //  Initialze
        //  Initializes the UIComponent

        public virtual void Initialize() { }
        //------------------------------------------------------------

        //------------------------------------------------------------
        //  Update
        public virtual void Update(GameTime gameTime) { }
        //------------------------------------------------------------

        //------------------------------------------------------------
        //  Draw 
        public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch) { }
        //------------------------------------------------------------

        public void GenerateNineSlice()
        {
            int padding = 3;
            _nineSlices = new List<NineSlice>();

            Rectangle topLeftRect = new Rectangle(0, 0, padding, padding);
            Vector2 topLeftPos = new Vector2(_position.X, _position.Y);
            Vector2 topLeftScale = new Vector2(1, 1);
            _nineSlices.Add(new NineSlice(topLeftRect, topLeftPos, topLeftScale));

            Rectangle topMidRect = new Rectangle(padding, 0, _texture.Width - (padding * 2), padding);
            Vector2 topMidPos = new Vector2(_position.X + padding, _position.Y);
            Vector2 topMidScale = new Vector2((this.BoundingBox.Width - (padding * 2.0f)) / topMidRect.Width, 1);
            _nineSlices.Add(new NineSlice(topMidRect, topMidPos, topMidScale));

            Rectangle topRightRect = new Rectangle(_texture.Width - padding, 0, padding, padding);
            Vector2 topRightPos = new Vector2(_position.X + this.BoundingBox.Width - padding, _position.Y);
            Vector2 topRightScale = new Vector2(1, 1);
            _nineSlices.Add(new NineSlice(topRightRect, topRightPos, topRightScale));

            Rectangle leftMidRect = new Rectangle(0, padding, padding, _texture.Height - (padding * 2));
            Vector2 leftMidPos = new Vector2(_position.X, _position.Y + padding);
            Vector2 leftMidScale = new Vector2(1, (this.BoundingBox.Height - (padding * 2.0f)) / leftMidRect.Height);
            _nineSlices.Add(new NineSlice(leftMidRect, leftMidPos, leftMidScale));

            Rectangle midRect = new Rectangle(padding, padding, _texture.Width - (padding * 2), _texture.Height - (padding * 2));
            Vector2 midPos = new Vector2(_position.X + padding, _position.Y + padding);
            Vector2 midScale = new Vector2((this.BoundingBox.Width - (padding * 2.0f)) / midRect.Width, (this.BoundingBox.Height - (padding * 2.0f)) / midRect.Height);
            _nineSlices.Add(new NineSlice(midRect, midPos, midScale));

            Rectangle rightMidRect = new Rectangle(_texture.Width - padding, padding, padding, _texture.Height - (padding * 2));
            Vector2 rightMidPos = new Vector2(_position.X + this.BoundingBox.Width - padding, _position.Y + padding);
            Vector2 rightMidScale = new Vector2(1, (this.BoundingBox.Height - (padding * 2.0f)) / rightMidRect.Height);
            _nineSlices.Add(new NineSlice(rightMidRect, rightMidPos, rightMidScale));

            Rectangle botLeftRect = new Rectangle(0, _texture.Height - padding, padding, padding);
            Vector2 botLeftPos = new Vector2(_position.X, _position.Y + this.BoundingBox.Height - padding);
            Vector2 botLeftScale = new Vector2(1, 1);
            _nineSlices.Add(new NineSlice(botLeftRect, botLeftPos, botLeftScale));

            Rectangle botMidRect = new Rectangle(padding, _texture.Height - padding, _texture.Width - (padding * 2), padding);
            Vector2 botMidPos = new Vector2(_position.X + padding, _position.Y + this.BoundingBox.Height - padding);
            Vector2 botMidScale = new Vector2((this.BoundingBox.Width - (padding * 2.0f)) / botMidRect.Width, 1);
            _nineSlices.Add(new NineSlice(botMidRect, botMidPos, botMidScale));

            Rectangle botRightRect = new Rectangle(_texture.Width - padding, _texture.Height - padding, padding, padding);
            Vector2 botRightPos = new Vector2(_position.X + this.BoundingBox.Width - padding, _position.Y + this.BoundingBox.Height - padding);
            Vector2 botRightScale = new Vector2(1, 1);
            _nineSlices.Add(new NineSlice(botRightRect, botRightPos, botRightScale));


        }

        //------------------------------------------------------------
        //  Dispose
        //  Used by IDisposable interface
        public virtual void Dispose()
        {
            
        }
        //------------------------------------------------------------
    }
}
