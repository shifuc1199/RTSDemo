using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class CameraController : MonoBehaviour
{
    public float moveSpeed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 offset = Camera.main.ScreenToViewportPoint(Mouse.current.position.ReadValue());
 
        if(offset.x>1)
        {
            transform.Translate(Vector3.right * moveSpeed * Time.deltaTime,Space.World);
        }
        if (offset.x < 0)
        {
            transform.Translate(Vector3.left * moveSpeed * Time.deltaTime, Space.World);
        }
        if (offset.y > 1)
        {
            transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime, Space.World);
        }
        if (offset.y <0)
        {
            transform.Translate(Vector3.back * moveSpeed * Time.deltaTime, Space.World);
        }
    }
}
