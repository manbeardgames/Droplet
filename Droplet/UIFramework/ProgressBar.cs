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

namespace Droplet.UIFramework
{
    public class ProgressBar : UIComponentBase
    {
        private Image _image;
        public Image Image { get => this._image; set => this._image = value; }

        private float _percentage;
        public float Percentage { get => this._percentage; set => this._percentage = value; }




        //------------------------------------------------------------
        //  Constructor
        public ProgressBar(string id, Vector2 position, Size<int> size, Texture2D texture, UIComponentBase container = null)
            : base(id, position, size, texture, container)
        {
            this._image = new Image
                (
                id: $"{id}_image",
                position: position,
                size: new Size<int>(0, size.Height - 5),
                texture: CacheManager.LoadTexture("ui/progressimage"),
                container: this);

        }
        //------------------------------------------------------------

        //------------------------------------------------------------
        //  UpdatePercentage
        //  Updates the percentage value and adjusts the image shown on the bar

        public void UpdatePercentage(float value, float outOf)
        {
            _percentage = value / outOf;

            if (_percentage >= 1.0f)
            {
                //  level up
                NotifyFullPercentage();

                _percentage = 0.0f;
            }
            Size<int> newImageSize = new Size<int>((int)(Size.Width * _percentage), Size.Height);
            _image.UpdateSize(newImageSize);



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
                if (NineSlices != null && Texture != null)
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
                            layerDepth: 0.0f
                                );
                    }
                }

                _image.Draw(gameTime, spriteBatch);
            }

            if (Container == null)
            {
                spriteBatch.End();
            }
        }

        //------------------------------------------------------------

        //------------------------------------------------------------
        //  OnFullPercentage Implementation
        //  Event callback used for componnets that need to know when
        //  progress has reached 100%

        public delegate void OnFullPercentageHandler(ProgressBar sender);
        public event OnFullPercentageHandler OnFullPercentage;
        private void NotifyFullPercentage()
        {
            OnFullPercentage?.Invoke(this);
        }

        //------------------------------------------------------------

    }
}
