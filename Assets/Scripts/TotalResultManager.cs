using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using UnityEditor.Experimental.RestService;

public class TotalResultManager : MonoBehaviour
{
    public class TestTeamData
    {
        public string teamName;
        public int winCount;
        public int drawCount;
        public int loseCount;
    }

    //ハッシュキー
    public List<string> teams;
    public const string WinCount = "WinCount";
    public const string DrawCount = "DrawCount";
    public const string LoseCount = "LoseCount";

    //画面
    [SerializeField] GameObject startUI;
    [SerializeField] GameObject totalResultUI;
    
    //テキスト
    [SerializeField] Text totalResultText;

    //音声
    [SerializeField] private AudioSource asrc;
    [SerializeField] private AudioClip click_sound;

    private void Start()
    {
        //音声の割り当て
        asrc = GetComponent<AudioSource>();

        //データを読み込んでテキストを更新
        TestTeamData t = new TestTeamData();
        teams = TestListMaker();
        foreach (var team in teams)
        {
            Debug.Log(team);
            DrawText(LoadTeamData(team));
        }

    }

    IEnumerator StartAction()
    {
        //音を鳴らす処理
        asrc.PlayOneShot(click_sound);
        yield return new WaitForSeconds(0.1f);
        //シーンの遷移
        totalResultUI.SetActive(false);
        startUI.SetActive(true);
    }

    //Home画面に戻る
    public void OnClickStart()
    {
        StartCoroutine("StartAction");
    }

    //テキストの読み込み
    public static TestTeamData LoadTeamData(string s)
    {
        TestTeamData t= new TestTeamData();
        t.teamName = s;
        t.winCount = PlayerPrefs.GetInt(s + WinCount, 0);
        t.drawCount = PlayerPrefs.GetInt(s + DrawCount, 0);
        t.loseCount = PlayerPrefs.GetInt(s + LoseCount, 0);

        return t;
    }

    //テキストの生成
    public void DrawText(TestTeamData t)
    {
        totalResultText.text+= "\n"+ t.teamName +" " + t.winCount + "勝 "+ t.drawCount + "分 "+ t.loseCount + "敗";
    }

    public List<string> TestListMaker()
    {
        //ファイルの読み込み
        List<string> list = new List<string>();
        list.Add("Liverpool");
        list.Add("ManchesterCity");
        list.Add("AstonVilla");

        return list;
    }
}
