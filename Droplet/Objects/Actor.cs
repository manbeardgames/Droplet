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
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;
using System;

namespace Droplet.Objects
{
    public class Actor
    {        
        private float _scale;
        public float Scale { get => this._scale; set => this._scale = value; }

        private Texture2D _texture;
        public Texture2D Texture { get => this._texture; set => this._texture = value; }

        private Texture2D _switchTexture;
        public Texture2D SwitchTexture { get => this._switchTexture; set => this._switchTexture = value; }


        private bool _isPlayer;
        public bool IsPlayer { get => this._isPlayer; set => this._isPlayer = value; }

        private bool _isIntroductionActor;
        public bool IsIntroductionActor { get => this._isIntroductionActor; set => this._isIntroductionActor = value; }

        private float _velocity;
        public float Velocity { get => this._velocity; set => this._velocity = value; }


        private Vector2 _direction;
        public Vector2 Direction { get => this._direction; set => this._direction = value; }

        private float _speed;
        public float Speed { get => this._speed; set => this._speed = value; }

        private bool _isPositive;
        public bool IsPositive { get => this._isPositive; set => this._isPositive = value; }

        private bool _shouldRespawn;
        public bool ShouldRespawn { get => this._shouldRespawn; set => this._shouldRespawn = value; }

        private float _nextLevel;
        public float NextLevel { get => this._nextLevel; set => this._nextLevel = value; }

        private float _amountConsumed;
        public float AmountConsumed { get => this._amountConsumed; set => this._amountConsumed = value; }


        private bool _didColorSwitch;
        public bool DidScolorSwitch { get => this._didColorSwitch; set => this._didColorSwitch = value; }

        public bool _isColliding = false;
        public CircleF circle;
        MouseState _prevMouseState;



        //-----------------------------------------------------------
        //  Constructor
        public Actor(float scale, Vector2 position)
        {
            _speed = 1.0f;
            _scale = scale;
            //_position = position;
            _velocity = 2.0f;
            Initialize();

            circle = new CircleF(position, scale * 64);
            _nextLevel = circle.Radius * 2.0f;
            _amountConsumed = 0.0f;
            
        }
        //-----------------------------------------------------------

        //-----------------------------------------------------------
        //  Initialize
        //  Initilizes the actor
        public void Initialize()
        {
            //  Load the texture
            _texture = CacheManager.LoadTexture("actor/circle");
            _switchTexture = CacheManager.LoadTexture("actor/circle");
        }

        //-----------------------------------------------------------


        //-----------------------------------------------------------
        //  Update
        public void Update(GameTime gameTime)
        {
            if (_isPlayer)
            {
                if(_didColorSwitch)
                {
                    _didColorSwitch = false;
                }
                CheckInput();
                FollowMouse(gameTime);
                //CameraFollow(gameTime);
            }
            else
            {
                //  TODO: ADD THINK METHOD
                Think(gameTime);
            }

        }
        //-----------------------------------------------------------

        //-----------------------------------------------------------
        //  Think
        //  Does the thinking for the enemy actors
        private void Think(GameTime gameTime)
        {
            var dt = (float)gameTime.ElapsedGameTime.TotalSeconds;
            circle.Center = Vector2.Lerp(circle.Center, new Vector2(circle.Center.X + _direction.X * _velocity, circle.Center.Y + _direction.Y * _velocity), dt);

            if(_isIntroductionActor)
            {
                circle.Center.X = MathHelper.Clamp(circle.Center.X, -circle.Radius - 10, CacheManager.WorldSize.Width / 2.0f);
            }
            else if(circle.Center.X + circle.Radius < CacheManager.BoundryTopLeft.X)
            {
                _shouldRespawn = true;
            }
            else if(circle.Center.X - circle.Radius > CacheManager.BoundryBottomRight.X)
            {
                _shouldRespawn = true;
            }

            if (circle.Center.Y + circle.Radius < CacheManager.BoundryTopLeft.Y)
            {
                _shouldRespawn = true;
            }
            else if (circle.Center.Y - circle.Radius > CacheManager.BoundryBottomRight.Y)
            {
                _shouldRespawn = true;
            }




            //if(circle.Center.X + circle.Radius < CacheManager.BoundryTopLeft.X || circle.Center.X - circle.Radius > CacheManager.BoundryBottomRight.X ||
            //    circle.Center.Y + circle.Radius < CacheManager.BoundryTopLeft.Y || circle.Center.Y - circle.Radius > CacheManager.BoundryBottomRight.Y)
            //{
            //    //  Circle is outside world bounds
            //    _shouldRespawn = true;
            //}
        }
        //-----------------------------------------------------------




        //-----------------------------------------------------------
        //  CheckInput
        //  Checks for input from the player
        private void CheckInput()
        {

            KeyboardState currentKeyboardState = Keyboard.GetState();
            MouseState currentMouseState = Mouse.GetState();

            //  Left click start switching to positive coloring
            //  right click start switching to negative coloring
            if(currentMouseState.LeftButton == ButtonState.Pressed && _prevMouseState.LeftButton == ButtonState.Released && this._isPositive == false)
            {
                //  Start positive switch
                this._isPositive = true;
                this._didColorSwitch = true;
                
            }
            else if (currentMouseState.RightButton == ButtonState.Pressed && _prevMouseState.RightButton == ButtonState.Released && this._isPositive)
            {
                //  Start negative switch
                this._isPositive = false;
                this._didColorSwitch = true;
            }

            _prevMouseState = currentMouseState;

        }
        //-----------------------------------------------------------



