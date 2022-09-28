using UnityEngine;
using UnityEngine.InputSystem;

namespace Creatures.Hero
{
    public class InputReader : MonoBehaviour
    {
        [SerializeField] private Hero _hero;

        public void OnMovement(InputAction.CallbackContext context)
        {
            _hero.DirectionX = context.ReadValue<float>();
        }

        public void OnJump(InputAction.CallbackContext context)
        {
            if (context.started)
            {
                _hero.DesiredJump = true;
                _hero.PressingJump = true;
            }

            if (context.canceled) _hero.PressingJump = false;
        }

        public void OnCrouch(InputAction.CallbackContext context)
        {
            if (context.started) _hero.StartCrouch();
            if (context.canceled) _hero.StopCrouch();
        }

        public void OnAttack(InputAction.CallbackContext context)
        {
            if (context.started) _hero.Attack();
        }

        public void OnInteract(InputAction.CallbackContext context)
        {
            if (context.started) _hero.Interact();
        }

        public void OnMenu(InputAction.CallbackContext context)
        {
            
        }
    }
}