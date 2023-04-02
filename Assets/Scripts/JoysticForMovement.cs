using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoysticForMovement : JoystickHandler
{
    [SerializeField] private PlayerController characterMovement;

    private void Update()
    {
        if (_inputVector.x != 0 || _inputVector.y != 0)
        {
            characterMovement.MoveCharacter(new Vector3(_inputVector.x, 0, _inputVector.y));
            characterMovement.RotateCharacter(new Vector3(_inputVector.x, 0, _inputVector.y));
        }
        
    }
   
}
