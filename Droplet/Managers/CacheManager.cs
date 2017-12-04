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
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using MonoGame.Extended;
using MonoGame.Extended.BitmapFonts;
using MonoGame.Extended.ViewportAdapters;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Droplet.Managers
{
    public class CacheManager
    {
        public static GameMain GameMain;

        public static Size<int> VirtualSize = new Size<int>(1280, 720);

        public static Size<int> WorldSize = new Size<int>(1280 , 720 );

        public static Vector2 BoundryTopLeft = Vector2.Zero;
        public static Vector2 BoundryBottomRight = new Vector2(WorldSize.Width, WorldSize.Height);

        public static Rectangle InnerBoundingBox = new Rectangle(960, 540, 640, 360);

        public static RectangleF CameraSensor = new RectangleF(480, 270, 320, 180 );

        public static RectangleF GetCameraSensorRect()
        {
            if (CacheManager.Camera == null) return new RectangleF();
            Vector2 xy = Camera.ScreenToWorld(new Vector2(960.0f, 540.0f));
            return new RectangleF(
                x: xy.X,
                y: xy.Y,
                width: 640,
                height: 180);
        }


        public static SoundEffect IncreaseFX { get; set; }
        public static SoundEffect DecreaseFx { get; set; }

        

        public static ContentManager ContentManager { get; set; }


        public static ScalingViewportAdapter ViewportAdapter { get; set; }
        public static OrthographicCamera Camera { get; set; }
        public static SpriteBatch SpriteBatch { get; set; }


        public static bool shouldClipCursor { get; set; }

        public static Dictionary<string, BitmapFont> _cachedFonts = new Dictionary<string, BitmapFont>();

        public static Dictionary<string, Texture2D> _cachedTextures = new Dictionary<string, Texture2D>();

        public static Dictionary<string, Song> _cachedAudio = new Dictionary<string, Song>();

        //------------------------------------------------------------
        //  LoadFont
        //  Attempts to load font from the cache, if not there
        //  then loads from the content manager

        public static BitmapFont LoadFont(string filename)
        {
            if (!_cachedFonts.ContainsKey(filename))
            {
                try
                {
                    _cachedFonts[filename] = ContentManager.Load<BitmapFont>(filename);
                }
                catch (Exception ex)
                {

                    Debug.WriteLine($"{ex}");
                }
            }

            return _cachedFonts[filename];
        }

        //------------------------------------------------------------

        //------------------------------------------------------------
        //  LoadTexture
        //  Loads a font from cache. If font is not in cache, attempts to load from content manager
        //  and then caches

        public static Texture2D LoadTexture(string filename)
        {
            if (!_cachedTextures.ContainsKey(filename))
            {
                try
                {
                    _cachedTextures[filename] = ContentManager.Load<Texture2D>(filename);
                }
                catch (Exception ex)
                {

                    Debug.WriteLine($"{ex}");
                }
            }

            return _cachedTextures[filename];
        }

        //------------------------------------------------------------


        //------------------------------------------------------------
        //  LoadAudio
        //  Attempts to load audio from the cache. If it's not there
        //  will load it from the content manager
        
        public static Song LoadAudio(string filename)
        {
            if (!_cachedAudio.ContainsKey(filename))
            {
                try
                {
                    _cachedAudio[filename] = ContentManager.Load<Song>(filename);
                }
                catch (Exception ex)
                {

                    Debug.WriteLine($"{ex}");
                }
            }

            return _cachedAudio[filename];
        }

        //------------------------------------------------------------


    }
}
