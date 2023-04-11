using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class Game : MonoBehaviour
{

    public Cube cube;
    public Text textView;
    public MainCamera mainCamera;
    private Transform cubePosition;
    private List<GameObject> cubes;
    private List<GameObject> panels;
    private bool cubeIsRotating = false;
    private bool watchIsWorking = false;

    public void RightButtonClick(){
        if(!mainCamera.isRotating) StartCoroutine(mainCamera.CameraRotate(cubePosition, new Vector3(0,1,0)));
    }

    public void LeftButtonClick(){
        if(!mainCamera.isRotating) StartCoroutine(mainCamera.CameraRotate(cubePosition, new Vector3(0,-1,0)));
    }

    public void ShuffleButtonClick(){
        if(!cube.isShuffling) StartCoroutine(cube.Shuffle());
    }

    public void StopWatchButtonClick(){
        if(watchIsWorking) {
            watchIsWorking = false;
            StopAllCoroutines();
        }
        else{ 
            watchIsWorking = true;
            StartCoroutine(TimeFlow());
        }
    }

    private void Start()
    {
        cubePosition = cube.transform;
        cubes = new List<GameObject>();
        panels = new List<GameObject>();
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

    private IEnumerator TimeFlow(){
        int seconds = -1;
        int minutes = 0;
        int hours = 0;
        textView.color = Color.white;
        while(true){
            if(cube.isComplete){
                watchIsWorking = false;
                yield break;
            }
            if(hours == 24){
                textView.text = "SLOW";
                textView.color = Color.red;
                yield break;
            }
            if(seconds == 59){
                if(minutes == 59){
                    hours++;
                    minutes = -1;
                }
                minutes++;
                seconds = -1;
            }
            seconds++;
            if(hours == 0) textView.text = minutes.ToString("D2") + ":" + seconds.ToString("D2");
            else textView.text = hours.ToString() + ":" + minutes.ToString("D2") + ":" + seconds.ToString("D2");
            yield return new WaitForSeconds(1);
        }
    }

}
