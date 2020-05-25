using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using DreamerTool.Util;
using UnityEngine.ResourceManagement.AsyncOperations;

public class BuildTest : MonoBehaviour
{
    
    private BoxCollider selectBuilding;
    List<BuildCheckCube> checkCubeList = new List<BuildCheckCube>();
    private void Awake()
    {
         
    }
    public void SetSelectBuilding()
    {
        Addressables.InstantiateAsync("Building").Completed += InitSelectBuilding;
    }
    public void InitSelectBuilding(AsyncOperationHandle<GameObject> handle)
    {
        selectBuilding = handle.Result.GetComponent<BoxCollider>();
        var point = new Vector3(-selectBuilding.bounds.extents.x, -selectBuilding.bounds.extents.y, -selectBuilding.bounds.extents.z);

        for (int i = (int)point.x; i < (int)point.x + Mathf.Ceil(selectBuilding.bounds.size.x); i++)
        {
            for (int j = (int)point.z; j < (int)point.z + Mathf.Ceil(selectBuilding.bounds.size.z); j++)
            {
                int tempi = i;
                int tempj = j;
                Addressables.InstantiateAsync("BuildCheckCube", selectBuilding.transform).Completed += (a) => {
                    a.Result.transform.localPosition = new Vector3(tempi, selectBuilding.bounds.center.y + point.y + 0.25f, tempj);
                    a.Result.transform.rotation = Quaternion.Euler(90, 0, 0);
                    checkCubeList.Add(a.Result.GetComponent<BuildCheckCube>());
                };
            }
        }
    }
    void HandleSelectBuildingCursor()
    {
        if (selectBuilding == null)
            return;

        var hit = DreamerUtil.GetMouseHit();
        if (hit.collider != null)
            selectBuilding.transform.position = hit.point;

        foreach (var checkCube in checkCubeList)
        {
            checkCube.Check();
        }
    }
    void Update()
    {
        HandleSelectBuildingCursor();
    }
}
