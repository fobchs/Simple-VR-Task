using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhotoSphereRotation : MonoBehaviour
{
    public float rotateSpeed = 100.0f;
    public bool isGyroSupports = false;

    private void Awake()
    {
        if (SystemInfo.supportsGyroscope && 
            (Application.platform == RuntimePlatform.IPhonePlayer ||
            Application.platform == RuntimePlatform.Android))
        {
            isGyroSupports = true;
            Debug.Log("You are using the phone");
        } else
        {
            Debug.Log("Gyroscope is not supported!!");
            if (Application.platform != RuntimePlatform.IPhonePlayer ||
                Application.platform != RuntimePlatform.Android)
            {
                Debug.Log("You are not on the phone");
            }
        }
    }

    void Update()
    {
        if (isGyroSupports)
        {
            MovementWithGyro();
        } else
        {
            MovementWithMouse();
        }
    }

    private void MovementWithGyro()
    {
        Quaternion currentPos = new Quaternion(Input.gyro.attitude.x, Input.gyro.attitude.y,
                                         -Input.gyro.attitude.z, -Input.gyro.attitude.w);
        gameObject.transform.rotation = Quaternion.Euler(90, 0, 0) * currentPos;
    }

    private void MovementWithMouse()
    {
        if (Input.GetMouseButton(0))
        {
            float rotateX = Input.GetAxis("Mouse Y") * rotateSpeed * Time.deltaTime;
            float rotateY = -Input.GetAxis("Mouse X") * rotateSpeed * Time.deltaTime;
            gameObject.transform.Rotate(rotateX, rotateY, 0.0f);

            Vector3 currentRotation = gameObject.transform.rotation.eulerAngles;
            gameObject.transform.rotation = Quaternion.Euler(currentRotation.x, currentRotation.y, 0);
        }
    }
}