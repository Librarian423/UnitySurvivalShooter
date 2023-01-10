using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public string moveVaxisName = "Vertical";
    public string moveHaxisName = "Horizontal";
    public string fireButtonName = "Fire1";
    
    public float moveV { get; private set; }
    public float moveH { get; private set; }
    public bool fire { get; private set; }

    public Vector3 mousePos { get; private set; }

    void Update()
    {
        if (GameManager.instance != null && GameManager.instance.isGameover)
        {
            moveV = 0;
            moveH = 0;
            fire = false;
            mousePos = Vector3.zero;
            return;
        }

        moveV = Input.GetAxis(moveVaxisName);
        moveH = Input.GetAxis(moveHaxisName);
        fire = Input.GetButton(fireButtonName);

        mousePos = Input.mousePosition;        
    }
}
