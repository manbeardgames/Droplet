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
