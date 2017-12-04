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
