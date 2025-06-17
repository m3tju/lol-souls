using UnityEngine;

namespace MT
{
    public class CharacterAnimatorManager : MonoBehaviour
    {
        CharacterManager character;

        protected virtual void Awake()
        {
            character = GetComponent<CharacterManager>(); 
        }

        public void UpdateAnimatorMovementParamters(float horizontalValue, float verticalValue)
        {
            character.animator.SetFloat("Horizontal", horizontalValue);
            character.animator.SetFloat("Vertical", verticalValue);
           
        }
    }
}
