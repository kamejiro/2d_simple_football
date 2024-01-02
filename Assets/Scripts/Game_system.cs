using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Game_system : MonoBehaviour
{
    public Text gametime,gamescore,nextButton;
    public GameObject gameUI, result_menu;
    public RectTransform soccer_ball;

    //初期条件
    public int TOTAL_STEP = 15;

    //変数
    public int turn;//0がteam1、1がteam2
    public int temp_step;
    public int bp;//ball pointの略。1～6で示す。
    public int score1, score2;//team1、team2のスコア
    public int ap1, dp1;//team1の攻撃、守備ポイント
    public int ap2, dp2;//team2の攻撃、守備ポイント


 

    // Start is called before the first frame update
    void Start()
    {
        //テスト用の値代入
        turn=0;
        temp_step=0;
        bp=3;
        score1=0;
        score2=0;
        ap1=100;
        dp1=100;
        ap2=100;
        dp2=100;
    }

    public void ballPosi(int __bollpositon){
    //ボールの処理
        int temp_bp=(__bollpositon *140)-450;
        soccer_ball.localPosition =new Vector3(temp_bp, -20,0);
    }

    // 一定時間待機するコルーチン
    IEnumerator GoalTeam1()
    {
        yield return new WaitForSeconds(1.0f);
        score1 =++score1;
        bp = 4;
        turn =1;
        ballPosi(bp);
        Debug.Log("Finished Waiting");
        string temp_score1=score1.ToString();
        string temp_score2=score2.ToString();
        //得点表示への反映
        gamescore.text= temp_score1 + " - " + temp_score2;
    }

    IEnumerator GoalTeam2()
    {
        yield return new WaitForSeconds(1.0f);
        score2 =++score2;
        bp = 3;
        turn =0;
        Debug.Log("Team1のゴール!!");
        ballPosi(bp);
        string temp_score1=score1.ToString();
        string temp_score2=score2.ToString();
        //得点表示への反映
        gamescore.text= temp_score1 + " - " + temp_score2;
    }

    public int step_calc(int bp,int ap1,int dp1,int ap2,int dp2){ 
        
        //攻撃/(攻撃+守備)の処理
        double num_for_calc1;
        int num_for_calc2;
        if (turn==0){
            num_for_calc1=ap1*100/ (ap1+dp2);
            num_for_calc2=(int)num_for_calc1;
        }
        else{
            num_for_calc1=ap2*100/ (ap2+dp1);
            num_for_calc2=(int)num_for_calc1;
        }    
        System.Random random = new System.Random();
        int randomNumber = random.Next(100);
        int result = (randomNumber < num_for_calc2) ? 1 : -1;
        bp = bp+result;
        ballPosi(bp);


        //攻守交替の処理
        if (turn==0){
            if(result==-1){
                turn=1;
            }
        }
        if (turn==1){
            if(result==1){
                turn=0;
            }
        }


        //得点処理
        if (bp == 6){
            StartCoroutine(GoalTeam1());

        }
        else if(bp==1){
            score2 =++score2;
            bp = 3;
            turn =0;
            Debug.Log("Team2のゴール!!");
            ballPosi(bp);
        }
        return bp;
    }

    public void STEP_run()
    {
        if (temp_step<15){
        bp=step_calc(bp, ap1, dp1, ap2, dp2);
        temp_step=++temp_step;
        string temp_time=(temp_step*6).ToString();

        gametime.text= temp_time+ "分経過";

        }
        else if (temp_step==15){
        temp_step=++temp_step;
        gametime.text= "試合終了！";
        nextButton.text="結果へ";
        }
        else{
        Debug.Log("結果表示");
        gameUI.SetActive(false);
        result_menu.SetActive(true);
        }
    }

}
