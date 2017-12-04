using Microsoft.Xna.Framework;

namespace Droplet.UIFramework
{
    public class NineSlice
    {
        private Rectangle _sourceRectangle;
        public Rectangle SourceRectangle { get => this._sourceRectangle; set => this._sourceRectangle = value; }

        private Vector2 _posiion;
        public Vector2 Position { get => this._posiion; set => this._posiion = value; }

        private Vector2 _scale;
        public Vector2 Scale { get => this._scale; set => this._scale = value; }

        //------------------------------------------------------------
        //  Constructor

        public NineSlice(Rectangle sourceRectangle, Vector2 position, Vector2 scale)
        {
            _sourceRectangle = sourceRectangle;
            _posiion = position;
            _scale = scale;
        }

        //------------------------------------------------------------
    }
}
