using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using DreamerTool.Util;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.InputSystem;

public class BuildTest : MonoBehaviour
{
   
    private BoxCollider selectBuilding;
    List<BuildCheckCube> checkCubeList = new List<BuildCheckCube>();
     
    private void Awake()
    {
       Addressables.LoadAssetsAsync<GameObject>("Building",null);
    }

    public void SetSelectBuilding(int buildType)
    {
       if(selectBuilding!=null)
        {
            Destroy(selectBuilding.gameObject);
            selectBuilding = null;
            checkCubeList.Clear();
        }
        var hit = DreamerUtil.GetMouseHit();
        Addressables.InstantiateAsync(((BuildingType)buildType).ToString(), hit.point, Quaternion.identity).Completed += InitSelectBuilding;
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
                    a.Result.transform.localPosition = new Vector3(tempi,  0.1f   , tempj);
                    a.Result.transform.rotation = Quaternion.Euler(90, 0, 0);
                    checkCubeList.Add(a.Result.GetComponent<BuildCheckCube>());
                };
            }
        }
    }
    public void Build()
    {
        foreach (var checkCube in checkCubeList)
        {
            if (!checkCube.isBuildable)
                return;
        }

        selectBuilding.gameObject.layer = LayerMask.NameToLayer("Building");
        selectBuilding = null;
        foreach (var checkCube in checkCubeList)
        {
            Destroy(checkCube.gameObject);
        }
        checkCubeList.Clear();
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

        if(Mouse.current.leftButton.wasPressedThisFrame)
        {
            Build();
        }
       
    }
    
    void Update()
    {
        HandleSelectBuildingCursor();
      


    }
}
