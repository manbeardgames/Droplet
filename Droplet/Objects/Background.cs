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
using System;

namespace Droplet.Objects
{
    public class Background
    {
        private const int PPU = 128;

        private Texture2D _texture;
        private Color _primaryColor = new Color(new Vector4(8.0f / 255.0f, 30.0f / 255.0f, 60.0f / 255.0f, 1.0f));
        private Color _secondaryColor = Color.Multiply(new Color(new Vector4(8.0f / 255.0f, 30.0f / 255.0f, 60.0f / 255.0f, 1.0f)), 0.95f) * 1.0f;

        private Size<float> _cellSize;
        private int _numWide = 32*2;
        private int _numTall;

        //------------------------------------------------------------
        //  Constructor

        public Background()
        {

            _cellSize = new Size<float>
            {
                Width = CacheManager.WorldSize.Width / _numWide,
                Height = CacheManager.WorldSize.Width / _numWide
            };

            _numTall = (int)Math.Floor(CacheManager.WorldSize.Height / _cellSize.Height);

            _texture = CacheManager.LoadTexture("background/cell");

        }

        //------------------------------------------------------------

        //------------------------------------------------------------
        //  Draw
        //  Draws the background to the screen
        //  TODO: I should instead of doing the loops to draw, render
        //        to a texture first and just draw the texture here

        public void Draw(GameTime gameTime)
        {
            CacheManager.GameMain.GraphicsDevice.Clear(Color.Black);
            var transformMatrix = CacheManager.Camera.GetViewMatrix();
            CacheManager.SpriteBatch.Begin(transformMatrix: transformMatrix);
            {
                for(int x = 0; x < _numWide; x++)
                {
                    for(int y = 0; y < _numTall; y++)
                    {
                        if(x % 2 == 0 && y % 2 == 0)
                        {
                            CacheManager.SpriteBatch.Draw(
                                texture: _texture,
                                position: new Vector2
                                {
                                    X = x * (float)_cellSize.Width,
                                    Y = y * (float)_cellSize.Height
                                },
                                sourceRectangle: null,
                                color: _secondaryColor,
                                rotation: 0.0f,
                                origin: Vector2.Zero,
                                scale: new Vector2
                                { 
                                    X = (float)_cellSize.Width / (float)_texture.Width,
                                    Y = (float)_cellSize.Height / (float)_texture.Height
                                },
                                effects: SpriteEffects.None,
                                layerDepth: 0.0f);
                        }
                        else
                        {
                            CacheManager.SpriteBatch.Draw(
                                texture: _texture,
                                position: new Vector2
                                {
                                    X = x * (float)_cellSize.Width,
                                    Y = y * (float)_cellSize.Height
                                },
                                sourceRectangle: null,
                                color: _primaryColor,
                                rotation: 0.0f,
                                origin: Vector2.Zero,
                                scale: new Vector2
                                {
                                    X = (float)_cellSize.Width / (float)_texture.Width,
                                    Y = (float)_cellSize.Height / (float)_texture.Height
                                },
                                effects: SpriteEffects.None,
                                layerDepth: 0.0f);
                        }
                    }
                }
            }
            CacheManager.SpriteBatch.End();
        }

        //------------------------------------------------------------



    }
}
