using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeManager : MonoBehaviour
{

    public GameObject CubePiecePref;
    Transform CubeTransf;
    List<GameObject> AllCubePieces = new List<GameObject>();
    GameObject CubeCenterPiece;
    bool isRotating = false;
    List<GameObject> frontCubes
    {
        get
        {
            return AllCubePieces.FindAll(x => Mathf.Round(x.transform.localPosition.x) == 0);
        }
    }
    List<GameObject> backCubes
    {
        get
        {
            return AllCubePieces.FindAll(x => Mathf.Round(x.transform.localPosition.x) == -2);
        }
    }
    List<GameObject> topCubes
    {
        get
        {
            return AllCubePieces.FindAll(x => Mathf.Round(x.transform.localPosition.y) == 0);
        }
    }
    List<GameObject> bottomCubes
    {
        get
        {
            return AllCubePieces.FindAll(x => Mathf.Round(x.transform.localPosition.y) == -2);
        }
    }
    List<GameObject> leftCubes
    {
        get
        {
            return AllCubePieces.FindAll(x => Mathf.Round(x.transform.localPosition.z) == 0);
        }
    }
    List<GameObject> rightCubes
    {
        get
        {
            return AllCubePieces.FindAll(x => Mathf.Round(x.transform.localPosition.z) == 2);
        }
    }


    void Start()
    {
        CreateCube();
    }

    void Update()
    {
        CheckInput();
    }

    void CreateCube()
    {
        for (int x = 0; x < 3; x++)
            for (int y = 0; y < 3; y++)
                for (int z = 0; z < 3; z++)
                {
                    GameObject go = Instantiate(CubePiecePref, CubeTransf, false);
                    go.transform.localPosition = new Vector3(-x, -y, z);
                    go.GetComponent<CubePieceManager>().SetColor(-x, -y, z);
                    AllCubePieces.Add(go);
                }
        CubeCenterPiece = AllCubePieces[13];
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
                go.transform.RotateAround(CubeCenterPiece.transform.position, rotation, 5);
            angle += 5;
            yield return null;
        }
        isRotating = false;
    }

}
