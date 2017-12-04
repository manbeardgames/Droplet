namespace Droplet.Extensions
{
    public class Size<T>
    {
        private T _width;
        public T Width { get => _width; set => _width = value; }

        private T _height;
        public T Height { get => _height; set => _height = value; }

        //------------------------------------------------------------
        //  Constructors

        public Size()
        {
            _width = default(T);
            _height = default(T);
        }

        public Size(T width, T height)
        {
            _width = width;
            _height = height;
        }

        //------------------------------------------------------------

        //------------------------------------------------------------
        //  Add
        //  Used to add two sizes together

        public static Size<int> Add(Size<int> sizeOne, Size<int> sizeTwo)
        {
            return new Size<int>(width: sizeOne.Width + sizeTwo.Width, height: sizeOne.Height + sizeTwo.Height);
        }

        public static Size<float> Add(Size<float> sizeOne, Size<float> sizeTwo)
        {
            return new Size<float>(width: sizeOne.Width + sizeTwo.Width, height: sizeOne.Height + sizeTwo.Height);
        }

        //------------------------------------------------------------


        //------------------------------------------------------------
        //  Subtract
        //  Used to subtract two sizes

        public static Size<int> Subtract(Size<int> sizeOne, Size<int> sizeTwo)
        {
            return new Size<int>(width: sizeTwo.Width - sizeOne.Width, height: sizeTwo.Height - sizeOne.Height);
        }

        public static Size<float> Subtract(Size<float> sizeOne, Size<float> sizeTwo)
        {
            return new Size<float>(width: sizeTwo.Width - sizeOne.Width, height: sizeTwo.Height - sizeOne.Height);
        }

        //------------------------------------------------------------


        //------------------------------------------------------------
        //  ToString
        //  Creates a string representation of the size object

        public override string ToString()
        {
            return $"{{{this._width}, {this._height}}}";
        }

        //------------------------------------------------------------


    }
}
