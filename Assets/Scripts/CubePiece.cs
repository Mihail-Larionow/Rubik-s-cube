using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubePiece : MonoBehaviour
{
    public GameObject topPanel, bottomPanel, frontPanel, backPanel, rightPanel, leftPanel;

    public void SetColor(int x, int y, int z)
    {
        if (x == 1) frontPanel.SetActive(true);
        else if (x == -1) backPanel.SetActive(true);
        if (y == 1) topPanel.SetActive(true);
        else if (y == -1) bottomPanel.SetActive(true);
        if (z == -1) leftPanel.SetActive(true);
        else if (z == 1) rightPanel.SetActive(true);
    }

}
