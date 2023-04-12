using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class Game : MonoBehaviour
{

    public Cube cube;
    public MainCamera mainCamera;
    public StopWatch stopWatch;
    private List<GameObject> cubes;
    private List<GameObject> panels;

    public void ShuffleButtonClick(){
        if(!cube.isShuffling && !stopWatch.isWorking) cube.StartShuffle();
    }

    public void StopWatchButtonClick(){
        if(!stopWatch.isWorking && !cube.isShuffling) {
            stopWatch.StartTimeFlow(cube);
        }
        else if(stopWatch.isWorking){ 
            stopWatch.StopTimeFlow();
        }
    }

    public void TextViewClick(){
        if(!stopWatch.isWorking) cube.CreateCube();
    }

    private void Start()
    {
        cubes = new List<GameObject>();
        panels = new List<GameObject>();
    }

    private void LateUpdate()
    {
        if(stopWatch.downCounting) cube.isDisabled = true;
        if(Input.GetMouseButton(0) && !EventSystem.current.IsPointerOverGameObject()){
            if(!cube.isDisabled){
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                if(Physics.Raycast(ray, out hit, 100)){
                    mainCamera.isDisabled = true;

                    if(cubes.Count < 2 && 
                    !cubes.Exists(x => x == hit.collider.transform.parent.gameObject) &&
                    hit.transform.parent.gameObject != cube.gameObject){
                        cubes.Add(hit.collider.transform.parent.gameObject);
                        panels.Add(hit.collider.gameObject);
                    }
                    else if(cubes.Count == 2){
                        cube.DetectRotate(cubes, panels);
                        cube.isDisabled = true;
                    }
                }
            }

            if(!mainCamera.isDisabled){
                cube.isDisabled = true;
            }
        }
        else if(Input.GetMouseButtonUp(0)){
            cubes.Clear();
            panels.Clear();
            mainCamera.isDisabled = false;
            cube.isDisabled = false;
        }
    }

    

}
