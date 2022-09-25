using System.Collections;
using System.Collections.Generic;
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
            if (context.started) _hero.IsCrouch = true;
            if (context.canceled) _hero.IsCrouch = false;
        }
    }
}