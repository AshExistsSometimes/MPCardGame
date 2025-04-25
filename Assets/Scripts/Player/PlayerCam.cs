using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerCam : MonoBehaviour
{
    [Header("Camera Sensitivity")]
    public float XSens;
    public float YSens;

    [Header("Orientation Transform")]
    public Transform orientation;

    private float xRot;
    private float yRot;

    [Header ("Camera Clamp Values")]
    [Range(0, 360)]
    public float xRotClamp = 89.9f;
    [Range(0, 360)]
    public float yRotClamp = 45f;

    [Header("Cursor Lock State (DEBUG)")]
    public bool CursorLocked = true;

    private void Start()
    {
        CursorLocked = true ;
    }

    private void Update()
    {
        UpdateCursorLockState();

        GetMouseInput();
        RotateCamera();
    }



    /////////////////////////
    ///
    private void RotateCamera()
    {
        if (CursorLocked)
        {
            transform.rotation = Quaternion.Euler(xRot, yRot, 0);
            orientation.rotation = Quaternion.Euler(0, yRot, 0);
        }
    }
    private void GetMouseInput()
    {
        if (CursorLocked)
        {
            // Get mouse input
            float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * XSens;
            float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * YSens;

            yRot += mouseX;
            yRot = Mathf.Clamp(yRot, -yRotClamp, yRotClamp);

            xRot -= mouseY;
            xRot = Mathf.Clamp(xRot, -xRotClamp, xRotClamp);
        }
    }
    private void UpdateCursorLockState() // Locks or unlocks cursor depending on if CursorLocked bool is true or false
    {
        if (CursorLocked)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        else if (!CursorLocked)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

}
