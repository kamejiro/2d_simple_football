using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using JetBrains.Annotations;
using Unity.VisualScripting.FullSerializer;

public class ButtonTest : MonoBehaviour
{
    // CSV�t�@�C���̃p�X���w��
    public string csvFilePath;

    // CSV�f�[�^���i�[���郊�X�g
    private List<List<string>> csvData = new List<List<string>>();

    public GameObject player;

    [SerializeField] Text viewText;

    public void OnClick()
    {
        // CSV�t�@�C���ǂݍ���
        ReadCSVFile();

        // �ǂݍ��񂾃f�[�^���R���\�[���ɕ\���i�f�o�b�O�p�j
        AttachCSVData();
    }
    void ReadCSVFile()
    {
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
        foreach (var row in csvData)
        {
            GameObject obj=Instantiate(player) as GameObject;
            MonsterData monsterData = obj.GetComponent<MonsterData>();
            monsterData.player_name = row[0];
            monsterData.position = row[1];
            monsterData.ATK = int.Parse(row[2]);
            monsterData.DEF = int.Parse(row[3]);
            Debug.Log(monsterData.player_name + "��hp��" + monsterData.position + "��atk��" + monsterData.ATK + "��def��" + monsterData.DEF);
            viewText.text = monsterData.player_name + "��hp��" + monsterData.position + "��atk��" + monsterData.ATK + "��def��" + monsterData.DEF;
        }
    }
}
