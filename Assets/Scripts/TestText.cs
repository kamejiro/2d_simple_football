using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestText : MonoBehaviour
{
    public Text text;
    public void OnClickTest()
    {
        GameObject foundObject = GameObject.Find("Team(Clone)");
        TeamData teamData= foundObject.GetComponent<TeamData>();
        text.text = teamData.team_name;
        Debug.Log(text.text + foundObject.name);
    }
}
