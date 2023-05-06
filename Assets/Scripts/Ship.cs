using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ship : MonoBehaviour
{
    [SerializeField] private GameObject canvas;
    [SerializeField] private Text wood;
    [SerializeField] private Text rocks;
    [SerializeField] private int startrocks;
    [SerializeField] private int startwood;
    [SerializeField] private GameObject person;
    private int a, b;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Character")
        {
            Debug.Log("000");
            a=person.gameObject.GetComponent<PlayerController>().getRocks();
            b = person.gameObject.GetComponent<PlayerController>().getWood();
            if (b != 0)
            {
                startwood -= b;
                wood.text = $"{startwood}";
                person.gameObject.GetComponent<PlayerController>().setWood(0);
            }
            else if (a != 0)
            {
                startrocks -= a;
                rocks.text = $"{startrocks}";
                person.gameObject.GetComponent<PlayerController>().setRocks(0);
            }
            canvas.gameObject.SetActive(true);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Character")
        {
            canvas.gameObject.SetActive(false);
        }
    }
}
