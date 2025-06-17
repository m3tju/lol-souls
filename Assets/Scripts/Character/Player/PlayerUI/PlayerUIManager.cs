using UnityEngine;
using Unity.Netcode;

namespace MT
{
    public class PlayerUIManager : MonoBehaviour
    {
        public static PlayerUIManager instance;
        [Header("NETWORK JOIN")]
        [SerializeField] bool startGameAsClient;

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private void Update()
        {
            if (startGameAsClient)
            {
                startGameAsClient = false;
                // must first shut down as a host during title screen
                NetworkManager.Singleton.Shutdown();
                // restart as a client 
                NetworkManager.Singleton.StartClient();
            }
        }
    }
}