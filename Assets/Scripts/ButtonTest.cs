using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using JetBrains.Annotations;
using Unity.VisualScripting.FullSerializer;

public class ButtonTest : MonoBehaviour
{
    // CSVファイルのパスを指定
    public string csvFilePath;

    // CSVデータを格納するリスト
    private List<List<string>> csvData = new List<List<string>>();

    public GameObject player;
    public GameObject team;

    [SerializeField] Text viewText;
    [SerializeField] Text viewText2;

    public void OnClick()
    {
        // CSVファイル読み込み
        ReadCSVFile();

        // 読み込んだデータをコンソールに表示（デバッグ用）
        AttachCSVData();
    }
    void ReadCSVFile()
    {
        // CSVファイルが存在するか確認
        if (File.Exists(csvFilePath))
        {
            // CSVファイルを読み込み
            using (StreamReader reader = new StreamReader(csvFilePath))
            {
                while (!reader.EndOfStream)
                {
                    // 一行読み込み、カンマで分割してリストに追加
                    string line = reader.ReadLine();
                    List<string> row = new List<string>(line.Split(','));

                    // 読み込んだ行をリストに追加
                    csvData.Add(row);
                }
            }
        }
        else
        {
            Debug.LogError("CSVファイルが見つかりません: " + csvFilePath);
        }
    }

    void AttachCSVData()
    {
        //チームデータの入力
        GameObject obj2 = Instantiate(team) as GameObject;
        TeamData teamData = obj2.GetComponent<TeamData>();
        teamData.team_name = "Liverpool";
        teamData.score = 0;
        teamData.Total_ATK = 0;
        teamData.Total_DEF = 0;

        foreach (var row in csvData)
        {
            //プレイヤーデータの入力
            GameObject obj=Instantiate(player) as GameObject;
            MonsterData monsterData = obj.GetComponent<MonsterData>();
            monsterData.player_name = row[0];
            monsterData.position = row[1];
            monsterData.ATK = int.Parse(row[2]);
            monsterData.DEF = int.Parse(row[3]);

            teamData.Total_ATK += int.Parse(row[2]);
            teamData.Total_DEF += int.Parse(row[3]);

            //プレイヤー出力
            Debug.Log(monsterData.player_name + "のposは" + monsterData.position + "のatkは" + monsterData.ATK + "のdefは" + monsterData.DEF);
            viewText.text += monsterData.position + " " + monsterData.player_name + "\n";
        }
        //チーム出力
        Debug.Log(teamData.team_name + "のscoreは" + teamData.score + "のatkは" + teamData.Total_ATK + "のdefは" + teamData.Total_DEF);
        viewText2.text += teamData.team_name + " " + teamData.score;
    }
}
