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
