using UnityEngine;

namespace Managers
{
    public class ResetCounter : MonoBehaviour
    {
        #region singleton
        public static ResetCounter Instance;
    
        private void Awake()
        {
            if (Instance == null) 
            {
                DontDestroyOnLoad(gameObject);
                Instance = this;
            }
            else if (Instance !=this)
            {
                Destroy(gameObject);
            }
        }
    

        #endregion
        public int resetCounted;
    }
}
