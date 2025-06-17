using UnityEngine.SceneManagement; // Used for scene detection
using UnityEngine;

namespace MT
{
    public class PlayerInputManager : MonoBehaviour
    {
        public static PlayerInputManager instance;
        PlayerControls playerControls;
        public PlayerManager player;

        [Header("Movement Input")]
        [SerializeField] Vector2 movementInput;
        public float verticalInput;
        public float horizontalInput;
        public float moveAmount;

        [Header("Camera Input")]
        [SerializeField] Vector2 cameraInput;
        public float cameraVerticalInput;
        public float cameraHorizontalInput;

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

        private void Start()
        {
            DontDestroyOnLoad(gameObject);

            SceneManager.activeSceneChanged += SceneManager_activeSceneChanged;

            // Disable input initially (until scene loads)
            instance.enabled = false;
        }

        private void SceneManager_activeSceneChanged(Scene OldScene, Scene NewScene)
        {             
            // enable player inputs when the player is loaded into a game (world scene)
            if (NewScene.buildIndex == WorldSaveGameManager.instance.GetWorldSceneIndex())
            {
                instance.enabled = true;
            }
            else
            {
                instance.enabled = false;
            }
        }

        private void OnEnable()
        {
            if (playerControls == null)
            {
                playerControls = new PlayerControls();         

                playerControls.PlayerMovement.Movement.performed += i => movementInput = i.ReadValue<Vector2>();      
                playerControls.PlayerCamera.Movement.performed += i => cameraInput = i.ReadValue<Vector2>();
            }

            playerControls.Enable();
        }

        private void OnDestroy()
        {
            SceneManager.activeSceneChanged -= SceneManager_activeSceneChanged;
        }

        private void OnApplicationFocus(bool focus)
        {
            // if we minimize the window, stop the adjusting inputs
            if (enabled)
            {
                if (focus)
                {
                    playerControls.Enable();
                }
                else
                {
                    playerControls.Disable();
                }
            }
        }

        private void Update()
        {
            if (player == null)
            {
                return;
            }

            HandleAMovementInput();
            HandleCameraMovementInput();

            if (player.characterAnimatorManager != null)
            {
                player.characterAnimatorManager.UpdateAnimatorMovementParamters(horizontalInput, verticalInput);
            }
        }


        private void HandleAMovementInput()
        {
            verticalInput = movementInput.y;
            horizontalInput = movementInput.x;
            moveAmount = Mathf.Clamp01(Mathf.Abs(verticalInput) + Mathf.Abs(horizontalInput));

            if (moveAmount <= 0.5f && moveAmount > 0)
            {
                moveAmount = 0.5f;
            }
            else if (moveAmount > 0.5f && moveAmount <= 1)
            {
                moveAmount = 1;
            }

            if (player != null && player.characterAnimatorManager != null)
            {
                player.playerAnimatorManager.UpdateAnimatorMovementParamters(0, moveAmount);
            }
        }

        private void HandleCameraMovementInput()
        {
            cameraVerticalInput = cameraInput.y;
            cameraHorizontalInput = cameraInput.x;
        }
    }
}
