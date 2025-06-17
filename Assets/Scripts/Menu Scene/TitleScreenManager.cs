using System.Collections;
using UnityEngine;
using Unity.Netcode;

namespace MT
{
    public class TitleScreenManager : MonoBehaviour
    {
        public void Start()
        {
            if (!NetworkManager.Singleton.IsHost && !NetworkManager.Singleton.IsClient)
            {
                NetworkManager.Singleton.StartHost();     // Starts the game as a host
            }
        }

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        public void StartNewGame()
        {
            StartCoroutine(WorldSaveGameManager.instance.LoadNewGame());
        }
    }
}
