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
