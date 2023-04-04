using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    public bool isRotating = false;

    public Transform cubePosition;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RightButtonClick(){
        if(!isRotating) StartCoroutine(Rotate(new Vector3(0,1,0)));
    }

    public void LeftButtonClick(){
        if(!isRotating) StartCoroutine(Rotate(new Vector3(0,-1,0)));
    }

    IEnumerator Rotate(Vector3 rotation, int speed = 3)
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
