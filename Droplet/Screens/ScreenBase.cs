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
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Screens;
using System;

namespace Droplet.Screens
{
    public class ScreenBase : Screen
    {
        private readonly IServiceProvider _serviceProvider;
        public SpriteBatch _spriteBatch;
        protected ContentManager Content { get; private set; }

        //------------------------------------------------------------
        //  Constructor

        protected ScreenBase(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        //------------------------------------------------------------

        //------------------------------------------------------------
        //  Initialze
        //  Used to initialize the object

        public override void Initialize()
        {
            base.Initialize();
            Content = new ContentManager(_serviceProvider, "Content");
        }

        //------------------------------------------------------------

        //------------------------------------------------------------
        //  Dispose
        //  Used by the IDisosable Interface

        public override void Dispose()
        {
            base.Dispose();
            _spriteBatch.Dispose();
        }

        //------------------------------------------------------------

        //------------------------------------------------------------
        //  LoadContent
        //  Used to load content for the object

        public override void LoadContent()
        {
            base.LoadContent();

            var graphicDeviceService = (IGraphicsDeviceService)_serviceProvider.GetService(typeof(IGraphicsDeviceService));

            _spriteBatch = new SpriteBatch(graphicDeviceService.GraphicsDevice);

        }

        //------------------------------------------------------------

        //------------------------------------------------------------
        //  UnloadContent
        //  Used to unload content for the object

        public override void UnloadContent()
        {
            Content.Unload();
            Content.Dispose();
            base.UnloadContent();
        }

        //------------------------------------------------------------
        //  Update
        //  Used to update the object each frame

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        //------------------------------------------------------------

        //------------------------------------------------------------
        //  Draw
        //  Used to draw the object to the screen

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
        }

        //------------------------------------------------------------


    }
}
