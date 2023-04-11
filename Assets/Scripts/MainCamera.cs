using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MainCamera : MonoBehaviour
{

    public bool isRotating = false;
    
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

}
