using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CubePiece : MonoBehaviour
{

    public List<GameObject> panels;

    public void SetColor(int x, int y, int z)
    {
        if (x == 1) panels[0].SetActive(true);
        else if (x == -1) panels[1].SetActive(true);
        if (y == 1) panels[2].SetActive(true);
        else if (y == -1) panels[3].SetActive(true);
        if (z == -1) panels[4].SetActive(true);
        else if (z == 1) panels[5].SetActive(true);
    }

}
