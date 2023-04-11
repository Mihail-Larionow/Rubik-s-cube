using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;

public class Game : MonoBehaviour
{

    public Cube cube;
    public Text textView;
    public MainCamera mainCamera;
    public GameObject leftButton;
    public GameObject rightButton;
    private List<GameObject> cubes;
    private List<GameObject> panels;
    private bool watchIsWorking = false;

    public void RightButtonClick(){
        if(!mainCamera.isRotating) StartCoroutine(mainCamera.CameraRotate(cube.transform, new Vector3(0,1,0)));
    }

    public void LeftButtonClick(){
        if(!mainCamera.isRotating) StartCoroutine(mainCamera.CameraRotate(cube.transform, new Vector3(0,-1,0)));
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

    public void TextViewClick(){
        if(!watchIsWorking) cube.CreateCube();
    }

    private void Start()
    {
        cubes = new List<GameObject>();
        panels = new List<GameObject>();
    }

    private void LateUpdate()
    {
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
                leftButton.SetActive(false);
                rightButton.SetActive(false);
            }
        }
        else if(Input.GetMouseButtonUp(0)){
            cubes.Clear();
            panels.Clear();
            mainCamera.isDisabled = false;
            cube.isDisabled = false;
            leftButton.SetActive(true);
            rightButton.SetActive(true);
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
