using DreamerTool.Util;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SelectManager : MonoBehaviour
{
    public Texture tex;
    Vector2 startMousePos;
    Vector2 endMosuePos;
    bool isDraw;
    Vector3 startWorldPos;
    Vector3 endWorldPos;
    void SelectUnitHandle()
    {

        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            startMousePos = Mouse.current.position.ReadValue();
            startWorldPos = DreamerUtil.GetMouseHit().point;
        }
        if (Mouse.current.leftButton.isPressed)
        {
            endMosuePos = Mouse.current.position.ReadValue();
            endWorldPos = DreamerUtil.GetMouseHit().point;
            if(Vector2.Distance(startMousePos,endMosuePos)>=5)
            {
                isDraw = true;
            }

        }
        if (Mouse.current.leftButton.wasReleasedThisFrame)
        {
            isDraw = false;

            var size = new Vector3(Mathf.Abs(endWorldPos.x - startWorldPos.x), 1, Mathf.Abs(endWorldPos.z - startWorldPos.z));
            var pos = (startWorldPos + endWorldPos) / 2;
            var units = Physics.OverlapBox(pos, size / 2, Quaternion.identity, LayerMask.GetMask("Unit"));
            foreach (var unit in units)
            {
                unit.GetComponent<BaseUnit>().OnSelect();
            }
        }
     
    }

    private void OnDrawGizmos()
    {

        if (isDraw)
        {
            var size = new Vector3(Mathf.Abs(endWorldPos.x - startWorldPos.x), 1, Mathf.Abs(endWorldPos.z - startWorldPos.z));
            var pos = (startWorldPos + endWorldPos) / 2;
            Gizmos.DrawCube(pos, size);
        }
    }
    private void OnGUI()
    {
        if (Event.current.type.Equals(EventType.Repaint) && isDraw)
        {
            Graphics.DrawTexture(new Rect(startMousePos.x, Screen.height - startMousePos.y, endMosuePos.x - startMousePos.x, startMousePos.y - endMosuePos.y), tex, 5, 5, 5, 5);
        }

    }
    private void Update()
    {
        SelectUnitHandle();
    }
}
