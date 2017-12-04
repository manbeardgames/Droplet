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

using Microsoft.Xna.Framework;

namespace Droplet.UIFramework
{
    public class NineSlice
    {
        private Rectangle _sourceRectangle;
        public Rectangle SourceRectangle { get => this._sourceRectangle; set => this._sourceRectangle = value; }

        private Vector2 _posiion;
        public Vector2 Position { get => this._posiion; set => this._posiion = value; }

        private Vector2 _scale;
        public Vector2 Scale { get => this._scale; set => this._scale = value; }

        //------------------------------------------------------------
        //  Constructor

        public NineSlice(Rectangle sourceRectangle, Vector2 position, Vector2 scale)
        {
            _sourceRectangle = sourceRectangle;
            _posiion = position;
            _scale = scale;
        }

        //------------------------------------------------------------
    }
}
