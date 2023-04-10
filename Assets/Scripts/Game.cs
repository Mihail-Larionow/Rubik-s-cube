using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{

    public Cube cube;
    public bool isRotating = false;
    public Transform cubePosition;
    private bool cubeIsRotating;
    private List<GameObject> cubes;
    private List<GameObject> panels;

    public void RightButtonClick(){
        if(!isRotating) StartCoroutine(Rotate(new Vector3(0,1,0)));
    }

    public void LeftButtonClick(){
        if(!isRotating) StartCoroutine(Rotate(new Vector3(0,-1,0)));
    }

    private void Start()
    {
        cubes = new List<GameObject>();
        panels = new List<GameObject>();
        cubeIsRotating = false;
    }

    private void LateUpdate()
    {
        if(Input.GetMouseButton(0)){

            if(!cubeIsRotating){
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                if(Physics.Raycast(ray, out hit, 100)){
                    if(cubes.Count < 2 && 
                    !cubes.Exists(x => x == hit.collider.transform.parent.gameObject) &&
                    hit.transform.parent.gameObject != cube.gameObject){
                        cubes.Add(hit.collider.transform.parent.gameObject);
                        panels.Add(hit.collider.gameObject);
                    }
                    else if(cubes.Count == 2){
                        cube.DetectRotate(cubes, panels);
                        cubeIsRotating = true;
                    }
                }
            }
        }
        else if(Input.GetMouseButtonUp(0)){
            cubes.Clear();
            panels.Clear();
            cubeIsRotating = false;
        }
    }

    private IEnumerator Rotate(Vector3 rotation, int speed = 30)
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
