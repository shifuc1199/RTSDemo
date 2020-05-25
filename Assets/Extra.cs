using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Vector3Extra 
{
    public static Vector3 AddX(this Vector3 v,float x)
    {
        return v + new Vector3(x, 0, 0);
    }
    public static Vector3 AddZ(this Vector3 v, float z)
    {
        return v + new Vector3(0, 0, z);
    }
    public static Vector3 MinusX(this Vector3 v, float x)
    {
        return v - new Vector3(x, 0, 0);
    }
    public static Vector3 MinusZ(this Vector3 v, float z)
    {
        return v - new Vector3(0, 0, z);
    }
}