        //-----------------------------------------------------------
        //  FollowMouse
        //  Moves the player towards the current mouse position smoothly
        private void FollowMouse(GameTime gameTime)
        {
            float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;
            MouseState mouse = Mouse.GetState();


            Vector2 mouseWorld = CacheManager.Camera.ScreenToWorld(mouse.X, mouse.Y);
            circle.Center = Vector2.Clamp(Vector2.Lerp(circle.Center, mouseWorld, _velocity * dt), CacheManager.BoundryTopLeft + new Vector2(circle.Radius, circle.Radius), CacheManager.BoundryBottomRight - new Vector2(circle.Radius, circle.Radius));


        }
        //-----------------------------------------------------------

        //-----------------------------------------------------------
        //  CameraFolow
        //  Allows the camera to follow the player
        private void CameraFollow(GameTime gameTime)
        {
            Vector2 playerScreenPos = CacheManager.Camera.WorldToScreen(circle.Center);
            Vector2 moveTo = Vector2.Zero;
            //float movementSpeed = 200.0f;
            //movementSpeed = ((playerScreenPos - new Vector2(CacheManager.VirtualSize.Width / 2.0f, CacheManager.VirtualSize.Height / 2.0f))).Length();

            Vector2 velocity = Vector2.Zero;
            velocity.X = Math.Abs(playerScreenPos.X - (CacheManager.VirtualSize.Width / 2.0f));
            velocity.Y = Math.Abs(playerScreenPos.Y - (CacheManager.VirtualSize.Height / 2.0f));

            velocity = Vector2.Multiply(velocity, _speed);

            //Debug.WriteLine($"MovementSPeed: {movementSpeed}");

            if (playerScreenPos.X > CacheManager.CameraSensor.X + CacheManager.CameraSensor.Width)
            {
                moveTo.X = velocity.X;
            }
            else if (playerScreenPos.X < CacheManager.CameraSensor.X)
            {
                moveTo.X = -velocity.X;
            }

            if (playerScreenPos.Y < CacheManager.CameraSensor.Y)
            {
                moveTo.Y = -velocity.Y;
            }
            else if (playerScreenPos.Y > CacheManager.CameraSensor.Y + CacheManager.CameraSensor.Height)
            {
                moveTo.Y = velocity.Y;
            }


            moveTo = moveTo * (float)gameTime.ElapsedGameTime.TotalSeconds;


            Vector2 newPosition = CacheManager.Camera.Position + Vector2.Transform(moveTo, Matrix.CreateRotationZ(CacheManager.Camera.Rotation));


            CacheManager.Camera.Move(moveTo);
            if (CacheManager.Camera.BoundingRectangle.X < 0 || CacheManager.Camera.BoundingRectangle.X + CacheManager.Camera.BoundingRectangle.Width > CacheManager.WorldSize.Width)
            {
                CacheManager.Camera.Move(new Vector2(-moveTo.X, 0));
            }
            if (CacheManager.Camera.BoundingRectangle.Y < 0 || CacheManager.Camera.BoundingRectangle.Y + CacheManager.Camera.BoundingRectangle.Height > CacheManager.WorldSize.Height)
            {
                CacheManager.Camera.Move(new Vector2(0, -moveTo.Y));
            }


        }
        //-----------------------------------------------------------


        //-----------------------------------------------------------
        //  Intersects
        //  Checks if one actor intersects with this actor
        public bool Intersects(Actor other)
        {

            //return ((other.Center - Center).Length() < (other.Radius + Radius));
            return circle.Intersects(ref other.circle);
        }

        //-----------------------------------------------------------

        //-----------------------------------------------------------
        //  Draw
        //  Draws the actor
        public void Draw(GameTime gameTime)
        {
            Matrix transformationMatrix = CacheManager.Camera.GetViewMatrix();

            CacheManager.SpriteBatch.Begin(transformMatrix: transformationMatrix);
            {
                CacheManager.SpriteBatch.Draw(
                    texture: _texture,
                    //position: _position,
                    position: circle.Center,
                    sourceRectangle: null,
                    rotation: 0.0f,
                    color: _isPositive ? Color.White : Color.Black,
                    origin: new Vector2(_texture.Width / 2.0f, _texture.Height / 2.0f),
                    //scale: new Vector2(_scale, _scale),
                    scale: new Vector2(circle.Radius / 64.0f, circle.Radius / 64.0f),
                    effects: SpriteEffects.None,
                    layerDepth: 0.0f
                    );
                ShapeExtensions.DrawCircle(CacheManager.SpriteBatch, circle, 20, _isPositive ? Color.White : Color.Black, 1);
            }
            CacheManager.SpriteBatch.End();
        }
        //-----------------------------------------------------------


    }
}
