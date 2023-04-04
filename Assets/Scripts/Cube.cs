using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube : MonoBehaviour
{

    public GameObject cubePiecePref;
    Transform cubeTransf;
    List<GameObject> allCubePieces = new List<GameObject>();
    GameObject cubeCenterPiece;
    bool isRotating = false;
    List<GameObject> frontCubes
    {
        get
        {
            return allCubePieces.FindAll(x => Mathf.Round(x.transform.localPosition.x) == 1);
        }
    }
    List<GameObject> backCubes
    {
        get
        {
            return allCubePieces.FindAll(x => Mathf.Round(x.transform.localPosition.x) == -1);
        }
    }
    List<GameObject> topCubes
    {
        get
        {
            return allCubePieces.FindAll(x => Mathf.Round(x.transform.localPosition.y) == 1);
        }
    }
    List<GameObject> bottomCubes
    {
        get
        {
            return allCubePieces.FindAll(x => Mathf.Round(x.transform.localPosition.y) == -1);
        }
    }
    List<GameObject> leftCubes
    {
        get
        {
            return allCubePieces.FindAll(x => Mathf.Round(x.transform.localPosition.z) == -1);
        }
    }
    List<GameObject> rightCubes
    {
        get
        {
            return allCubePieces.FindAll(x => Mathf.Round(x.transform.localPosition.z) == 1);
        }
    }


    void Start()
    {
        CreateCube();
    }

    void Update()
    {
        if(!isRotating)
            CheckInput();
    }

    void CreateCube()
    {
        for (int x = -1; x < 2; x++)
            for (int y = -1; y < 2; y++)
                for (int z = -1; z < 2; z++)
                {
                    GameObject go = Instantiate(cubePiecePref, cubeTransf, false);
                    go.transform.localPosition = new Vector3(-x, -y, z);
                    go.GetComponent<CubePiece>().SetColor(-x, -y, z);
                    allCubePieces.Add(go);
                }
        cubeCenterPiece = allCubePieces[13];
    }

    void CheckInput()
    {
        if (Input.GetKeyDown(KeyCode.W))
            StartCoroutine(Rotate(topCubes, new Vector3(0, 1, 0)));
        else if (Input.GetKeyDown(KeyCode.S))
            StartCoroutine(Rotate(bottomCubes, new Vector3(0, -1, 0)));
        else if (Input.GetKeyDown(KeyCode.A))
            StartCoroutine(Rotate(leftCubes, new Vector3(0, 0, -1)));
        else if (Input.GetKeyDown(KeyCode.D))
            StartCoroutine(Rotate(rightCubes, new Vector3(0, 0, 1)));
        else if (Input.GetKeyDown(KeyCode.B))
            StartCoroutine(Rotate(backCubes, new Vector3(-1, 0, 0)));
        else if (Input.GetKeyDown(KeyCode.F))
            StartCoroutine(Rotate(frontCubes, new Vector3(-1, 0, 0)));
    }

    IEnumerator Rotate(List<GameObject> pieces, Vector3 rotation)
    {
        int angle = 0;
        isRotating = true;
        while (angle < 90)
        {
            foreach (GameObject go in pieces)
                go.transform.RotateAround(cubeCenterPiece.transform.position, rotation, 5);
            angle += 5;
            yield return null;
        }
        isRotating = false;
    }

}
