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

    [SerializeField] Text viewText;

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
        foreach (var row in csvData)
        {
            GameObject obj=Instantiate(player) as GameObject;
            MonsterData monsterData = obj.GetComponent<MonsterData>();
            monsterData.player_name = row[0];
            monsterData.position = row[1];
            monsterData.ATK = int.Parse(row[2]);
            monsterData.DEF = int.Parse(row[3]);
            Debug.Log(monsterData.player_name + "のhpは" + monsterData.position + "のatkは" + monsterData.ATK + "のdefは" + monsterData.DEF);
            viewText.text = monsterData.player_name + "のhpは" + monsterData.position + "のatkは" + monsterData.ATK + "のdefは" + monsterData.DEF;
        }
    }
}
