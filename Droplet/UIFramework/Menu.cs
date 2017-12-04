﻿using Droplet.Extensions;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.BitmapFonts;
using System;
using System.Collections.Generic;

namespace Droplet.UIFramework
{
    public class Menu : UIComponentBase
    {
        private List<UIComponentBase> _UIComponents;
        public List<UIComponentBase> UIComponents { get => this._UIComponents; set => this._UIComponents = value; }

        private UIComponentBase _prevSelected;
        public UIComponentBase PrevSelected { get => this._prevSelected; set => this._prevSelected = value; }

        private UIComponentBase _selected;
        public UIComponentBase Selected { get => this._selected; set => this._selected = value; }





        //------------------------------------------------------------
        //  Constructor

        public Menu(string id, Vector2 position, Size<int> size, Texture2D texture, UIComponentBase container = null)
            : base(id, position, size, texture, container)
        {
            this.UIComponents = new List<UIComponentBase>();
        }

        //------------------------------------------------------------

        //------------------------------------------------------------
        //  Initialize
        //  Initializes the menu

        public override void Initialize()
        {
            base.Initialize();
        }

        //------------------------------------------------------------

        //------------------------------------------------------------
        //  Update
        //  Used to update the menu each frame

        public override void Update(GameTime gameTime)
        {
            foreach (UIComponentBase UIComponentBase in _UIComponents)
            {
                UIComponentBase.Update(gameTime);
            }
            base.Update(gameTime);
        }

        //------------------------------------------------------------

        //------------------------------------------------------------
        //  Draw
        //  Draws the menu to the screen

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if (Texture != null && NineSlices != null)
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
                        layerDepth: 0.0f);
                }
            }
            foreach (UIComponentBase UIComponentBase in _UIComponents)
            {
                UIComponentBase.Draw(gameTime, spriteBatch);
            }
            base.Draw(gameTime, spriteBatch);
        }

        //------------------------------------------------------------

        //------------------------------------------------------------
        //  AddLabel
        //  Adds a label to the menu

        public Label AddLabel(string id, Vector2 position, Size<int> size, BitmapFont font, string text, TextAlignment alignment, Color color)
        {
            Label label = new Label(
                id: id,
                position: position,
                size: size,
                texture: null,
                container: this,
                font: font,
                text: text,
                alignment: alignment,
                color: color);
            this.UIComponents.Add(label);
            return label;
        }

        //------------------------------------------------------------

        //------------------------------------------------------------
        //  Add Button
        //  Adds a button to the menu

        public Button AddButton(string id, Vector2 position, Size<int> size, Texture2D texture, BitmapFont font, string text, TextAlignment alignment, Color fontColor, Color backgroundColor, Color selectedColor, Action initilizeAction = null)
        {
            Button button = new Button(
                id: id,
                position: position,
                size: size,
                texture: texture,
                container: this,
                font: font,
                text: text,
                textAlignment: alignment,
                fontColor: fontColor,
                backgroundColor: backgroundColor,
                selectedColor: selectedColor);
            this.UIComponents.Add(button);
            return button;
        }

        //------------------------------------------------------------

        //------------------------------------------------------------
        //  RemoveAll
        //  Removes all components from the Menu

        public void RemoveAll()
        {
            foreach (UIComponentBase UIComponentBase in UIComponents)
            {
                if (UIComponentBase.UIType == UIType.Button)
                {
                    ((Button)UIComponentBase).Dispose();
                }
            }
            UIComponents.Clear();
            UIComponents = null;

        }

        //------------------------------------------------------------

        //------------------------------------------------------------
        //  Dispose
        //  Used by the IDisposable Interface

        public override void Dispose()
        {
            this.RemoveAll();
        }

        //------------------------------------------------------------
    }
}
