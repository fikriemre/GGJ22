using System;
using UnityEngine;

namespace Shinjingi
{
  
    public class PlayerController : MonoBehaviour
    {
        public Jump jump;
        public Move move;

        private void Update()
        {
            move.MoveInput(Input.GetAxis("Horizontal"));
            jump.JumpInput(Input.GetKey(KeyCode.Space));
        }
    }
}
