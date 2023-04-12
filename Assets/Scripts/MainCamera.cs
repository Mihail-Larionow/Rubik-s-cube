using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MainCamera : MonoBehaviour
{
    Vector3 localRotation;
    public bool isDisabled = false;

    private void Start(){
        localRotation.x = -45;
        localRotation.y = 30;
    }

    private void LateUpdate(){
        if(Input.GetMouseButton(0)){
            if(!isDisabled){
                localRotation.x += Input.GetAxis("Mouse X") * 10;
                localRotation.y += Input.GetAxis("Mouse Y") * -10;
                localRotation.y = Mathf.Clamp(localRotation.y, -90, 90);
                Quaternion quaternion = Quaternion.Euler(localRotation.y, localRotation.x, 0);
                transform.parent.rotation = Quaternion.Lerp(transform.parent.rotation, quaternion, Time.deltaTime * 15);
            }   
        }
        else if(Input.GetMouseButtonUp(0)){
            localRotation.x = (int)(localRotation.x / 45) * 45;
            localRotation.y = 30;
            Debug.Log(localRotation.x + " " + localRotation.y);
        }
        else{
            StabilizeCamera();
        }
    }

    public void StabilizeCamera(){
        localRotation.x = (int)(localRotation.x / 45) * 45;
        Quaternion quaternion = Quaternion.Euler(30, localRotation.x, 0);
        transform.parent.rotation = Quaternion.Lerp(transform.parent.rotation, quaternion, Time.deltaTime * 1);
    }

}
