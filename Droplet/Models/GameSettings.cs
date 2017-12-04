namespace Droplet.Models
{
    public class GameSettings
    {
        private float _musicVolume;
        public float MusicVolume { get => this._musicVolume; set => this._musicVolume = value; }

        private bool _musicOn;
        public bool MusicOn { get => this._musicOn; set => this._musicOn = value; }

        private float _soundVolume;
        public float SoundVolume { get => this._soundVolume; set => this._soundVolume = value; }

        private bool _soundOn;
        public bool SoundOn { get => this._soundOn; set => this._soundOn = value; }

        private bool _isFullScreen;
        public bool IsFullScreen { get => this._isFullScreen; set => this._isFullScreen = value; }

        private bool _forceIntroduction;
        public bool ForceIntroduction { get => this._forceIntroduction; set => this._forceIntroduction = value; }


        public GameSettings() { }
    }
}
