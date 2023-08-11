using UnityEngine;
// to check the active scene
using UnityEngine.SceneManagement;

namespace SNG.Save
{
    [DefaultExecutionOrder(-101)]
    public class SaveGame : MonoBehaviour
    {
        // in order to set the Application.targetFrameRate, we need to check the device's RAM
        public int lowRAMThresholdMB = 2048;
        public static SaveGame Instance => s_instance;
        private static SaveGame s_instance;
       
        private const string GeneralDataKey = "mxye)ds";
        private const string PlayerDataKey = "udfsd9d";
        private const string GameStateKey = "mxye)d0";

        [SerializeField] private PlayerData _playerData;
        [SerializeField] private GeneralData _generalData;
        [SerializeField] private GameState _gameState;

        public GeneralData GeneralData
        {
            get => _generalData;
            set => _generalData = value;
        }
        public PlayerData PlayerData
        {
            get => _playerData;
            set => _playerData = value;
        }

        public GameState GameState
        {
            get => _gameState;
            set => _gameState = value;
        }

        /// <summary>
        /// Loads the private classes
        /// </summary>
        public void Load()
        {
            try
            {
                // set default values of classes if it is the user's first session
                if (PlayerPrefs.GetInt("FirstTime") == 0)
                {
                    FirstTime();
                    return;
                }

                _generalData = FileManager.Load<GeneralData>(GeneralDataKey);
                _playerData = FileManager.Load<PlayerData>(PlayerDataKey);
                _gameState = FileManager.Load<GameState>(GameStateKey);
            }
            catch (System.Exception ex)
            {
                // something went wrong
                // sending an error message is encouraged here

                // reinitialize the data
                FirstTime();
            }
        }

        /// <summary>
        /// Initialize all classes
        /// </summary>
        private void FirstTime()
        {
            // turn off vSync
            QualitySettings.vSyncCount = 0;

            int systemMemoryMB = SystemInfo.systemMemorySize;

            if (systemMemoryMB <= lowRAMThresholdMB)
            {
                Application.targetFrameRate = 30;
            }
            else
            {
                Application.targetFrameRate = 60;
            }
            
            _generalData = new GeneralData();
            _playerData = new PlayerData();
            _gameState = new GameState();
            PlayerPrefs.SetInt("FirstTime", 1);
            PlayerPrefs.Save();
        }

        /// <summary>
        /// Saves the private classes
        /// </summary>
        public void Save()
        {
            try
            {
                FileManager.Save(GeneralDataKey, _generalData);
                FileManager.Save(PlayerDataKey, _playerData);
                // check the active scene and save the game state accordingly
                
                GameEvents.Instance.CallOnSaveGame();
                FileManager.Save(GameStateKey, _gameState);
                
            }
            catch (System.Exception ex)
            {
                // something went wrong
                // sending an error message is encouraged here
            }
        }

        private void Awake()
        {
            if (!s_instance)
            {
                s_instance = this;
                DontDestroyOnLoad(gameObject);
                Load();
            }
            else
            {
                DestroyImmediate(gameObject);
            }
        }
        

#if UNITY_EDITOR
        void OnApplicationQuit()
        {
            if (s_instance == this)
                Save();
        }
#else
        
        public void OnApplicationPause(bool paused)
        {
            if (paused)
                Save();
        }
#endif

    }

}