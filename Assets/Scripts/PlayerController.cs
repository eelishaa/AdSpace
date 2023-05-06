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
    [SerializeField] private Text woodText;
    [SerializeField] private Text rocksText;
    private int wood=0;
    private int rocks=0;
    private int coins=0;
    void Start()
    {
        woodText.text = $"{wood}";
        rocksText.text = $"{rocks}";
        coinsText.text = $" {coins}";
        _characterController = GetComponent<CharacterController>();
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
                coins++;
                coinsText.text = $" {coins}";
                Destroy(other.gameObject);
        }
        
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Water")
        {
            if (coins != 0) coins--;
            else if (rocks != 0) rocks--;
            else if (wood != 0) wood--;
            woodText.text = $"{wood}";
            rocksText.text = $"{rocks}";
            coinsText.text = $" {coins}";
        }
    }
    public int getWood()
    {
        return wood;
    }
    public int getRocks()
    {
        return rocks;
    }
    public void setRocks(int o)
    {
        rocks = o;
        rocksText.text = $"{rocks}";
    }
    public void setWood(int o)
    {
        wood=o;
        woodText.text = $"{wood}";
    }
}
