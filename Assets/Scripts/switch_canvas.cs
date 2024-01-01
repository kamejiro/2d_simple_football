using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class switch_canvas : MonoBehaviour
{
    public GameObject start_menu, result;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClick_to_Result()
    {
    start_menu.SetActive(false);
    result.SetActive(true);
    }

    public void OnClick_to_StartMenu()
    {
    start_menu.SetActive(true);
    result.SetActive(false);
    }

}
