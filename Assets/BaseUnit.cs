using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseUnit : MonoBehaviour
{
    public GameObject selectEffect;
    public void OnSelect()
    {
        selectEffect.SetActive(true);
    }
    public void OnDeSelect()
    {
        selectEffect.SetActive(false);
    }
}
