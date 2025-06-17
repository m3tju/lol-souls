
using UnityEngine;

namespace MT
{

    public class PlayerManager : CharacterManager
    {
        [HideInInspector] public PlayerMoveAroundManager playerMoveAroundManager;
        [HideInInspector] public PlayerAnimatorManager playerAnimatorManager;
        public CharacterAnimatorManager characterAnimatorManager;

        protected override void Awake()
        {
            base.Awake();

            playerMoveAroundManager = GetComponent<PlayerMoveAroundManager>();
            playerAnimatorManager = GetComponent<PlayerAnimatorManager>();
        }

        protected override void Update()
        {
            base.Update();

            // if we dont own the gameobject we cant control or edit it
            if (!IsOwner)
                return;

            playerMoveAroundManager.HandleAllMovement();
        }

        protected override void LateUpdate()
        {
            if (!IsOwner)
                return;

            base.LateUpdate();

            PlayerCamera.instance.HandleAllCameraActions();
        }

        public override void OnNetworkSpawn()
        {
            base.OnNetworkSpawn();

            if (IsOwner)
            {
                PlayerCamera.instance.player = this;
                PlayerInputManager.instance.player = this;
            }

            if (playerAnimatorManager == null)
            {
                playerAnimatorManager = GetComponent<PlayerAnimatorManager>();
            }
        }
        
    }

}
