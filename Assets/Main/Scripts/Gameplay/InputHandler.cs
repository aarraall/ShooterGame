using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler 
{
    public Quaternion GunRotation { get; private set; }
    public Vector3 CharRotation { get; private set; }

    private float xRotation = 0f;
    public InputHandler()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }
    public void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * GameConfig.MOUSE_SENSITIVITY * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * GameConfig.MOUSE_SENSITIVITY * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        GunRotation = Quaternion.Euler(xRotation, 0, 0);
        CharRotation = Vector3.up * mouseX;
    }
}
