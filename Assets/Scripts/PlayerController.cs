using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _rotateSpeed;
    private float _currentAttractionCharacter = 0;
    [SerializeField] private float _gravityForce = 20;
    private CharacterController _characterController;
    [SerializeField] private Text coinsText;
    private int coins=5;
    void Start()
    {
        _characterController = GetComponent<CharacterController>();
        coinsText.text = $"Соберите ещё {coins} вишней";
    }
    private void Update()
    {
        GravityHandling();
    }
    public void MoveCharacter(Vector3 moveDirection)
    {
        moveDirection = moveDirection *_moveSpeed;
        moveDirection.y = _currentAttractionCharacter;
        _characterController.Move(moveDirection * Time.deltaTime);
    }
   
    public void RotateCharacter(Vector3 moveDirection)
    {
        if(Vector3.Angle(transform.forward, moveDirection) > 0)
        {
            Vector3 newDirection = Vector3.RotateTowards(transform.forward, moveDirection, _rotateSpeed, 0);
            transform.rotation = Quaternion.LookRotation(newDirection);
        }
    }
     private void GravityHandling()
    {
        if (!_characterController.isGrounded)
        {
            _currentAttractionCharacter -= _gravityForce * Time.deltaTime;
        }
        else
        {
            _currentAttractionCharacter = 0;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Coin")
        {
                coins--;
                if (coins == 0) coinsText.text = $"Отнесите вишни на корабль!";
                else coinsText.text = $"Соберите ещё {coins} вишней";
                Destroy(other.gameObject);
        }
    }
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.tag == "Ship" && coins == 0)
        {
            coinsText.text = $"Отлично, миссия выполнена!";
        }
    } 
}
