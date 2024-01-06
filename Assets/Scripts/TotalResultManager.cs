using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using UnityEditor.Experimental.RestService;
using System.Reflection;

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
    }

    public void ActiveResult()
    {
        //データを読み込んでテキストを更新
        totalResultText.text = "";
        TestTeamData t = new TestTeamData();
        teams = TestListMaker();
        foreach (var team in teams)
        {
            DrawText(LoadTeamData(team));
        }
    }

    //Homeボタンを押したときの動作
    IEnumerator StartAction()
    {
        //音を鳴らす処理
        asrc.PlayOneShot(click_sound);
        yield return new WaitForSeconds(0.1f);
        //シーンの遷移
        totalResultUI.SetActive(false);
        startUI.SetActive(true);
    }

    //
    IEnumerator ResetAction()
    {
        //音を鳴らす処理
        asrc.PlayOneShot(click_sound);
        yield return new WaitForSeconds(0.1f);
        //リセット機能
        Debug.Log("リセットボタンが押されました");
        teams = TestListMaker();
        foreach (var team in teams)
        {
            PlayerPrefs.SetInt(team + "WinCount", 0);
            PlayerPrefs.SetInt(team + "DrawCount", 0);
            PlayerPrefs.SetInt(team + "LoseCount", 0);
        }
        ActiveResult();
    }

    //Home画面に戻る
    public void OnClickStart()
    {
        StartCoroutine("StartAction");
    }

    //Resetボタン
    public void OnClickReset()
    {
        StartCoroutine("ResetAction");
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
        list.Add("ManchesterUnited");
        list.Add("Arsenal");
        list.Add("TottenhamHotspur");
        list.Add("Westham");
        list.Add("Brighton");
        list.Add("NewCastle");
        list.Add("Chelsea");
        list.Add("Wolves");
        list.Add("Bournemouth");
        list.Add("Fulham");
        list.Add("CrystalPalace");
        list.Add("NottinghamForest");
        list.Add("Brentford");
        list.Add("Everton");
        list.Add("Luton");
        list.Add("Burnley");
        list.Add("SheffieldUnited");

        return list;
    }
}
