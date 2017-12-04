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

using Droplet.Managers;
using Droplet.Objects;
using Droplet.Repositories;
using Droplet.Screens;
using Droplet.UIFramework;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using MonoGame.Extended;
using MonoGame.Extended.Screens;
using MonoGame.Extended.ViewportAdapters;
using System.Runtime.InteropServices;

namespace Droplet
{

    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class GameMain : Game
    {
        [DllImport("user32.dll")]
        static extern void ClipCursor(ref Rectangle rect);

        public GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Background _background;
        
        public GameMain()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            CacheManager.ContentManager = Content;
            IsMouseVisible = true;
            Window.AllowUserResizing = false;
            Window.Position = Point.Zero;

            graphics.GraphicsProfile = GraphicsProfile.HiDef;
            graphics.PreferredBackBufferWidth = CacheManager.VirtualSize.Width;
            graphics.PreferredBackBufferHeight = CacheManager.VirtualSize.Height;
            graphics.HardwareModeSwitch = false;

            GameSettingsRepository.Initialize();

            graphics.IsFullScreen = GameSettingsRepository.GameSettings.IsFullScreen;

            ScreenGameComponent screenComponent;
            Components.Add(screenComponent = new ScreenGameComponent(this));
            screenComponent.Register(new TitleScreen(Services, this));
            screenComponent.Register(new PlayScreen(Services, this));

            CacheManager.GameMain = this;
        }   // Awake

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            CacheManager.ViewportAdapter = new ScalingViewportAdapter(GraphicsDevice, CacheManager.VirtualSize.Width, CacheManager.VirtualSize.Height);
            CacheManager.Camera = new OrthographicCamera(CacheManager.ViewportAdapter);

            base.Initialize();
        }   // Start

        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            CacheManager.SpriteBatch = spriteBatch;

            _background = new Background();
            MediaPlayer.IsRepeating = true;
            //MediaPlayer.Volume = GameSettingsRepository.GameSettings.MusicVolume;
            MediaPlayer.Volume = GameSettingsRepository.GameSettings.MusicVolume;
            MediaPlayer.Play(Content.Load<Song>("audio/music"));
            MediaPlayer.IsMuted = !GameSettingsRepository.GameSettings.MusicOn;

            //  Load sound effects
            CacheManager.IncreaseFX = Content.Load<SoundEffect>("audio/increase");
            CacheManager.DecreaseFx = Content.Load<SoundEffect>("audio/decrease");


            // TODO: use this.Content to load your game content here
        }   

        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        protected override void Update(GameTime gameTime)
        {

            // TODO: Add your update logic here
            var deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            var keyboardState = Keyboard.GetState();
            var mouseState = Mouse.GetState();

            Button._globalClickDelay = MathHelper.Clamp(Button._globalClickDelay - deltaTime, 0.0f, 1.0f);

            base.Update(gameTime);

        }   // Update

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            Matrix transformMatrix = CacheManager.Camera.GetViewMatrix();

            // TODO: Add your drawing code here
                _background.Draw(gameTime);
            spriteBatch.Begin(transformMatrix: transformMatrix);
            {
            }
            spriteBatch.End();


            base.Draw(gameTime);
        }   //  Draw
    }
}
