using Unity.Netcode;
using Unity.VisualScripting;
using UnityEngine;

namespace MT
{
    // Inherit from NetworkBehaviour to enable network features
    public class CharacterManager : NetworkBehaviour
    {
        // Public: So other classes (like PlayerManager) can assign or access this component
        [HideInInspector] public CharacterController characterController;
        [HideInInspector] public Animator animator;
        CharacterNetworkManager characterNetworkManager;
        // Awake() is called when the script instance is loaded    
        protected virtual void Awake()
        {
            DontDestroyOnLoad(this);

            characterController = GetComponent<CharacterController>();
            animator = GetComponent<Animator>();
            characterNetworkManager = GetComponent<CharacterNetworkManager>();
        }
        // Update() is called every frame
        protected virtual void Update()
        {
            // assigns our game object to a netwrok position on our screen
            if (IsOwner)
            {
                characterNetworkManager.networkPosition.Value = transform.position;
                characterNetworkManager.networkRotation.Value = transform.rotation;
            }
            //  assigns someone elses game object to network position on our screen 
            else
            {
                // Position
                transform.position = Vector3.SmoothDamp(transform.position, characterNetworkManager.networkPosition.Value, ref characterNetworkManager.networkPositionVelocity, characterNetworkManager.networkPositionSmoothTime);
                // Rotation
                transform.rotation = Quaternion.Slerp(transform.rotation, characterNetworkManager.networkRotation.Value, characterNetworkManager.networkRotationSmoothTime);
            }
            // this basicly makes sure that if two players are playing togheter the movement of their characters is visible on both screens, so transfering network positions between the computers
        }

        protected virtual void LateUpdate()
        {
            
        }
    }
}
