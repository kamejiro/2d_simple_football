using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class TeamList2 : MonoBehaviour
{
    // CSV�t�@�C���̃p�X���w��
    private string csvFilePath;

    // CSV�f�[�^���i�[���郊�X�g
    private List<List<string>> csvData = new List<List<string>>();

    public GameObject player;
    public GameObject team;

    [SerializeField] private Dropdown dropdown;
    [SerializeField] Text viewText;
    [SerializeField] Text viewText2;
    private List<GameObject> playerList = new List<GameObject>();
    private int valueChangeCount = 0;
    private void Start()
    {

        viewText.text = "";
        viewText2.text = "";
        dropdown.onValueChanged.AddListener(ValueChanged);
    }

    public void OnClickDropDown()
    {
        csvFilePath = "assets/scripts/PlayerData/" + dropdown.options[dropdown.value].text + ".csv";
        Debug.Log(csvFilePath);

        // CSV�t�@�C���ǂݍ���
        ReadCSVFile();

        // �ǂݍ��񂾃f�[�^���R���\�[���ɕ\���i�f�o�b�O�p�j
        playerList.Clear();
        Debug.Log(playerList.Count);
        AttachCSVData();
    }

    void ReadCSVFile()
    {
        if(csvData.Count > 0)
        {
            csvData.Clear();
        }
        // CSV�t�@�C�������݂��邩�m�F
        if (File.Exists(csvFilePath))
        {
            // CSV�t�@�C����ǂݍ���
            using (StreamReader reader = new StreamReader(csvFilePath))
            {
                while (!reader.EndOfStream)
                {
                    // ��s�ǂݍ��݁A�J���}�ŕ������ă��X�g�ɒǉ�
                    string line = reader.ReadLine();
                    List<string> row = new List<string>(line.Split(','));

                    // �ǂݍ��񂾍s�����X�g�ɒǉ�
                    csvData.Add(row);
                }
            }
        }
        else
        {
            Debug.LogError("CSV�t�@�C����������܂���: " + csvFilePath);
        }
    }

    void AttachCSVData()
    {
        if (playerList.Count > 0)
        {
            playerList.Clear();
        }
        GameObject obj2 = Instantiate(team) as GameObject;
        playerList.Add(obj2);
        TeamData teamData = obj2.GetComponent<TeamData>();
        teamData.team_name = dropdown.options[dropdown.value].text;
        teamData.name = dropdown.options[dropdown.value].text;
        teamData.score = 0;
        teamData.Total_ATK = 0;
        teamData.Total_DEF = 0;

        foreach (var row in csvData)
        {
            GameObject obj = Instantiate(player) as GameObject;
            playerList.Add(obj);
            MonsterData monsterData = obj.GetComponent<MonsterData>();
            monsterData.player_name = row[0];
            monsterData.name = row[0];
            monsterData.position = row[1];
            monsterData.ATK = int.Parse(row[2]);
            monsterData.DEF = int.Parse(row[3]);

            teamData.Total_ATK += int.Parse(row[2]);
            teamData.Total_DEF += int.Parse(row[3]);

            //�v���C���[�o��
            Debug.Log(monsterData.player_name + "��pos��" + monsterData.position + "��atk��" + monsterData.ATK + "��def��" + monsterData.DEF);
            viewText.text += monsterData.position + " " + monsterData.player_name + "\n";
        }
        //�`�[���o��
        Debug.Log(teamData.team_name + "��score��" + teamData.score + "��atk��" + teamData.Total_ATK + "��def��" + teamData.Total_DEF);
        viewText2.text = teamData.team_name;

        //gameUIで計算するために値代入
        Calc_Deta calc_deta = GetComponent<Calc_Deta>();
        calc_deta.AP2=teamData.Total_ATK;
        calc_deta.DP2=teamData.Total_DEF;
    }

    private void ValueChanged(int value)
    {
        valueChangeCount++;
        if (value >= 2)
        {
            foreach (var player in playerList)
            {
                Destroy(player);
            }
            valueChangeCount = 0;
        }
        Debug.Log("値が変わりました");
    }

}
