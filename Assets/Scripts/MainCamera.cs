using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
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
        if(Input.GetMouseButton(0) && !EventSystem.current.IsPointerOverGameObject()){
            if(!isDisabled){
                localRotation.x += Input.GetAxis("Mouse X") * 5;
                localRotation.y += Input.GetAxis("Mouse Y") * -5;
                localRotation.y = Mathf.Clamp(localRotation.y, -90, 90);
                Quaternion quaternion = Quaternion.Euler(localRotation.y, localRotation.x, 0);
                transform.parent.rotation = Quaternion.Lerp(transform.parent.rotation, quaternion, Time.deltaTime * 5);
            }   
        }
        else if(Input.GetMouseButtonUp(0)){
            localRotation.x = (int)(localRotation.x / 45) * 45;
            localRotation.y = 30;
        }
        else{
            StabilizeCamera();
        }
    }

    public void StabilizeCamera(){
        localRotation.x = (int)(localRotation.x / 45) * 45;
        Quaternion quaternion = Quaternion.Euler(30, localRotation.x, 0);
        transform.parent.rotation = Quaternion.Lerp(transform.parent.rotation, quaternion, Time.deltaTime * 5);
    }

}
