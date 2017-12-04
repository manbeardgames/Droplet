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

namespace Droplet.UIFramework
{
    public class Image : UIComponentBase
    {
        //------------------------------------------------------------
        //  Constructor

        public Image(string id, Vector2 position, Size<int> size, Texture2D texture, UIComponentBase container = null)
            : base(id, position, size, texture, container)
        {

        }

        //------------------------------------------------------------

        //------------------------------------------------------------
        //  UpdateSize
        //  Used to update the size of the image

        public void UpdateSize(Size<int> size)
        {
            Size = size;
        }

        //------------------------------------------------------------

        //------------------------------------------------------------
        //  Draw
        //  Draws the image to the scren

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)

        {
            base.Draw(gameTime, spriteBatch);
            spriteBatch.Draw(Texture, new Rectangle((int)Position.X, (int)Position.Y, Size.Width, Size.Height), Color.White);
        }

        //------------------------------------------------------------
    }
}
