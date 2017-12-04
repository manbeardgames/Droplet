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
