using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MainCamera : MonoBehaviour
{
    Vector3 localRotation;
    public bool isRotating = false;
    public bool isDisabled = false;

    public IEnumerator CameraRotate(Transform cubePosition, Vector3 rotation, int speed = 30)
    {
        isRotating = true;
        int angle = 0;
        while(angle < 90){
            transform.RotateAround(cubePosition.position, rotation, speed);
            angle += speed;
            yield return null;
        }
        isRotating = false;
    }

    private void LateUpdate(){
        if(Input.GetMouseButton(0)){
            if(!isDisabled){
                localRotation.x += Input.GetAxis("Mouse X") * 10;
                localRotation.y += Input.GetAxis("Mouse Y") * -10;
                localRotation.y = Mathf.Clamp(localRotation.y, -90, 90);
            }   
        }
        
        Quaternion quaternion = Quaternion.Euler(localRotation.y, localRotation.x, 0);
        transform.parent.rotation = Quaternion.Lerp(transform.parent.rotation, quaternion, Time.deltaTime * 10);
    }
}
