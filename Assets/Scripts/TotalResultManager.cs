using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TotalResultManager : MonoBehaviour
{
    public GameObject startUI;
    public GameObject totalResultUI;

    //音声
    [SerializeField] private AudioSource asrc;
    [SerializeField] private AudioClip click_sound;

    private void Start()
    {
        asrc = GetComponent<AudioSource>();

    }

    IEnumerator StartButton()
    {
        //音を鳴らす処理
        asrc.PlayOneShot(click_sound);
        yield return new WaitForSeconds(0.1f);
        //シーンの遷移
        totalResultUI.SetActive(false);
        startUI.SetActive(true);
    }
    public void OnClickStart()
    {
        StartCoroutine("StartButton");
    }

}
