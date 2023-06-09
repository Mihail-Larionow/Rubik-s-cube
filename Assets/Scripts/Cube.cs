using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Cube : MonoBehaviour
{

    public bool isComplete = true;
    public bool isRotating = false; 
    public bool isDisabled = false;
    public bool isShuffling = false;
    public GameObject cubePiecePref;
    private Transform cubeTransf;
    private GameObject cubeCenterPiece;
    private List<GameObject> allCubePieces;
    private List<GameObject> frontCubes
    {
        get
        {
            return allCubePieces.FindAll(x => Mathf.Round(x.transform.localPosition.x) == 1);
        }
    }
    private List<GameObject> backCubes
    {
        get
        {
            return allCubePieces.FindAll(x => Mathf.Round(x.transform.localPosition.x) == -1);
        }
    }
    private List<GameObject> topCubes
    {
        get
        {
            return allCubePieces.FindAll(x => Mathf.Round(x.transform.localPosition.y) == 1);
        }
    }
    private List<GameObject> bottomCubes
    {
        get
        {
            return allCubePieces.FindAll(x => Mathf.Round(x.transform.localPosition.y) == -1);
        }
    }
    private List<GameObject> leftCubes
    {
        get
        {
            return allCubePieces.FindAll(x => Mathf.Round(x.transform.localPosition.z) == -1);
        }
    }
    private List<GameObject> rightCubes
    {
        get
        {
            return allCubePieces.FindAll(x => Mathf.Round(x.transform.localPosition.z) == 1);
        }
    }
    private List<GameObject> upHorizontalCubes
    {
        get{
            return allCubePieces.FindAll(x => Mathf.Round(x.transform.localPosition.x) == 0);
        }
    }
    private List<GameObject> upVerticalCubes
    {
        get{
            return allCubePieces.FindAll(x => Mathf.Round(x.transform.localPosition.z) == 0);
        }
    }
    private List<GameObject> frontHorizontalCubes
    {
        get{
            return allCubePieces.FindAll(x => Mathf.Round(x.transform.localPosition.y) == 0);
        }
    }
    private Vector3[] RotationVectors = {
        new Vector3(1, 0, 0), new Vector3(-1, 0, 0), new Vector3(1, 0, 0),
        new Vector3(0, 1, 0), new Vector3(0, -1, 0), new Vector3(0, 1, 0),
        new Vector3(0, 0, 1), new Vector3(0, 0, -1), new Vector3(0, 0, 1)
    };


    public void DetectRotate(List<GameObject> cubes, List<GameObject> panels)
    {
        if(isRotating || isDisabled) return;
        if(upVerticalCubes.Exists(x => x == cubes[0]) && upVerticalCubes.Exists(x => x == cubes[1]))
            StartCoroutine(Rotate(upVerticalCubes, new Vector3(0,0,1 * DetectLRSign(cubes))));
        else if(upHorizontalCubes.Exists(x => x == cubes[0]) && upHorizontalCubes.Exists(x => x == cubes[1]))
            StartCoroutine(Rotate(upHorizontalCubes, new Vector3(1 * DetectFBSign(cubes),0,0)));
        else if(frontHorizontalCubes.Exists(x => x == cubes[0]) && frontHorizontalCubes.Exists(x => x == cubes[1]))
            StartCoroutine(Rotate(frontHorizontalCubes, new Vector3(0,1 * DetectTBSign(cubes),0)));
        else if(DetectSide(panels, new Vector3(1,0,0), new Vector3(0,0,1), topCubes))
            StartCoroutine(Rotate(topCubes, new Vector3(0,1 * DetectTBSign(cubes),0)));
        else if(DetectSide(panels, new Vector3(1,0,0), new Vector3(0,0,1), bottomCubes))
            StartCoroutine(Rotate(bottomCubes, new Vector3(0,1 * DetectTBSign(cubes),0)));
        else if(DetectSide(panels, new Vector3(0,0,1), new Vector3(0,1,0), frontCubes))
            StartCoroutine(Rotate(frontCubes, new Vector3(1 * DetectFBSign(cubes),0,0)));
        else if(DetectSide(panels, new Vector3(0,0,1), new Vector3(0,1,0), backCubes))
            StartCoroutine(Rotate(backCubes, new Vector3(1 * DetectFBSign(cubes),0,0)));
        else if(DetectSide(panels, new Vector3(1,0,0), new Vector3(0,1,0), leftCubes))
            StartCoroutine(Rotate(leftCubes, new Vector3(0,0,1 * DetectLRSign(cubes))));
        else if(DetectSide(panels, new Vector3(1,0,0), new Vector3(0,1,0), rightCubes))
            StartCoroutine(Rotate(rightCubes, new Vector3(0,0,1 * DetectLRSign(cubes))));
    }

    public void CreateCube()
    {
        foreach(GameObject cube in allCubePieces){
            DestroyImmediate(cube);
        }
        allCubePieces.Clear();

        for (int x = -1; x < 2; x++)
            for (int y = -1; y < 2; y++)
                for (int z = -1; z < 2; z++)
                {
                    GameObject cube = Instantiate(cubePiecePref, cubeTransf, false);
                    cube.transform.localPosition = new Vector3(-x, -y, z);
                    cube.GetComponent<CubePiece>().SetColor(-x, -y, z);
                    allCubePieces.Add(cube);
                }
        cubeCenterPiece = allCubePieces[13];
    }

    public void StartShuffle(){
        StartCoroutine(Shuffle());
    }

    public IEnumerator Shuffle(){
        isShuffling = true;
        for(int i=0; i<Random.Range(15,40); i++){
            int edge = Random.Range(0, 9);
            List<GameObject> edgeCubes = new List<GameObject>();
            switch (edge)
            {
                case 0: edgeCubes = frontCubes; break;
                case 1: edgeCubes = backCubes; break;
                case 2: edgeCubes = upHorizontalCubes; break;
                case 3: edgeCubes = topCubes; break;
                case 4: edgeCubes = bottomCubes; break;
                case 5: edgeCubes = frontHorizontalCubes; break;
                case 6: edgeCubes = rightCubes; break;
                case 7: edgeCubes = leftCubes; break;
                case 8: edgeCubes = upVerticalCubes; break;
            }
            StartCoroutine(Rotate(edgeCubes, RotationVectors[edge], 90));
            yield return new WaitForSeconds(.3f);
        }
        isShuffling = false;
    }

    private void Start()
    {
        allCubePieces = new List<GameObject>();
        cubeTransf = transform;
        CreateCube();
    }

    private bool DetectSide(List<GameObject> panels, Vector3 fDirection, Vector3 sDirection, List<GameObject> side)
    {
        GameObject centerCube = side.Find(x => x.GetComponent<CubePiece>().panels.FindAll(y => y.activeInHierarchy).Count == 1);

        List<RaycastHit> hit1 = new  List<RaycastHit>(Physics.RaycastAll(panels[1].transform.position, fDirection)),
            hit2 = new  List<RaycastHit>(Physics.RaycastAll(panels[0].transform.position, fDirection)),
            hit1_m = new  List<RaycastHit>(Physics.RaycastAll(panels[1].transform.position, -fDirection)),
            hit2_m = new  List<RaycastHit>(Physics.RaycastAll(panels[0].transform.position, -fDirection)),

            hit3 = new  List<RaycastHit>(Physics.RaycastAll(panels[1].transform.position, sDirection)),
            hit4 = new  List<RaycastHit>(Physics.RaycastAll(panels[0].transform.position, sDirection)),
            hit3_m = new  List<RaycastHit>(Physics.RaycastAll(panels[1].transform.position, -sDirection)),
            hit4_m = new  List<RaycastHit>(Physics.RaycastAll(panels[0].transform.position, -sDirection));

        return hit1.Exists(x => x.collider.gameObject == centerCube) ||
            hit2.Exists(x => x.collider.gameObject == centerCube) ||
            hit1_m.Exists(x => x.collider.gameObject == centerCube) ||
            hit2_m.Exists(x => x.collider.gameObject == centerCube) ||

            hit3.Exists(x => x.collider.gameObject == centerCube) ||
            hit4.Exists(x => x.collider.gameObject == centerCube) ||
            hit3_m.Exists(x => x.collider.gameObject == centerCube) ||
            hit4_m.Exists(x => x.collider.gameObject == centerCube);
    }

    private float DetectLRSign(List<GameObject> cubes){
        float sign = 0;
        
        if(Mathf.Round(cubes[1].transform.position.y) != Mathf.Round(cubes[0].transform.position.y)){
            if(Mathf.Round(cubes[0].transform.position.x) == -1)
                sign = Mathf.Round(cubes[0].transform.position.y) - Mathf.Round(cubes[1].transform.position.y);
            else
                sign = Mathf.Round(cubes[1].transform.position.y) - Mathf.Round(cubes[0].transform.position.y);
        }
        else{
            if(Mathf.Round(cubes[0].transform.position.y) == -1)
                sign = Mathf.Round(cubes[1].transform.position.x) - Mathf.Round(cubes[0].transform.position.x);
            else
                sign = Mathf.Round(cubes[0].transform.position.x) - Mathf.Round(cubes[1].transform.position.x);
        }
        return sign;
    }

    private float DetectFBSign(List<GameObject> cubes){
        float sign = 0;
        
        if(Mathf.Round(cubes[1].transform.position.z) != Mathf.Round(cubes[0].transform.position.z)){
            if(Mathf.Round(cubes[0].transform.position.y) == -1)
                sign = Mathf.Round(cubes[0].transform.position.z) - Mathf.Round(cubes[1].transform.position.z);
            else
                sign = Mathf.Round(cubes[1].transform.position.z) - Mathf.Round(cubes[0].transform.position.z);
        }
        else{
            if(Mathf.Round(cubes[0].transform.position.z) == -1)
                sign = Mathf.Round(cubes[1].transform.position.y) - Mathf.Round(cubes[0].transform.position.y);
            else
                sign = Mathf.Round(cubes[0].transform.position.y) - Mathf.Round(cubes[1].transform.position.y);
        }
        return sign;
    }

    private float DetectTBSign(List<GameObject> cubes){
        float sign = 0;
        
        if(Mathf.Round(cubes[1].transform.position.z) != Mathf.Round(cubes[0].transform.position.z)){
            if(Mathf.Round(cubes[0].transform.position.x) == 1)
                sign = Mathf.Round(cubes[0].transform.position.z) - Mathf.Round(cubes[1].transform.position.z);
            else
                sign = Mathf.Round(cubes[1].transform.position.z) - Mathf.Round(cubes[0].transform.position.z);
        }
        else{
            if(Mathf.Round(cubes[0].transform.position.z) == 1)
                sign = Mathf.Round(cubes[1].transform.position.x) - Mathf.Round(cubes[0].transform.position.x);
            else
                sign = Mathf.Round(cubes[0].transform.position.x) - Mathf.Round(cubes[1].transform.position.x);
        }
        return sign;
    }

    private bool CheckComplete(){
        if(IsSideComplete(topCubes) && IsSideComplete(bottomCubes) && 
        IsSideComplete(leftCubes) && IsSideComplete(rightCubes) &&
        IsSideComplete(frontCubes) && IsSideComplete(backCubes)) 
            return true;
        else 
            return false;
    }
    
    private bool IsSideComplete(List<GameObject> cubes){
        int mainPanelIndex = cubes[4].GetComponent<CubePiece>().panels.FindIndex(x => x.activeInHierarchy);
        Color mainPanelColor = cubes[4].GetComponent<CubePiece>().panels[mainPanelIndex].GetComponent<Renderer>().material.color;
        for(int i=0; i< cubes.Count; i++){
            if(!cubes[i].GetComponent<CubePiece>().panels[mainPanelIndex].activeInHierarchy ||
            cubes[i].GetComponent<CubePiece>().panels[mainPanelIndex].GetComponent<Renderer>().material.color != mainPanelColor)
                return false;
        }
        return true;
    }

    private IEnumerator Rotate(List<GameObject> pieces, Vector3 rotation, int speed = 30)
    {
        int angle = 0;
        isRotating = true;
        while (angle < 90)
        {
            foreach (GameObject piece in pieces)
                piece.transform.RotateAround(cubeCenterPiece.transform.position, rotation, speed);
            angle += speed;
            yield return null;
        }
        isRotating = false;
        isComplete = CheckComplete();
    }

}