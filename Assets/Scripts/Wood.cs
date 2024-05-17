using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Wood : MonoBehaviour
{

    [SerializeField] private float _hp;
    [SerializeField] private int _count;
    [SerializeField] private int _timeForRebirth;
    [SerializeField] private GameObject robot;
    [SerializeField] private ParticleSystem fire;
    [SerializeField] private GameObject person;
    private Animator anim;
    private GameObject offElement;
    private float generalHP;
    private bool once = false;
    private void Start()
    {
        _hp *= 10;
        generalHP = _hp;
        offElement = this.transform.GetChild(0).gameObject;
        anim = this.transform.GetChild(0).GetComponent<Animator>();
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Character")
        {
            if (_hp != 0)
            {
                robot.gameObject.SetActive(true);
                fire.Play();
                _hp--;
            }
            else
            {
                if (!once)
                {
                    once = true;
                    robot.gameObject.SetActive(false);
                    fire.Stop();
                    person.gameObject.GetComponent<PlayerController>().setWood(_count);
                    LiveCycle();
                }
            }
        }
    }
    private void LiveCycle()
    {
        StartCoroutine(RemoveObjects());
        StartCoroutine(CreateObjects(_timeForRebirth));
    }
    private IEnumerator RemoveObjects()
    {
        anim.SetBool("isDestroy", true);
        yield return new WaitForSeconds(2);
        offElement.SetActive(false);
    }
    private IEnumerator CreateObjects(int time)
    {
        Debug.Log(111);
        yield return new WaitForSeconds(time);
        Debug.Log(222);
        offElement.transform.localScale = new Vector3(0,0,0);
        offElement.gameObject.SetActive(true);
        anim.SetBool("isCreate", true);
        yield return new WaitForSeconds(2);
        anim.SetBool("isDestroy", false);
        anim.SetBool("isCreate", false);
        _hp = generalHP;
        once = false;
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Character")
        {
            robot.gameObject.SetActive(false);
            fire.Stop();
            //Debug.Log(_hp);
        }
    }
}
