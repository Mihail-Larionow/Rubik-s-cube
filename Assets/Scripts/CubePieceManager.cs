using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubePieceManager : MonoBehaviour
{
    public GameObject topPanel, bottomPanel, frontPanel, backPanel, rightPanel, leftPanel;

    public void SetColor(int x, int y, int z)
    {
        if (x == 0) frontPanel.SetActive(true);
        else if (x == -2) backPanel.SetActive(true);
        if (y == 0) topPanel.SetActive(true);
        else if (y == -2) bottomPanel.SetActive(true);
        if (z == 0) leftPanel.SetActive(true);
        else if (z == 2) rightPanel.SetActive(true);
    }

}
