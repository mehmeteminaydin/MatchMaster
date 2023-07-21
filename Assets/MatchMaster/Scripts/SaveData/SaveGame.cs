using UnityEngine;

namespace SNG.Save
{
    [DefaultExecutionOrder(-101)]
    public class SaveGame : MonoBehaviour
    {
        public static SaveGame Instance => s_instance;
        private static SaveGame s_instance;
       
        private const string GeneralDataKey = "mxye)ds";
        private const string PlayerDataKey = "udfsd9d";

        [SerializeField] private PlayerData _playerData;
        [SerializeField] private GeneralData _generalData;

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
            _generalData = new GeneralData();
            _playerData = new PlayerData();
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