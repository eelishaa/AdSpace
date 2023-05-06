using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : MonoBehaviour
{
    [SerializeField] private float _hp;
    [SerializeField] private int _count;
    [SerializeField] private GameObject robot;
    [SerializeField] private ParticleSystem fire;
    [SerializeField] private ParticleSystem destroyFire;
    [SerializeField] private GameObject person;
    private void Start()
    {
        _hp *= 10;
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Character")
        {
            if (_hp != 0) {
                robot.gameObject.SetActive(true);
                fire.Play();
                _hp--;
                Debug.Log(_hp);
            }
            else {
                robot.gameObject.SetActive(false);
                fire.Stop();
                person.gameObject.GetComponent<PlayerController>().setRocks(_count);
                destroyFire.gameObject.SetActive(true);
                destroyFire.Play();
                Destroy(this.gameObject, 5.0f);
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Character")
        {
            robot.gameObject.SetActive(false);
            fire.Stop();
            Debug.Log(_hp);
        }
    }
}
