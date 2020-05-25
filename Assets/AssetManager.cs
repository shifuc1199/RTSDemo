using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
public class AssetManager   
{
 
    public void InstantiateByEnum(Enum _enum)
    {
        Addressables.InstantiateAsync(_enum.ToString());
    }
}
