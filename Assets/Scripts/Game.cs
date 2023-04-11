using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Game : MonoBehaviour
{

    public Cube cube;
    public Text textView;
    public Transform cubePosition;
    private List<GameObject> cubes;
    private List<GameObject> panels;
    private bool isRotating = false;
    private bool cubeIsRotating = false;
    private bool watchIsWorking = false;

    public void RightButtonClick(){
        if(!isRotating) StartCoroutine(CameraRotate(new Vector3(0,1,0)));
    }

    public void LeftButtonClick(){
        if(!isRotating) StartCoroutine(CameraRotate(new Vector3(0,-1,0)));
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
        int second = -1;
        int minute = 0;
        int hour = 0;
        while(true){
            if(cube.isComplete){
                watchIsWorking = false;
                yield break;
            }
            if(second == 59){
                if(minute == 59){
                    hour++;
                    minute = -1;
                }
                minute++;
                second = -1;
            }
            second++;
            if(hour == 0) textView.text = minute.ToString("D2") + ":" + second.ToString("D2");
            else textView.text = hour.ToString() + ":" + minute.ToString("D2") + ":" + second.ToString("D2");
            yield return new WaitForSeconds(1);
        }
    }

    private IEnumerator CameraRotate(Vector3 rotation, int speed = 30)
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
