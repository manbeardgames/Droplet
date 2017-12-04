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
using Droplet.Objects;
using Droplet.Repositories;
using Droplet.UIFramework;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using MonoGame.Extended.BitmapFonts;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Droplet.Screens
{
    public class TitleScreen : ScreenBase
    {
        private readonly Game _game;
        private MouseState _mouseState;
        private Vector2 _worldPosition;

        private Menu _mainMenu;
        private Menu _optionsMenu;
        private Menu _skipHowToPlayMenu;
        private Menu _creditsMenu;
        private Menu _currentMenu;


        private List<Actor> _actors;
        private Random rand;

        //------------------------------------------------------------
        //  Constructor

        public TitleScreen(IServiceProvider serviceProvider, Game game)
            : base(serviceProvider)
        {
            _game = game;
            rand = new Random();
            _actors = new List<Actor>();
        }

        //------------------------------------------------------------


        //------------------------------------------------------------
        //  Initalize
        //  Initializes the title screen

        public override void Initialize()
        {
            base.Initialize();


        }

        //------------------------------------------------------------

        //------------------------------------------------------------
        //  Dispose
        //  Used by the IDisposable interface

        public override void Dispose()
        {
            base.Dispose();
        }

        //------------------------------------------------------------

        //------------------------------------------------------------
        //  LoadContent
        //  Loads content for the screen

        public override void LoadContent()
        {
            base.LoadContent();


            BuildMainMenu();
            BuildHowToPlayMenu();
            BuildOptionMenu();
            BuildCreditsMenu();
            _currentMenu = _mainMenu;

            for(int i = 0; i < 100; i++)
            {
                SpawnActors();
            }
        }

        //------------------------------------------------------------

        public override void UnloadContent()
        {
            base.UnloadContent();
        }

        //------------------------------------------------------------

        //------------------------------------------------------------
        //  Update
        //  Updates the title screen each frame

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            _mouseState = Mouse.GetState();
            _worldPosition = CacheManager.Camera.ScreenToWorld(new Vector2(_mouseState.X, _mouseState.Y));
            _currentMenu.Update(gameTime);
            UpdateActors(gameTime);
            

        }

        //------------------------------------------------------------

        //------------------------------------------------------------
        //  Draw
        //  Draws the tite screen each frame

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
            var destinationRectangle = new Rectangle(0, 0, CacheManager.VirtualSize.Width, CacheManager.VirtualSize.Height);
            var transformMatrix = CacheManager.Camera.GetViewMatrix(Vector2.Zero);

            foreach (Actor actor in _actors)
            {
                actor.Draw(gameTime);
            }

            _spriteBatch.Begin(transformMatrix: transformMatrix);
            {
                _currentMenu.Draw(gameTime, _spriteBatch);
            }
            _spriteBatch.End();

        }

        //------------------------------------------------------------

        //------------------------------------------------------------
        //  BuildMainMenu
        //  Builds the main menu used by the title screen

        private void BuildMainMenu()
        {
            BitmapFont buttonFont = CacheManager.LoadFont("fonts/sneakattack-16");
            BitmapFont titleFont = CacheManager.LoadFont("fonts/sneakattack-42");

            Size<int> menuSize = new Size<int>
            {
                Width = 1280,
                Height = (int)titleFont.MeasureString("A").Height + 10
                 + (int)((buttonFont.MeasureString("A").Height + 10) * 4) + 25

            };

            Vector2 menuPosition = new Vector2
            {
                X = 0,
                Y = (CacheManager.VirtualSize.Height / 2.0f) - (menuSize.Height / 2.0f)
            };



            _mainMenu = new Menu(
                id: "_mainMenu",
                position: menuPosition,
                size: menuSize,
                texture: CacheManager.LoadTexture("ui/background"));

            Label titleLabel = _mainMenu.AddLabel(
                id: "_titleLabel",
                position: new Vector2(_mainMenu.Position.X + 0, _mainMenu.Position.Y),
                size: new Size<int>(_mainMenu.Size.Width, (int)titleFont.MeasureString("A").Height + 10),
                font: titleFont,
                text: "Droplet",
                alignment: TextAlignment.TopCenter,
                color: Color.Black);

            Button playButton = _mainMenu.AddButton(
                                            id: "_playButton",
                                            position: new Vector2(_mainMenu.Position.X, titleLabel.Position.Y + titleLabel.Size.Height),
                                            size: new Size<int>(_mainMenu.Size.Width, (int)buttonFont.MeasureString("A").Height + 10),
                                            texture: CacheManager.LoadTexture("ui/background"),
                                            font: buttonFont,
                                            text: "Play",
                                            alignment: TextAlignment.Middle,
                                            fontColor: Color.Black,
                                            backgroundColor: Color.White,
                                            selectedColor: Color.LightGray);

            Button optionsButton = _mainMenu.AddButton(
                                            id: "_optionsButton",
                                            position: new Vector2(0, playButton.Position.Y + playButton.Size.Height + 5),
                                            size: new Size<int>(_mainMenu.Size.Width, (int)buttonFont.MeasureString("A").Height + 10),
                                            texture: CacheManager.LoadTexture("ui/background"),
                                            font: buttonFont,
                                            text: "Options",
                                            alignment: TextAlignment.Middle,
                                            fontColor: Color.Black,
                                            backgroundColor: Color.White,
                                            selectedColor: Color.LightGray);

            Button creditsButton = _mainMenu.AddButton(
                                            id: "_credtisButton",
                                            position: new Vector2(0, optionsButton.Position.Y + optionsButton.Size.Height + 5),
                                            size: new Size<int>(_mainMenu.Size.Width, (int)buttonFont.MeasureString("A").Height + 10),
                                            texture: CacheManager.LoadTexture("ui/background"),
                                            font: buttonFont,
                                            text: "Credits",
                                            alignment: TextAlignment.Middle,
                                            fontColor: Color.Black,
                                            backgroundColor: Color.White,
                                            selectedColor: Color.LightGray);

            Button exitButton = _mainMenu.AddButton(
                                            id: "_exitButton",
                                            position: new Vector2(0, creditsButton.Position.Y + creditsButton.Size.Height + 5),
                                            size: new Size<int>(_mainMenu.Size.Width, (int)buttonFont.MeasureString("A").Height + 10),
                                            texture: CacheManager.LoadTexture("ui/background"),
                                            font: buttonFont,
                                            text: "Exit",
                                            alignment: TextAlignment.Middle,
                                            fontColor: Color.Black,
                                            backgroundColor: Color.White,
                                            selectedColor: Color.LightGray);


            playButton.OnClick += PlayModeButton_OnClick;
            optionsButton.OnClick += OptionsButton_OnClick;
            creditsButton.OnClick += CreditsButton_OnClick;
            exitButton.OnClick += ExitButton_OnClick;
        }

        //------------------------------------------------------------
        //  CreditsButton_OnClick
        //  Called when the credits button is clicked on the main menu
        private void CreditsButton_OnClick(Button sender)
        {
            _currentMenu = _creditsMenu;
        }

        //------------------------------------------------------------

        //------------------------------------------------------------

        //------------------------------------------------------------
        //  ExitButton_OnClick
        //  Called when the exit button on the main menu is clicked

        private void ExitButton_OnClick(Button sender)
        {
            _game.Exit();

        }

        //------------------------------------------------------------

        //------------------------------------------------------------
        //  OptionsButton_OnClick
        //  Called when the options button on the main menu is clicked

        private void OptionsButton_OnClick(Button sender)
        {
            Button fullScreenButton = (Button)_optionsMenu.UIComponents.FirstOrDefault(x => x.Id == "_fullScreenButton");
            fullScreenButton.Label.Text = $"Fullscreen: {(GameSettingsRepository.GameSettings.IsFullScreen ? "On" : "Off")}";

            Button musicVolumButton = (Button)_optionsMenu.UIComponents.FirstOrDefault(x => x.Id == "_musicOnButton");
            musicVolumButton.Label.Text = $"Music: {(GameSettingsRepository.GameSettings.MusicOn ? "On" : "Off")}";

            Button soundVolumeButton = (Button)_optionsMenu.UIComponents.FirstOrDefault(x => x.Id == "_soundOnButton");
            soundVolumeButton.Label.Text = $"Sound: {(GameSettingsRepository.GameSettings.SoundOn ? "On" : "Off")}";

            _currentMenu = _optionsMenu;
        }

        //------------------------------------------------------------

        //------------------------------------------------------------
        //  PlayModeButton_OnClick
        //  Called when the play mode button is clicked on the main menu

        private void PlayModeButton_OnClick(Button sender)
        {
            //  Check game settings for forced introduction
            if(GameSettingsRepository.GameSettings.ForceIntroduction)
            {
                if(PlayScreen.instance != null)
                {
                    PlayScreen.instance.CreateIntroduction();
                    this.Hide();
                    Show<PlayScreen>();
                }
            }
            else
            {
                //  Ask if they would like to skip the how to play
                _currentMenu = _skipHowToPlayMenu;
            }
                
        }

        //------------------------------------------------------------


        //------------------------------------------------------------
        //  BuildHowToPlayMenu
        //  Builds the How To Play Menu (aka the Skip Introduction Menu)

        private void BuildHowToPlayMenu()
        {
            BitmapFont buttonFont = CacheManager.LoadFont("fonts/sneakattack-16");
            BitmapFont titleFont = CacheManager.LoadFont("fonts/sneakattack-42");

            Size<int> menuSize = new Size<int>
            {
                Width = 1280,
                Height = (int)titleFont.MeasureString("A").Height + 10
                 + (int)((buttonFont.MeasureString("A").Height + 10) * 3) + 25

            };

            Vector2 menuPosition = new Vector2
            {
                X = 0,
                Y = (CacheManager.VirtualSize.Height / 2.0f) - (menuSize.Height / 2.0f)
            };



            _skipHowToPlayMenu = new Menu(
                id: "_skipHowToPlayMenu",
                position: menuPosition,
                size: menuSize,
                texture: CacheManager.LoadTexture("ui/background"));

            Label titleLabel = _skipHowToPlayMenu.AddLabel(
                id: "_titleLabel",
                position: new Vector2(_mainMenu.Position.X + 0, _mainMenu.Position.Y),
                size: new Size<int>(_mainMenu.Size.Width, (int)titleFont.MeasureString("A").Height + 10),
                font: titleFont,
                text: "Skip Introduction",
                alignment: TextAlignment.TopCenter,
                color: Color.Black);

            Button yesButton = _skipHowToPlayMenu.AddButton(
                                            id: "_yesButton",
                                            position: new Vector2(_mainMenu.Position.X, titleLabel.Position.Y + titleLabel.Size.Height),
                                            size: new Size<int>(_mainMenu.Size.Width, (int)buttonFont.MeasureString("A").Height + 10),
                                            texture: CacheManager.LoadTexture("ui/background"),
                                            font: buttonFont,
                                            text: "Yes",
                                            alignment: TextAlignment.Middle,
                                            fontColor: Color.Black,
                                            backgroundColor: Color.White,
                                            selectedColor: Color.LightGray);

            Button noButton = _skipHowToPlayMenu.AddButton(
                                            id: "_noButton",
                                            position: new Vector2(0, yesButton.Position.Y + yesButton.Size.Height + 5),
                                            size: new Size<int>(_mainMenu.Size.Width, (int)buttonFont.MeasureString("A").Height + 10),
                                            texture: CacheManager.LoadTexture("ui/background"),
                                            font: buttonFont,
                                            text: "No",
                                            alignment: TextAlignment.Middle,
                                            fontColor: Color.Black,
                                            backgroundColor: Color.White,
                                            selectedColor: Color.LightGray);



            yesButton.UpSelection = noButton;
            yesButton.DownSelection = noButton;


            noButton.UpSelection = yesButton;
            noButton.DownSelection = yesButton;

            yesButton.OnClick += YesButton_OnClick; ;
            noButton.OnClick += NoButton_OnClick; ;
        }

        //------------------------------------------------------------

        //------------------------------------------------------------
        //  NoButton_OnClick
        //  Called when the no button on the how to play menu is clicked

        private void NoButton_OnClick(Button sender)
        {
            if (PlayScreen.instance != null)
            {
                PlayScreen.instance.CreateIntroduction();
            }
            _currentMenu = _mainMenu;
            this.Hide();
            Show<PlayScreen>();
        }

        //------------------------------------------------------------

        //------------------------------------------------------------
        //  YesButton_OnClick
        //  Called when the yes button on the how to play menu is clicked

        private void YesButton_OnClick(Button sender)
        {
            if (PlayScreen.instance != null)
            {
                PlayScreen.instance.CreateWithoutIntroduction();
            }
            _currentMenu = _mainMenu;
            this.Hide();
            Show<PlayScreen>();
        }

        //------------------------------------------------------------

        //------------------------------------------------------------
        //  BuildOptionMenu
        //  Builds the options menu

        private void BuildOptionMenu()
        {
            BitmapFont buttonFont = CacheManager.LoadFont("fonts/sneakattack-16");
            BitmapFont titleFont = CacheManager.LoadFont("fonts/sneakattack-42");

            Size<int> menuSize = new Size<int>
            {
                Width = 1300,
                Height = (int)titleFont.MeasureString("A").Height + 10
                 + (int)((buttonFont.MeasureString("A").Height + 10) * 4) + 30

            };

            Vector2 menuPosition = new Vector2
            {
                X = 0,
                Y = (CacheManager.VirtualSize.Height / 2.0f) - (menuSize.Height / 2.0f)
            };



            _optionsMenu = new Menu(
                id: "_optionsMenu",
                position: menuPosition,
                size: menuSize,
                texture: CacheManager.LoadTexture("ui/background"));

            Label titleLabel = _optionsMenu.AddLabel(
                id: "_titleLabel",
                position: new Vector2(_optionsMenu.Position.X + 0, _optionsMenu.Position.Y),
                size: new Size<int>(_optionsMenu.Size.Width, (int)titleFont.MeasureString("A").Height + 10),
                font: titleFont,
                text: "Options",
                alignment: TextAlignment.TopCenter,
                color: Color.Black);

            Button fullScreenButton = _optionsMenu.AddButton(
                                            id: "_fullScreenButton",
                                            position: new Vector2(_optionsMenu.Position.X, titleLabel.Position.Y + titleLabel.Size.Height),
                                            size: new Size<int>(_optionsMenu.Size.Width, (int)buttonFont.MeasureString("A").Height + 10),
                                            texture: CacheManager.LoadTexture("ui/background"),
                                            font: buttonFont,
                                            text: $"Fullscreen {(GameSettingsRepository.GameSettings.IsFullScreen ? "On" : "Off")}",
                                            alignment: TextAlignment.Middle,
                                            fontColor: Color.Black,
                                            backgroundColor: Color.White,
                                            selectedColor: Color.LightGray);

            Button musicOnButton = _optionsMenu.AddButton(
                                            id: "_musicOnButton",
                                            position: new Vector2(0, fullScreenButton.Position.Y + fullScreenButton.Size.Height + 5),
                                            size: new Size<int>(_optionsMenu.Size.Width, (int)buttonFont.MeasureString("A").Height + 10),
                                            texture: CacheManager.LoadTexture("ui/background"),
                                            font: buttonFont,
                                            text: $"Music : {(GameSettingsRepository.GameSettings.MusicOn ? "On" : "Off")}",
                                            alignment: TextAlignment.Middle,
                                            fontColor: Color.Black,
                                            backgroundColor: Color.White,
                                            selectedColor: Color.LightGray);

            Button soundOnButton = _optionsMenu.AddButton(
                                            id: "_soundOnButton",
                                            position: new Vector2(0, musicOnButton.Position.Y + musicOnButton.Size.Height + 5),
                                            size: new Size<int>(_optionsMenu.Size.Width, (int)buttonFont.MeasureString("A").Height + 10),
                                            texture: CacheManager.LoadTexture("ui/background"),
                                            font: buttonFont,
                                            text: $"Sound: {(GameSettingsRepository.GameSettings.SoundOn ? "On" : "Off")}",
                                            alignment: TextAlignment.Middle,
                                            fontColor: Color.Black,
                                            backgroundColor: Color.White,
                                            selectedColor: Color.LightGray);

            Button exitOptionsButton = _optionsMenu.AddButton(
                                            id: "_exitOptionsButton",
                                            position: new Vector2(0, soundOnButton.Position.Y + soundOnButton.Size.Height + 5),
                                            size: new Size<int>(_optionsMenu.Size.Width, (int)buttonFont.MeasureString("A").Height + 10),
                                            texture: CacheManager.LoadTexture("ui/background"),
                                            font: buttonFont,
                                            text: "Exit",
                                            alignment: TextAlignment.Middle,
                                            fontColor: Color.Black,
                                            backgroundColor: Color.White,
                                            selectedColor: Color.LightGray);



            fullScreenButton.UpSelection = exitOptionsButton;
            fullScreenButton.DownSelection = musicOnButton;

            musicOnButton.UpSelection = fullScreenButton;
            musicOnButton.DownSelection = soundOnButton;

            soundOnButton.UpSelection = musicOnButton;
            soundOnButton.DownSelection = exitOptionsButton;

            exitOptionsButton.UpSelection = soundOnButton;
            exitOptionsButton.DownSelection = fullScreenButton;

            musicOnButton.OnClick += MusicOnButton_OnClick;
            soundOnButton.OnClick += SoundOnButton_OnClick;
            fullScreenButton.OnClick += FullScreenButton_OnClick;
            exitOptionsButton.OnClick += ExitOptionsButton_OnClick;

        }

        //------------------------------------------------------------

        //------------------------------------------------------------
        //  SoundOnButton_OnClick
        //  Called when the sound button is click on the options menu

        private void SoundOnButton_OnClick(Button sender)
        {
            GameSettingsRepository.GameSettings.SoundOn = !GameSettingsRepository.GameSettings.SoundOn;
            sender.Label.Text = $"Sound: {(GameSettingsRepository.GameSettings.SoundOn ? "On" : "Off")}";
        }

        //------------------------------------------------------------

        //------------------------------------------------------------
        //  MusicOnButton_OnClick
        //  Called when the music button is clicked on the options menu

        private void MusicOnButton_OnClick(Button sender)
        {
            GameSettingsRepository.GameSettings.MusicOn = !GameSettingsRepository.GameSettings.MusicOn;

            MediaPlayer.IsMuted = !GameSettingsRepository.GameSettings.MusicOn;

            sender.Label.Text = $"Music: {(GameSettingsRepository.GameSettings.MusicOn ? "On" : "Off")}";
        }

        //------------------------------------------------------------

        //------------------------------------------------------------
        //  ExitOptionsButton_OnClick
        //  Called when the exit options button is clicked on the options menu

        private void ExitOptionsButton_OnClick(Button sender)
        {
            GameSettingsRepository.SaveSettings();
            _currentMenu = _mainMenu;
            
        }

        //------------------------------------------------------------

        //------------------------------------------------------------
        //  FullScreenButton_OnClick
        //  Called when the full screen button is clicked on the options menu

        private void FullScreenButton_OnClick(Button sender)
        {
            GameSettingsRepository.GameSettings.IsFullScreen = !GameSettingsRepository.GameSettings.IsFullScreen;
            CacheManager.GameMain.graphics.IsFullScreen = GameSettingsRepository.GameSettings.IsFullScreen;
            sender.Label.Text = $"Fullscreen: {(GameSettingsRepository.GameSettings.IsFullScreen ? "On" : "Off")}";
            CacheManager.GameMain.graphics.ApplyChanges();
        }

        //------------------------------------------------------------
        
        //------------------------------------------------------------
        //  BuildCreditsMenu
        //  Builds the credits menu
        private void BuildCreditsMenu()
        {
            BitmapFont labelFont = CacheManager.LoadFont("fonts/sneakattack-16");
            BitmapFont titleFont = CacheManager.LoadFont("fonts/sneakattack-42");

            Size<int> menuSize = new Size<int>
            {
                Width = 1280,
                Height = (int)titleFont.MeasureString("A").Height + 10
                 + (int)((labelFont.MeasureString("A").Height + 10) * 8) + 10

            };

            Vector2 menuPosition = new Vector2
            {
                X = 0,
                Y = (CacheManager.VirtualSize.Height / 2.0f) - (menuSize.Height / 2.0f)
            };



            _creditsMenu = new Menu(
                id: "_creditsMenu",
                position: menuPosition,
                size: menuSize,
                texture: CacheManager.LoadTexture("ui/background"));

            Label titleLabel = _creditsMenu.AddLabel(
                id: "_titleLabel",
                position: new Vector2(_creditsMenu.Position.X + 0, _creditsMenu.Position.Y),
                size: new Size<int>(_creditsMenu.Size.Width, (int)titleFont.MeasureString("A").Height + 10),
                font: titleFont,
                text: "Credits",
                alignment: TextAlignment.TopCenter,
                color: Color.Black);

            Label gameDesignLabel = _creditsMenu.AddLabel(
                id: "_gameDesignLabel",
                position: new Vector2(0, titleLabel.Position.Y + titleLabel.Size.Height + 5),
                size: new Size<int>(_creditsMenu.Size.Width, (int)labelFont.MeasureString("A").Height + 10),
                font: labelFont,
                text: "Game Design, Programming, and Sound Effects",
                alignment: TextAlignment.TopCenter,
                color: Color.Black);

            Label gameDesignCreditLabel = _creditsMenu.AddLabel(
                id: "_gameDesignCreditLabel",
                position: new Vector2(0, gameDesignLabel.Position.Y + gameDesignLabel.Size.Height),
                size: new Size<int>(_creditsMenu.Size.Width, (int)labelFont.MeasureString("A").Height + 10),
                font: labelFont,
                text: "Christopher Whitley (ManBeardGames)",
                alignment: TextAlignment.TopCenter,
                color: Color.Black);

            Label musicLabel = _creditsMenu.AddLabel(
                id: "_musicLabel",
                position: new Vector2(0, gameDesignCreditLabel.Position.Y + gameDesignCreditLabel.Size.Height + 5),
                size: new Size<int>(_creditsMenu.Size.Width, (int)labelFont.MeasureString("A").Height + 10),
                font: labelFont,
                text: "Music:",
                alignment: TextAlignment.TopCenter,
                color: Color.Black);


            Label musicTitleCreditLabel = _creditsMenu.AddLabel(
                id: "_musicCreditLabel",
                position: new Vector2(0, musicLabel.Position.Y + musicLabel.Size.Height),
                size: new Size<int>(_creditsMenu.Size.Width, (int)labelFont.MeasureString("A").Height + 10),
                font: labelFont,
                text: "\"Water Lily\" Kevin MacLeod (incompetech.com)",
                alignment: TextAlignment.TopCenter,
                color: Color.Black);

            Label musicLicenseCreditLabel = _creditsMenu.AddLabel(
                id: "_musicCreditLabel",
                position: new Vector2(0, musicTitleCreditLabel.Position.Y + musicTitleCreditLabel.Size.Height),
                size: new Size<int>(_creditsMenu.Size.Width, (int)labelFont.MeasureString("A").Height + 10),
                font: labelFont,
                text: "Licensed under Creative Commons: By Attribution 3.0",
                alignment: TextAlignment.TopCenter,
                color: Color.Black);

            Label musicLinkCreditLabel = _creditsMenu.AddLabel(
                id: "_musicCreditLabel",
                position: new Vector2(0, musicLicenseCreditLabel.Position.Y + musicLicenseCreditLabel.Size.Height),
                size: new Size<int>(_creditsMenu.Size.Width, (int)labelFont.MeasureString("A").Height + 10),
                font: labelFont,
                text: @"http://creativecommons.org/licenses/by/3.0/",
                alignment: TextAlignment.TopCenter,
                color: Color.Black);


            Button exitCreditsButton = _creditsMenu.AddButton(
                                            id: "_exitButton",
                                            position: new Vector2(0, musicLinkCreditLabel.Position.Y + musicLinkCreditLabel.Size.Height + 5),
                                            size: new Size<int>(_creditsMenu.Size.Width, (int)labelFont.MeasureString("A").Height + 10),
                                            texture: CacheManager.LoadTexture("ui/background"),
                                            font: labelFont,
                                            text: "Exit",
                                            alignment: TextAlignment.Middle,
                                            fontColor: Color.Black,
                                            backgroundColor: Color.White,
                                            selectedColor: Color.LightGray);


            exitCreditsButton.OnClick += ExitCreditsButton_OnClick;
        }

        //------------------------------------------------------------

        //------------------------------------------------------------
        //  ExitCreditsButton_OnClick
        //  Called when the exit credits button is clicked

        private void ExitCreditsButton_OnClick(Button sender)
        {
            GameSettingsRepository.SaveSettings();
            _currentMenu = _mainMenu;

        }

        //------------------------------------------------------------

        //------------------------------------------------------------
        //  SpawnEnemy
        //  Spawns actors to the screen

        private Actor SpawnActors(bool isNew = true)
        {
            float minRadius = 0.15f;
            float maxRadius = 1.0f;

            float smallerThanCount = _actors.Count(x => x.circle.Radius <= 0.40f);

            float ratio = smallerThanCount / _actors.Count();

            float scale = 0.0f;

            //  If the smaller than ratio is less than half
            if (ratio < 0.5f)
            {
                scale = MathHelper.Lerp(minRadius, 0.40f, (float)rand.NextDouble());
            }
            else
            {
                scale = MathHelper.Lerp(minRadius, maxRadius, (float)rand.NextDouble());
            }

            Actor enemy = new Actor(scale, Vector2.Zero);

            Vector2 direction = Directions.None;

            //  is the player spawning from the top or bottom OR the left or right
            var xEdge = rand.NextDouble();
            var yEdge = rand.NextDouble();
            bool spawnXEdge = xEdge >= yEdge;


            //  Then choose if it's one of the two in the group (either top OR bottom, OR, left OR right)
            if (spawnXEdge)
            {
                var spawnLeft = rand.NextDouble();
                var spawnRight = rand.NextDouble();
                bool isLeftSide = spawnLeft >= spawnRight;
                if (isLeftSide)
                {
                    enemy.circle.Center.X = -enemy.circle.Radius;
                    
                }
                else
                {
                    enemy.circle.Center.X = CacheManager.WorldSize.Width + enemy.circle.Radius;
                    
                }
                enemy.circle.Center.Y = MathHelper.Clamp((float)rand.NextDouble() * CacheManager.WorldSize.Height, scale, CacheManager.WorldSize.Height - scale);
                
            }
            else
            {
                var spawnTop = rand.NextDouble();
                var spawnBottom = rand.NextDouble();
                bool isTop = spawnTop >= spawnBottom;
                if (isTop)
                {
                    enemy.circle.Center.Y = -enemy.circle.Radius;
                    
                }
                else
                {
                    enemy.circle.Center.Y = CacheManager.WorldSize.Height + enemy.circle.Radius;
                    
                }
                enemy.circle.Center.X = MathHelper.Clamp((float)rand.NextDouble() * CacheManager.WorldSize.Width, scale, CacheManager.WorldSize.Width - scale);
                

            }


            direction.X = enemy.circle.Center.X <= 0 ? MathHelper.Lerp(0.1f, 1.0f, (float)rand.NextDouble()) : MathHelper.Lerp(-1.0f, -0.1f, (float)rand.NextDouble());
            direction.Y = enemy.circle.Center.Y <= 0 ? MathHelper.Lerp(0.1f, 1.0f, (float)rand.NextDouble()) : MathHelper.Lerp(-1.0f, -0.1f, (float)rand.NextDouble());
            enemy.Direction = direction;

            //  Once side is chosen, calculate velocity
            enemy.Velocity = MathHelper.Lerp(50.0f, 100, (float)rand.NextDouble());
            if (enemy.Velocity == 0)
            {
                enemy.Velocity = 1.0f;
            }
            enemy.IsPlayer = false;

            enemy.IsPositive = rand.NextDouble() >= 0.5;



            if (isNew)
            {
                _actors.Add(enemy);
            }
            return enemy;


        }

        //------------------------------------------------------------

        //-----------------------------------------------------------
        //  UpdateActors
        //  Updates actors and checks for respawns

        private void UpdateActors(GameTime gameTime)
        {
            for (int i = 0; i < _actors.Count; i++)
            {
                _actors[i].Update(gameTime);
                if (_actors[i].ShouldRespawn)
                {
                    _actors[i] = SpawnActors(false);
                    _actors[i].ShouldRespawn = false;
                }

            }
        }

        //-----------------------------------------------------------









    }
}

