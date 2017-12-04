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

namespace Droplet.UIFramework
{
    //------------------------------------------------------------
    //  UIType
    //  The types of UI Elements used by the UI Framework

    public enum UIType
    {
        Component,
        Label,
        Button,
        Menu
    }

    //------------------------------------------------------------

    //------------------------------------------------------------
    //  TextAlignment
    //  Used to define text alignment for UI Elements in the UI Framework
    
    public enum TextAlignment
    {
        /// <summary>
        /// Align text to the top left of the bounding area
        /// </summary>
        TopLeft,

        /// <summary>
        /// Alignt text to the top center of the bounding area
        /// </summary>
        TopCenter,

        /// <summary>
        /// Align text to the top right of the bounding area
        /// </summary>
        TopRight,

        /// <summary>
        /// Align text to the middle left of the bounding area
        /// </summary>
        MiddleLeft,

        /// <summary>
        /// Align text to the middle of the bounding area
        /// </summary>
        Middle,

        /// <summary>
        /// Align text to the middle right of the bounding area
        /// </summary>
        MiddleRight,

        /// <summary>
        /// Align text to the bottom left of the bounding area
        /// </summary>
        BottomLeft,

        /// <summary>
        /// Align text to the bottom middle of the bounding area
        /// </summary>
        BottomCenter,

        /// <summary>
        /// Align text to the bottom right of the bounding area
        /// </summary>
        BottomrRight
    }

    //------------------------------------------------------------
}
