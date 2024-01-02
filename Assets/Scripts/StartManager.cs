using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartManager : MonoBehaviour
{
    public GameObject startUI;
    public GameObject gameUI;
    public GameObject resultUI;
    private AudioSource asrc;

    private void Start()
    {
        gameUI.SetActive(false);
        resultUI.SetActive(false);
        asrc = GetComponent<AudioSource>();

    }

    IEnumerator StartButton()
    {
        //����炷����
        asrc.PlayOneShot(asrc.clip);
        yield return new WaitForSeconds(0.1f);
        //�V�[���̑J��
        gameUI.SetActive(true);
        startUI.SetActive(false);
    }
    public void OnClickStart()
    {
        StartCoroutine("StartButton");
    }


}
