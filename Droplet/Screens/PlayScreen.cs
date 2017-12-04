//  NOTE:
//  I am sorry for the mess of the code in this one.  A lot is last minute fixes and rushed changes

using Droplet.Extensions;
using Droplet.Managers;
using Droplet.Models;
using Droplet.Objects;
using Droplet.Repositories;
using Droplet.UIFramework;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.BitmapFonts;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Droplet.Screens
{
    public class PlayScreen : ScreenBase
    {
        public static PlayScreen instance;
        private readonly Game _game;
        private Vector2 _worldPosition;


        private Actor _player;
        private Actor _introductionActor;

        private GameState _gameState;

        private List<Actor> _actors;


        private List<UIComponentBase> _hud;
        private ProgressBar _progressBar;
        private KeyboardState _prevKeyboardState;
        private MouseState _prevMouseState;

        private int _level = 1;
        private Label _levelLabel;

        private int _score = int.MaxValue;
        private Label _scoreLabel;

        private Label _helperLabel;

        private Menu _pauseMenu;
        private Menu _gameOverMenu;
        private MessageBox _messageBox;



        Random rand;

        private List<string> _introductionLines;
        private IntroductionTriggers _introductionTrigger;
        private int _introductionIndex;
        private bool _introductionPlayerClicked;
        private float _introductionTimer;
        private float _colorSwitchCount;






        //-----------------------------------------------------------
        //  Constructor
        public PlayScreen(IServiceProvider serviceProvider, Game game) : base(serviceProvider)
        {
            _game = game;
            _actors = new List<Actor>();
            _hud = new List<UIComponentBase>();
            rand = new Random();
            instance = this;

        }
        //-----------------------------------------------------------

        //-----------------------------------------------------------
        //  Initialize
        //  Initializes the PlayScreen

        public override void Initialize()
        {
            base.Initialize();

        }

        //-----------------------------------------------------------

        //-----------------------------------------------------------
        //  Dispose
        //  Handles gracefully disposing of the object 

        public override void Dispose()
        {
            base.Dispose();
        }

        //-----------------------------------------------------------

        //-----------------------------------------------------------
        //  LoadContent
        //  Load content here

        public override void LoadContent()
        {
            base.LoadContent();

            //  Create Player
            _player = new Actor(0.2f, new Vector2(CacheManager.WorldSize.Width / 2.0f, CacheManager.WorldSize.Height / 2.0f));
            _player.IsPlayer = true;

            //  Generate Menus
            BitmapFont font = CacheManager.LoadFont("fonts/sneakattack-16");
            CreatePauseMenu();
            CreateGameOverMenu();
            BuildHud();

            _helperLabel = new Label(
                id: "_waitingLabel",
                position: new Vector2(0, (CacheManager.WorldSize.Height / 1.5f) /*- (_font.MeasureString("A").Height / 2.0f)*/),
                size: new Size<int>(1280, (int)font.MeasureString("A").Height),
                texture: null,
                container: null,
                font: font,
                text: "",
                alignment: TextAlignment.Middle,
                color: Color.White);
        }

        //-----------------------------------------------------------

        //-----------------------------------------------------------
        //  CreatePauseMenu
        //  Creates the pause menu

        private void CreatePauseMenu()
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



            _pauseMenu = new Menu(
                id: "_pauseMenu",
                position: menuPosition,
                size: menuSize,
                texture: CacheManager.LoadTexture("ui/background"));

            Label titleLabel = _pauseMenu.AddLabel(
                id: "_titleLabel",
                position: new Vector2(_pauseMenu.Position.X + 0, _pauseMenu.Position.Y),
                size: new Size<int>(_pauseMenu.Size.Width, (int)titleFont.MeasureString("A").Height + 10),
                font: titleFont,
                text: "Game Paused",
                alignment: TextAlignment.TopCenter,
                color: Color.Black);

            Button resumeButton = _pauseMenu.AddButton(
                                            id: "_resumeButton",
                                            position: new Vector2(_pauseMenu.Position.X, titleLabel.Position.Y + titleLabel.Size.Height),
                                            size: new Size<int>(_pauseMenu.Size.Width, (int)buttonFont.MeasureString("A").Height + 10),
                                            texture: CacheManager.LoadTexture("ui/background"),
                                            font: buttonFont,
                                            text: "Resume",
                                            alignment: TextAlignment.Middle,
                                            fontColor: Color.Black,
                                            backgroundColor: Color.White,
                                            selectedColor: Color.LightGray);


            Button retryButton = _pauseMenu.AddButton(
                                            id: "_retryButton",
                                            position: new Vector2(0, resumeButton.Position.Y + resumeButton.Size.Height + 5),
                                            size: new Size<int>(_pauseMenu.Size.Width, (int)buttonFont.MeasureString("A").Height + 10),
                                            texture: CacheManager.LoadTexture("ui/background"),
                                            font: buttonFont,
                                            text: "Retry",
                                            alignment: TextAlignment.Middle,
                                            fontColor: Color.Black,
                                            backgroundColor: Color.White,
                                            selectedColor: Color.LightGray);

            Button returnToTitleButton = _pauseMenu.AddButton(
                                            id: "_returnToTitleButton",
                                            position: new Vector2(0, retryButton.Position.Y + retryButton.Size.Height + 5),
                                            size: new Size<int>(_pauseMenu.Size.Width, (int)buttonFont.MeasureString("A").Height + 10),
                                            texture: CacheManager.LoadTexture("ui/background"),
                                            font: buttonFont,
                                            text: "Return To Title",
                                            alignment: TextAlignment.Middle,
                                            fontColor: Color.Black,
                                            backgroundColor: Color.White,
                                            selectedColor: Color.LightGray);




            resumeButton.UpSelection = returnToTitleButton;
            resumeButton.DownSelection = retryButton;

            retryButton.UpSelection = resumeButton;
            retryButton.DownSelection = returnToTitleButton;

            returnToTitleButton.UpSelection = retryButton;
            returnToTitleButton.DownSelection = resumeButton;

            retryButton.OnClick += RetryButton_OnClick;
            resumeButton.OnClick += ResumeButton_OnClick; ;
            returnToTitleButton.OnClick += ReturnToTitleButton_OnClick;
        }

        //-----------------------------------------------------------

        //-----------------------------------------------------------
        //  CreateGameOverMenu
        //  Creates the game over menu

        private void CreateGameOverMenu()
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



            _gameOverMenu = new Menu(
                id: "_gameOverMenu",
                position: menuPosition,
                size: menuSize,
                texture: CacheManager.LoadTexture("ui/background"));

            Label titleLabel = _gameOverMenu.AddLabel(
                id: "_titleLabel",
                position: new Vector2(_gameOverMenu.Position.X + 0, _gameOverMenu.Position.Y),
                size: new Size<int>(_gameOverMenu.Size.Width, (int)titleFont.MeasureString("A").Height + 10),
                font: titleFont,
                text: "Game Over",
                alignment: TextAlignment.TopCenter,
                color: Color.Black);



            Button retryButton = _gameOverMenu.AddButton(
                                            id: "_retryButton",
                                            position: new Vector2(_gameOverMenu.Position.X, titleLabel.Position.Y + titleLabel.Size.Height),
                                            size: new Size<int>(_gameOverMenu.Size.Width, (int)buttonFont.MeasureString("A").Height + 10),
                                            texture: CacheManager.LoadTexture("ui/background"),
                                            font: buttonFont,
                                            text: "Retry",
                                            alignment: TextAlignment.Middle,
                                            fontColor: Color.Black,
                                            backgroundColor: Color.White,
                                            selectedColor: Color.LightGray);

            Button returnToTitleButton = _gameOverMenu.AddButton(
                                            id: "_returnToTitleButton",
                                            position: new Vector2(0, retryButton.Position.Y + retryButton.Size.Height + 5),
                                            size: new Size<int>(_gameOverMenu.Size.Width, (int)buttonFont.MeasureString("A").Height + 10),
                                            texture: CacheManager.LoadTexture("ui/background"),
                                            font: buttonFont,
                                            text: "Return To Title",
                                            alignment: TextAlignment.Middle,
                                            fontColor: Color.Black,
                                            backgroundColor: Color.White,
                                            selectedColor: Color.LightGray);





            retryButton.UpSelection = returnToTitleButton;
            retryButton.DownSelection = returnToTitleButton;

            returnToTitleButton.UpSelection = retryButton;
            returnToTitleButton.DownSelection = retryButton;

            retryButton.OnClick += RetryButton_OnClick;
            returnToTitleButton.OnClick += ReturnToTitleButton_OnClick;
        }

        //-----------------------------------------------------------

        //------------------------------------------------------------
        //  ReturnToTitleButton_OnClick
        //  Called when the return to title button is clicked
        private void ReturnToTitleButton_OnClick(Button sender)
        {
            ResetGame();
            this.Hide();
            Show<TitleScreen>();
        }

        //------------------------------------------------------------

        //------------------------------------------------------------
        //  RetryButton_OnClick
        //  Called when the retry button is clicked

        private void RetryButton_OnClick(Button sender)
        {
            //  Rebuild game
            ResetGame();
            CreateWithoutIntroduction();
        }

        //------------------------------------------------------------
        
        //------------------------------------------------------------
        //  ResumeButton_OnClick
        //  Callled when the resume button is clicked

        private void ResumeButton_OnClick(Button sender)
        {
            _gameState = GameState.Playing;
        }

        //-----------------------------------------------------------

        //-----------------------------------------------------------
        //  Reset Game
        //  Resets the game to a blank state
        private void ResetGame()
        {
            _player = null;
            _introductionActor = null;
            _actors = null;
            _score = 0;
            _level = 1;
            _progressBar.UpdatePercentage(0, 100);
            _introductionTrigger = IntroductionTriggers.None;
            _introductionTimer = 0.0f;
            _introductionIndex = 0;
            _colorSwitchCount = 0;

            _scoreLabel.UpdateText($"Score: {_score:D6}");
            _levelLabel.UpdateText($"Level: {_level}");
        }
        //-----------------------------------------------------------

        //-----------------------------------------------------------
        //  BuildHud
        //  Builds the hud used for the gameplay
        private void BuildHud()
        {
            _progressBar = new ProgressBar(
                id: "progressBard",
                position: new Vector2(0, CacheManager.VirtualSize.Height - 15),
                size: new Size<int>(CacheManager.VirtualSize.Width, 15),
                texture: CacheManager.LoadTexture("ui/progressbar"),
                container: null);
            _progressBar.UpdatePercentage(0.0f, _player.NextLevel);

            _progressBar.OnFullPercentage += ProgressBar_OnFullPercentage;

            BitmapFont labelFont = CacheManager.LoadFont("fonts/sneakattack-16");


            _levelLabel = new Label(
                id: "levelLabel",
                size: new Size<int>(1280, (int)labelFont.MeasureString("A").Height),
                position: new Vector2(0, _progressBar.Position.Y - labelFont.MeasureString("A").Height),
                container: null,
                texture: null,
                font: labelFont,
                text: $"Level: {_level}",
                alignment: TextAlignment.MiddleLeft,
                color: Color.White);

            _scoreLabel = new Label(
                  id: "levelLabel",
                  size: new Size<int>(1280, (int)labelFont.MeasureString("A").Height),
                  position: new Vector2(0, _progressBar.Position.Y - labelFont.MeasureString("A").Height),
                  container: null,
                  texture: null,
                  font: labelFont,
                  text: "Score: 000000",
                  alignment: TextAlignment.MiddleRight,
                  color: Color.White);




            _hud.Add(_progressBar);
            _hud.Add(_levelLabel);
            _hud.Add(_scoreLabel);
        }

        //------------------------------------------------------------
        //  ProgressBar_OnFullPercentage
        //  Called when the progress bar reaches 100%

        private void ProgressBar_OnFullPercentage(ProgressBar sender)
        {
            //  Increment the level count
            _level++;
            _levelLabel.Text = $"Level: {_level}";

            //  Determine next level needed for player
            _player.NextLevel = _player.NextLevel + _player.circle.Radius;
            _player.AmountConsumed = 0.0f;

            //  Reupdate progressbar
            _progressBar.UpdatePercentage(0.0f, _player.NextLevel);

            SpawnEnemy();
            SpawnEnemy();
        }

        //-----------------------------------------------------------

        //-----------------------------------------------------------
        //  SpawnEnemy
        //  Spawns an enemy to the game world

        private Actor SpawnEnemy(bool isNew = true)
        {
            float minRadius = 0.15f;
            float maxRadius = (_player.circle.Radius / 64.0f) + 0.25f;

            float smallerThanCount = _actors.Count(x => x.circle.Radius <= _player.circle.Radius);

            float ratio = smallerThanCount / _actors.Count();

            float scale = 0.0f;

            //  If the smaller than ratio is less than half
            if (ratio < 0.5f)
            {
                scale = MathHelper.Lerp(minRadius, (_player.circle.Radius / 64.0f), (float)rand.NextDouble());
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

        //------------------------------------------------------------
        //  SpawnIntroductionEnemy
        //  Spawns the introduction enemy to the screen

        private Actor SpawnIntroductionEnemey()
        {
            float scale = 0.15f;
            Actor enemy = new Actor(scale, Vector2.Zero);

            enemy.Direction = Directions.Right;

            enemy.circle.Center.X = -enemy.circle.Radius;
            enemy.circle.Center.Y = CacheManager.WorldSize.Height / 2.0f;

            enemy.Velocity = 50.0f;
            enemy.IsPlayer = false;
            enemy.IsIntroductionActor = true;

            enemy.IsPositive = true;
            return enemy;
        }

        //-----------------------------------------------------------

        //-----------------------------------------------------------
        //  UnloadContent
        //  Unload content here
        public override void UnloadContent()
        {
            base.UnloadContent();
        }
        //-----------------------------------------------------------

        //-----------------------------------------------------------
        //  Update
        //  Updates the screen
        public override void Update(GameTime gameTime)
        {
            KeyboardState keyboardState = Keyboard.GetState();
            MouseState mouseState = Mouse.GetState();

            base.Update(gameTime);

            if (_gameState == GameState.Playing)
            {
                if (keyboardState.IsKeyDown(Keys.Escape) && _prevKeyboardState.IsKeyUp(Keys.Escape))
                {
                    //  Pause the game
                    _gameState = GameState.Paused;
                }
                else
                {
                    _player.Update(gameTime);

                    UpdateActors(gameTime);

                    CheckForCollisions(gameTime);
                }
            }
            else if (_gameState == GameState.Paused)
            {
                if (keyboardState.IsKeyDown(Keys.Escape) && _prevKeyboardState.IsKeyUp(Keys.Escape))
                {
                    //  Unpause Game
                    _gameState = GameState.Playing;
                }
                else
                {
                    _pauseMenu.Update(gameTime);
                }
            }
            else if (_gameState == GameState.GameOver)
            {
                _gameOverMenu.Update(gameTime);
            }
            else if (_gameState == GameState.Waiting)
            {
                CheckForMouseClick(mouseState);
            }
            else if (_gameState == GameState.Introduction)
            {
                switch (_introductionTrigger)
                {
                    case IntroductionTriggers.ClickPlayer:
                        if (_introductionPlayerClicked)
                        {
                            _introductionTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;
                            _helperLabel.UpdateText($"Click player circle and move mouse around ({(5.0f - _introductionTimer):00.0})");
                            if (_introductionTimer >= 5.0f)
                            {
                                _introductionTrigger = IntroductionTriggers.None;
                            }
                            _player.Update(gameTime);
                        }
                        else
                        {
                            CheckForMouseClick(mouseState);
                        }
                        break;
                    case IntroductionTriggers.ConsumeFood:
                        _player.Update(gameTime);
                        CheckForCollisions(gameTime);
                        _introductionActor.Update(gameTime);
                        break;
                    case IntroductionTriggers.ColorSwitch:
                        if (_colorSwitchCount >= 2)
                        {
                            _introductionTrigger = IntroductionTriggers.None;
                        }
                        if (_player.DidScolorSwitch) _colorSwitchCount++;
                        _player.Update(gameTime);
                        break;
                    case IntroductionTriggers.Start:
                        _gameState = GameState.Waiting;
                        break;
                    default:
                        _helperLabel.UpdateText("");
                        _messageBox.Update(gameTime);
                        break;
                }
            }


            _prevKeyboardState = keyboardState;
            _prevMouseState = mouseState;


        }
        //-----------------------------------------------------------

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
                    _actors[i] = SpawnEnemy(false);
                    _actors[i].ShouldRespawn = false;
                }

            }
        }

        //-----------------------------------------------------------

        //-----------------------------------------------------------
        //  CheckForCollisions
        //  Checks for colliions between the player and the actors

        private void CheckForCollisions(GameTime gameTime)
        {
            for (int i = 0; i < _actors.Count(); i++)
            {
                //  Check player collision first as priority
                if (_player.Intersects(_actors[i]))
                {
                    //  Check if player or actor is bigger
                    if (_player.circle.Radius >= _actors[i].circle.Radius)
                    {
                        //  Absorb
                        _actors[i].ShouldRespawn = true;
                        //  Check polarity
                        if (_player.IsPositive == _actors[i].IsPositive)
                        {
                            //  Play sound effect
                            if (GameSettingsRepository.GameSettings.SoundOn)
                            {
                                CacheManager.IncreaseFX.Play(GameSettingsRepository.GameSettings.SoundVolume, 0.0f, 0.0f);
                            }

                            //  Increase player size
                            _player.circle.Radius += _actors[i].circle.Radius / 64.0f;
                            _player.AmountConsumed += _actors[i].circle.Radius;
                            _player.Velocity -= 0.01f;

                            //  Increase Score
                            _score += (int)Math.Ceiling(_actors[i].circle.Radius);

                            //  Increase progress
                            _progressBar.UpdatePercentage(_player.AmountConsumed, _player.NextLevel);
                        }
                        else
                        {
                            //  Play sound effect
                            if (GameSettingsRepository.GameSettings.SoundOn)
                            {
                                CacheManager.DecreaseFx.Play(GameSettingsRepository.GameSettings.SoundVolume, 0.0f, 0.0f);
                            }

                            //  Decrease progress
                            _player.circle.Radius -= _actors[i].circle.Radius / 64.0f;
                            _player.AmountConsumed += _actors[i].circle.Radius;
                            _player.Velocity += 0.01f;
                            _score -= (int)Math.Ceiling(_actors[i].circle.Radius);

                            //  Increase progress
                            _progressBar.UpdatePercentage(_player.AmountConsumed, _player.NextLevel);
                        }
                    }
                    else
                    {
                        _gameState = GameState.GameOver;
                    }
                }

                //  Check actor to actor collisions
                for(int j = 0; j < _actors.Count(); j++)
                {
                    if(_actors[i].Intersects(_actors[j]))
                    {
                        if(_actors[i].circle.Radius > _actors[j].circle.Radius)
                        {
                            //  This means actor[i] eats actor[j]
                            _actors[j].ShouldRespawn = true;
                        }
                    }
                }
            }




            if (_introductionTrigger == IntroductionTriggers.ConsumeFood)
            {
                if (_player.Intersects(_introductionActor))
                {
                    //  Play sound effect
                    if (GameSettingsRepository.GameSettings.SoundOn)
                    {
                        CacheManager.IncreaseFX.Play(GameSettingsRepository.GameSettings.SoundVolume, 0.0f, 0.0f);
                    }


                    //  Increase player size
                    _player.circle.Radius += _introductionActor.circle.Radius / 64.0f;
                    _player.AmountConsumed += _introductionActor.circle.Radius;
                    _player.Velocity -= 0.01f;

                    //  Increase Score
                    _score += (int)Math.Ceiling(_introductionActor.circle.Radius);

                    //  Increase progress
                    _progressBar.UpdatePercentage(_player.AmountConsumed, _player.NextLevel);

                    _helperLabel.UpdateText("");
                    _introductionTrigger = IntroductionTriggers.None;
                }

            }

            _score = MathHelper.Clamp(_score, 0, int.MaxValue);
            _scoreLabel.UpdateText($"Score: {_score:D6}");
        }
        //-----------------------------------------------------------


        //-----------------------------------------------------------
        //  Draw
        //  Draws the screen
        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);

            Matrix transformMatrix = CacheManager.Camera.GetViewMatrix(Vector2.Zero);

            if (_gameState != GameState.None)
            {
                _player.Draw(gameTime);
                foreach (Actor actor in _actors)
                {
                    actor.Draw(gameTime);
                }
                foreach (UIComponentBase uiComponnet in _hud)
                {
                    uiComponnet.Draw(gameTime, _spriteBatch);
                }




                if (_gameState == GameState.Introduction)
                {
                    if (_introductionTrigger == IntroductionTriggers.None)
                    {
                        _messageBox.Draw(gameTime, _spriteBatch);
                    }
                    else if (_introductionTrigger == IntroductionTriggers.ConsumeFood)
                    {
                        if (_introductionActor != null)
                        {
                            _introductionActor.Draw(gameTime);
                        }
                    }

                }

                //if (_gameState == GameState.Waiting)
                //{
                    _helperLabel.Draw(gameTime, _spriteBatch);
                //}

                _spriteBatch.Begin(transformMatrix: transformMatrix);
                {

                    if (_gameState == GameState.Paused)
                    {
                        _pauseMenu.Draw(gameTime, _spriteBatch);
                    }
                    else if (_gameState == GameState.GameOver)
                    {
                        _gameOverMenu.Draw(gameTime, _spriteBatch);
                    }
                }
                _spriteBatch.End();
            }
        }
        //-----------------------------------------------------------


        //-----------------------------------------------------------
        //  CreateIntroduction
        //  Creates the introduction sequence

        public void CreateIntroduction()
        {
            //  Create the player
            ResetGame();
            _player = new Actor(0.2f, new Vector2(CacheManager.WorldSize.Width / 2.0f, CacheManager.WorldSize.Height / 2.0f));
            _player.IsPlayer = true;
            _player.IsPositive = true;

            _actors = new List<Actor>();



            _introductionTrigger = IntroductionTriggers.None;
            _introductionIndex = -1;
            _introductionPlayerClicked = false;
            _introductionTimer = 0.0f;

            _introductionLines = new List<string>();
            /*0*/
            _introductionLines.Add("This is you. This tiny, unimportant, small droplet in an world of other tiny, unimportant small droplets.");
            /*1*/
            _introductionLines.Add("No need to feel sad about that. Everyone starts out this way.");

            /*2: Trigger clicking the player*/
            _introductionLines.Add("For now let’s get you moving.  No need to sit an sulk about your existence.  Do me a favor and click yourself with the mouse, then move the mouse around the screen a bit.");

            /*3: Trigger consumeing*/
            _introductionLines.Add("Now that you're up and moving around, how about eating some food.  Look, I see something coming now that you can eat. Why don't you move over to it and try consuming it.");

            /*4*/
            _introductionLines.Add("Did you notice that?  The progress for your level increased.  You can see this at the bottom of the screen. The bar indicates how much further to go till the next level.");

            /*5*/
            _introductionLines.Add("The higher your level gets, however, the more life throws at you.  Life is always trying to bring you down.");

            /*6*/
            _introductionLines.Add("You can only consume those smaller, weaker, than you.  Don't let your ego get to big though, there will always be others bigger and better than you trying to take you down.");


            /*7: Trigger color switching */
            _introductionLines.Add("Did that hurt your feelings?  Well, how about this. Try left and right clicking with your mouse (not at the same time though).");


            /*8*/
            _introductionLines.Add("Don't you just feel special now? You have the ability to adapt to your surroundings.");

            /*9*/
            _introductionLines.Add("If you consume someone that is the same color as you, it will increase your size. However, consuming one of the opposite color will cause you to shrink.");

            /*10*/
            _introductionLines.Add("That's it. That's life. You have to make a way for yourself.  Stand on the shoulders of those smaller and weaker in order to challenge those bigger and better.  Good luck.");


            Size<int> boxSize = new Size<int>((int)(CacheManager.VirtualSize.Width / 1.25f), (int)(CacheManager.VirtualSize.Height / 2.5f));
            Vector2 boxPosition = new Vector2
            {
                X = (CacheManager.VirtualSize.Width / 2.0f) - (boxSize.Width / 2.0f),
                Y = CacheManager.VirtualSize.Height - _progressBar.Size.Height - (boxSize.Height)
            };

            _messageBox = new MessageBox(
                id: "_messageBox",
                position: boxPosition,
                size: boxSize,
                texture: CacheManager.LoadTexture("ui/messagebox"),
                text: _introductionLines[0],
                padding: 5.0f,
                font: CacheManager.LoadFont("fonts/sneakattack-16"),
                container: null);


            _messageBox.OnClick += MessageBox_OnClick;

            _gameState = GameState.Introduction;
        }

        //------------------------------------------------------------
        //  MessageBox_OnClick
        //  Called when the message box detects an on click event

        private void MessageBox_OnClick(MessageBox sender)
        {
            //  Increment index
            _introductionIndex++;

            //  Check if index exists
            if (_introductionIndex < _introductionLines.Count)
            {
                _messageBox.ChangeText(_introductionLines[_introductionIndex]);
            }


            //  Check for trigger switch
            
            switch (_introductionIndex)
            {
                case 3:
                    _helperLabel.UpdateText("Click player circle and move mouse around");
                    _introductionTrigger = IntroductionTriggers.ClickPlayer;
                    break;
                case 4:
                    _helperLabel.UpdateText("Consume to other circle by running into it");
                    _introductionActor = SpawnIntroductionEnemey();
                    _introductionTrigger = IntroductionTriggers.ConsumeFood;
                    break;
                case 8:
                    _helperLabel.UpdateText("Use left and right click to change colors");
                    _colorSwitchCount = 0;
                    _introductionTrigger = IntroductionTriggers.ColorSwitch;
                    break;
                case 11:
                    CreateWithoutIntroduction();
                    break;
                default:
                    _helperLabel.UpdateText("");
                    break;
            }
        }

        //-----------------------------------------------------------

        //-----------------------------------------------------------
        //  CreateWithoutIntroduction
        //  Creates the game without the introduction sequence

        public void CreateWithoutIntroduction()
        {
            if(GameSettingsRepository.GameSettings.ForceIntroduction)
            {
                GameSettingsRepository.GameSettings.ForceIntroduction = false;
                GameSettingsRepository.SaveSettings();
            }

            _gameState = GameState.Waiting;

            //  Create the player
            _player = new Actor(0.2f, new Vector2(CacheManager.WorldSize.Width / 2.0f, CacheManager.WorldSize.Height / 2.0f));
            _player.IsPlayer = true;
            _player.IsPositive = true;

            _actors = new List<Actor>();

            //  Spawn initial actors
            for (int i = 0; i < 10; i++)
            {
                SpawnEnemy();
            }

            //  Zero out score
            _score = 0;
            _scoreLabel.UpdateText($"Score: {_score:D6}");

            //  Set level to 1
            _level = 1;
            _levelLabel.UpdateText($"Level: {_level}");

            _helperLabel.UpdateText("Click player circle to being");





        }

        //-----------------------------------------------------------

        //-----------------------------------------------------------
        //  CheckForMouseClick
        //  Checks for the player to click the player circle with the mouse

        private void CheckForMouseClick(MouseState mouseState)
        {
            //  Convert the mouse coordinate to the world coordinate
            _worldPosition = CacheManager.Camera.ScreenToWorld(Mouse.GetState().X, Mouse.GetState().Y);
            if (_player.circle.Contains(_worldPosition))
            {
                if (mouseState.LeftButton == ButtonState.Pressed && _prevMouseState.LeftButton == ButtonState.Released)
                {
                    if (_gameState == GameState.Introduction)
                    {
                        _introductionPlayerClicked = true;
                    }
                    else
                    {
                        _helperLabel.UpdateText("");
                        _gameState = GameState.Playing;
                    }
                }
            }
        }
        //-----------------------------------------------------------




    }
}
