using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildCheckCube : MonoBehaviour
{
    public bool isBuildable;
    public void Check()
    {
        if (Physics.CheckBox(transform.position, new Vector3(0.5f, 0, 0.5f),Quaternion.identity,LayerMask.GetMask("Building")))
        {
            GetComponent<SpriteRenderer>().color = Color.red;
            isBuildable = false;
        }
        else
        {
            GetComponent<SpriteRenderer>().color = Color.green;
            isBuildable = true;
        }
    }
   
}
