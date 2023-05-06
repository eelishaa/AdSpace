using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetBtn : MonoBehaviour
{
    [SerializeField] private GameObject cet;
    [SerializeField] private GameObject mus;
    public void Sett()
    {
        cet.gameObject.SetActive(true);
    }
    public void Yess()
    {
        mus.gameObject.SetActive(false);
        cet.gameObject.SetActive(false);
    }
    public void Noo()
    {
        mus.gameObject.SetActive(true);
        cet.gameObject.SetActive(false);
    }
}
