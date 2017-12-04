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

using Droplet.Models;
using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.IO;

namespace Droplet.Repositories
{
    public class GameSettingsRepository
    {
        //-----------------------------------------------------------
        //  Singleton Setup
        private static GameSettingsRepository _instance;
        private static GameSettingsRepository Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new GameSettingsRepository();
                }

                return _instance;
            }
        }

        //-----------------------------------------------------------


        private GameSettings _gameSettings;
        public static GameSettings GameSettings { get => Instance._gameSettings; set => Instance._gameSettings = value; }

        //-----------------------------------------------------------
        //  Constructor
        public GameSettingsRepository()
        {
            _instance = this;
        }
        //-----------------------------------------------------------


        //-----------------------------------------------------------
        //  Initilize
        //  Initializes the repository
        public static void Initialize()
        {
            Directory.CreateDirectory(Path.Combine(GetRootDirectory(), "Droplet"));
            LoadSettings();
        }
        //-----------------------------------------------------------

        //-----------------------------------------------------------
        //  ResetSettings
        //  Resets settings to their defautls
        public static void ResetSettings()
        {
            Instance._gameSettings = new GameSettings
            {
                IsFullScreen = false,
                MusicVolume = 0.5f,
                SoundVolume = 0.4f,
                MusicOn = true,
                SoundOn = true,
                ForceIntroduction = true
            };
            SaveSettings();
        }
        //-----------------------------------------------------------

        //-----------------------------------------------------------
        //  LoadSettings
        //  Loads Settings from disk.  If file is not found, will create a default setting state
        public static void LoadSettings()
        {
            if (File.Exists(GetSettingsFileUrl()))
            {
                string settingsString = string.Empty;
                using (FileStream fs = new FileStream(GetSettingsFileUrl(), FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    using (StreamReader sr = new StreamReader(fs))
                    {
                        try
                        {
                            settingsString = sr.ReadToEnd();
                        }
                        catch (Exception ex)
                        {
                            Debug.WriteLine("[GameSettingsRepository] - [LoadSettings] - Error Reading Settings File");
                            Debug.WriteLine($"{ex}");

                        }
                    }
                }

                try
                {
                    Instance._gameSettings = JsonConvert.DeserializeObject<GameSettings>(settingsString);
                }
                catch (Exception ex)
                {

                    Debug.WriteLine("[GameSettingsRepository] - [LoadSettings] - Gamesettings File Corrupted");
                    Debug.Write($"{ex}");
                }
            }
            else
            {
                ResetSettings();
            }
        }
        //-----------------------------------------------------------

        //-----------------------------------------------------------
        //  SaveSettings
        //  Saves settings to disk
        public static void SaveSettings()
        {
            using (FileStream fs = new FileStream(GetSettingsFileUrl(), FileMode.Create, FileAccess.Write, FileShare.Write))
            {
                using (StreamWriter sw = new StreamWriter(fs))
                {
                    try
                    {
                        sw.Write(JsonConvert.SerializeObject(Instance._gameSettings, Formatting.Indented));
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine("[GameSettingsRepository] - [SaveSettings] - Error Saving Settings File");
                        Debug.WriteLine($"{ex}");
                    }
                }
            }
        }
        //-----------------------------------------------------------

        //-----------------------------------------------------------
        //  Gets the root directory for the settings
        private static string GetRootDirectory()
        {
            return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "ManBeardGames");
        }
        //-----------------------------------------------------------

        //-----------------------------------------------------------
        //  Gets the fully qualified file path for the settins file
        private static string GetSettingsFileUrl()
        {
            return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "ManBeardGames", "Droplet", "gameSettings.json");
        }
        //-----------------------------------------------------------
    }



}
