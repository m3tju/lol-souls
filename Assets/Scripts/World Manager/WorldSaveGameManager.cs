using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MT
{
    public class WorldSaveGameManager : MonoBehaviour
    {
        public static WorldSaveGameManager instance; // Only declared once

        [SerializeField] int worldSceneIndex = 1;

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
                DontDestroyOnLoad(gameObject); // persist across scenes
            }
            else
            {
                Destroy(gameObject);
            }
        }

        public IEnumerator LoadNewGame()
        {
            AsyncOperation loadOperation = SceneManager.LoadSceneAsync(worldSceneIndex);
            yield return loadOperation;
        }

        public int GetWorldSceneIndex()
        {
            return worldSceneIndex;
        }
    }
}
