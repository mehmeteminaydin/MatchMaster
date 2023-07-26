using UnityEngine;
using Configuration;


namespace SNG.Configs{

    [DefaultExecutionOrder(-120)]
    public class Configs : MonoBehaviour
    {
        
        private static Configs s_instance;

        [SerializeField] private ThemeConfig _themeConfig = null;

        public static ThemeConfig ThemeConfig => s_instance._themeConfig;

        void Awake()
        {
            if (s_instance == null)
            {
                s_instance = this;
            }
        }
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
