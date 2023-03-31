using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeManager : MonoBehaviour
{

    public GameObject CubePiecePref;
    Transform CubeTransf;

    void Start()
    {
        CreateCube();
    }

    void Update()
    {
        
    }

    void CreateCube()
    {
        for (int x = 0; x < 3; x++)
            for (int y = 0; y < 3; y++)
                for (int z = 0; z < 3; z++)
                {
                    GameObject go = Instantiate(CubePiecePref, CubeTransf, false);
                    go.transform.localPosition = new Vector3(-x, -y, z);
                }

    }

}
