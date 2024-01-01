using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartManager : MonoBehaviour
{
    public GameObject startUI;
    public GameObject gameUI;
    public GameObject resultUI;

    private void Start()
    {
        gameUI.SetActive(false);
        resultUI.SetActive(false);

    }
    public void OnClickStart()
    {
        gameUI.SetActive(true);
        startUI.SetActive(false);
    }
}
