namespace Droplet.Models
{
    //------------------------------------------------------------
    //  GameState
    //  Enums used to manage the gamestate

    public enum GameState
    {
        None,
        Playing,
        Paused,
        GameOver,
        Waiting,
        Introduction
    }

    //------------------------------------------------------------

    //------------------------------------------------------------
    //  IntroductionTriggers
    //  Introduction triggers are used to trigger events 
    //  when the introduction/how to play is being shown

    public enum IntroductionTriggers
    {
        None,
        ClickPlayer,
        ConsumeFood,
        ColorSwitch,
        Start
    }

    //------------------------------------------------------------
}
