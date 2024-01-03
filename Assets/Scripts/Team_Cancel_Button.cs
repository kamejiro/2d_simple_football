using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Team_Cancel_Button : MonoBehaviour
{
    [SerializeField] private Dropdown dropdown;
    [SerializeField] private Dropdown dropdown2;
    public void ResetGameVaruable()
    {
        dropdown.value = 0;
        dropdown2.value = 0;
        ResetTeamObject();
    }

    public void ResetTeamObject()
    {
        TeamList teamList;
        TeamList2 teamList2;
        teamList=dropdown.GetComponent<TeamList>();
        teamList2=dropdown2.GetComponent<TeamList2>();
        teamList.DeletePlayer1();
        teamList2.DeletePlayer2();
    }


}
