using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class TeamList2 : MonoBehaviour
{
    //CSVファイルへのパス
    private string csvFilePath;

    //ファイルを読み込む際に使用するリスト
    private List<List<string>> csvData = new List<List<string>>();

    //プレハブ
    public GameObject player;
    public GameObject team;

    //ドロップダウンとテキスト
    [SerializeField] private Dropdown dropdown;
    [SerializeField] Text viewText;
    [SerializeField] Text viewText2;
    private void Start()
    {
        viewText.text = "";
        viewText2.text = "";
    }

    public void OnClickDropDown()
    {
        csvFilePath = "assets/scripts/PlayerData/" + dropdown.options[dropdown.value].text + ".csv";
        Debug.Log(csvFilePath);

        ReadCSVFile();

        AttachCSVData();
    }

    //CSVファイルを読み込む
    void ReadCSVFile()
    {
        csvData.Clear();
        //ファイルが存在したら
        if (File.Exists(csvFilePath))
        {
            //1行ずつ読み込む
            using (StreamReader reader = new StreamReader(csvFilePath))
            {
                while (!reader.EndOfStream)
                {
                    //コンマ、行ごとにリストに追加
                    string line = reader.ReadLine();
                    List<string> row = new List<string>(line.Split(','));
                    csvData.Add(row);
                }
            }
        }
        else
        {
            Debug.LogError("CSVファイルの読み込みに失敗しました" + csvFilePath);
        }
    }

    //オブジェクトにCSVファイルを割り当てる
    void AttachCSVData()
    {
        DeletePlayer2();
        viewText.text = "";
        viewText2.text = "";
        GameObject obj2 = Instantiate(team) as GameObject;
        TeamData teamData = obj2.GetComponent<TeamData>();
        teamData.team_name = dropdown.options[dropdown.value].text;
        teamData.gameObject.tag = "team2";
        teamData.name = dropdown.options[dropdown.value].text;
        teamData.score = 0;
        teamData.Total_ATK = 0;
        teamData.Total_DEF = 0;

        foreach (var row in csvData)
        {
            GameObject obj = Instantiate(player) as GameObject;
            MonsterData monsterData = obj.GetComponent<MonsterData>();
            monsterData.gameObject.tag = "player2";
            monsterData.player_name = row[0];
            monsterData.name = row[0];
            monsterData.position = row[1];
            monsterData.ATK = int.Parse(row[2]);
            monsterData.DEF = int.Parse(row[3]);

            teamData.Total_ATK += int.Parse(row[2]);
            teamData.Total_DEF += int.Parse(row[3]);

            //選手ログの出力
            Debug.Log(monsterData.player_name + "のposは" + monsterData.position + "のatkは" + monsterData.ATK + "のdefは" + monsterData.DEF);
            viewText.text += monsterData.position + " " + monsterData.player_name + "\n";
        }
        //チームログの出力
        Debug.Log(teamData.team_name + "のscoreは" + teamData.score + "のatkは" + teamData.Total_ATK + "のdefは" + teamData.Total_DEF);
        viewText2.text = teamData.team_name;

        //gameUIで計算するために値代入
        Calc_Deta calc_deta = GetComponent<Calc_Deta>();
        calc_deta.AP2=teamData.Total_ATK;
        calc_deta.DP2=teamData.Total_DEF;
    }

    //選手、チームオブジェクトの初期化
    public void DeletePlayer2()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("player2");
        GameObject delete_team = GameObject.FindGameObjectWithTag("team2");
        Destroy(delete_team);
        foreach (GameObject player in players)
        {
            Destroy(player);
        }
    }

}
