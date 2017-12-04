using Droplet.Extensions;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Droplet.UIFramework
{
    public class Image : UIComponentBase
    {
        //------------------------------------------------------------
        //  Constructor

        public Image(string id, Vector2 position, Size<int> size, Texture2D texture, UIComponentBase container = null)
            : base(id, position, size, texture, container)
        {

        }

        //------------------------------------------------------------

        //------------------------------------------------------------
        //  UpdateSize
        //  Used to update the size of the image

        public void UpdateSize(Size<int> size)
        {
            Size = size;
        }

        //------------------------------------------------------------

        //------------------------------------------------------------
        //  Draw
        //  Draws the image to the scren

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)

        {
            base.Draw(gameTime, spriteBatch);
            spriteBatch.Draw(Texture, new Rectangle((int)Position.X, (int)Position.Y, Size.Width, Size.Height), Color.White);
        }

        //------------------------------------------------------------
    }
}
