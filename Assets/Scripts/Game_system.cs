using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UI;

public class Game_system : MonoBehaviour
{
    public Text gametime,gamescore,nextButton_text,gamescore_resultMenu,holderName;
    public Button nextButton, attackButton, defenseButton;
    public GameObject gameUI, result_menu;
    public RectTransform soccer_ball;
    public Image goalCelebration;

    //初期条件
    public int TOTAL_STEP = 15;

    //変数
    public int turn;//0がteam1、1がteam2
    public int temp_step;
    public int bp;//ball pointの略。1～6で示す。
    public int score1, score2;//team1、team2のスコア
    public int ap1, dp1;//team1の攻撃、守備ポイント
    public int ap2, dp2;//team2の攻撃、守備ポイント

    //結果表示に使うチームテキスト
    public Text teamName;
    public Text teamName2;
    public Text teamName3;
    public Text teamName4;
    public Text teamText;
    public Text teamText2;
    public Text teamText3;
    public Text teamText4;

    //音声
    [SerializeField]private AudioSource asrc;
    [SerializeField]private AudioClip click_sound;
    [SerializeField]private AudioClip goal_sound;
    [SerializeField]private AudioClip result_sound;

    // Start is called before the first frame update
    void Start()
    {
        //テスト用の値代入
        turn=0;
        temp_step=0;
        bp=3;
        score1=0;
        score2=0;
        goalCelebration.enabled = false;

        ap1 = 100;
        dp1 = 100;
        ap2 = 100;
        dp2 = 100;

        attackButton.interactable = false;
        defenseButton.interactable = false;
    }

    public void ballPosi(int __bollpositon){
    //ボールの処理
        int temp_bp=(__bollpositon *140)-450;
        soccer_ball.localPosition =new Vector3(temp_bp, -20,0);
    }

    // 一定時間待機するコルーチン
    IEnumerator GoalTeam1()
    {
        goalCelebration.enabled = true;
        asrc.PlayOneShot(goal_sound);
        yield return new WaitForSeconds(1.0f);
        goalCelebration.enabled = false;
        score1 =++score1;
        bp = 4;
        turn =1;
        ballPosi(bp);
        Debug.Log("Finished Waiting");
        string temp_score1=score1.ToString();
        string temp_score2=score2.ToString();
        //得点表示への反映
        gamescore.text= temp_score1 + " - " + temp_score2;
        nextButton.interactable = true;

    }

    IEnumerator GoalTeam2()
    {
        goalCelebration.enabled = true;
        asrc.PlayOneShot(goal_sound);
        yield return new WaitForSeconds(1.0f);
        goalCelebration.enabled = false;
        score2 =++score2;
        bp = 3;
        turn =0;
        Debug.Log("Team1のゴール!!");
        ballPosi(bp);
        string temp_score1=score1.ToString();
        string temp_score2=score2.ToString();
        //得点表示への反映
        gamescore.text= temp_score1 + " - " + temp_score2;
        nextButton.interactable = true;

    }

    IEnumerator ClickSound()
    {
        asrc.PlayOneShot(click_sound);
        yield return new WaitForSeconds(0.1f);
    }
    IEnumerator ResultSound()
    {
        asrc.PlayOneShot(result_sound);
        yield return new WaitForSeconds(0.1f);
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
            nextButton.interactable = false;
            StartCoroutine(GoalTeam1());

        }
        else if(bp==1){
            nextButton.interactable = false;
            StartCoroutine(GoalTeam2());
        }
        return bp;
    }


    public void Apdp_Input(){

        Calc_Deta calc_Deta;
        GameObject obj = transform.parent.gameObject;
        calc_Deta = obj.GetComponent<Calc_Deta>();
        //GameObject obj = transform.parent.gameObject;
        //Calc_Deta calc_deta = GetComponent<Calc_Deta>();

        if (calc_Deta != null)
        {
            ap1 = calc_Deta.AP1;
            dp1 = calc_Deta.DP1;
            ap2 = calc_Deta.AP2;
            dp2 = calc_Deta.DP2;
            Debug.Log(ap1 + " , "+ dp1 + " , "+ ap2 + " , "+ dp2 + " , "); 
        }
        else{
            Debug.Log("not found.");
        }
    }


    public void STEP_run()
    {
        StartCoroutine(ClickSound());
        
        if (temp_step==0){
            Apdp_Input();
            attackButton.interactable = true;
            defenseButton.interactable = true;      
        }

        if (temp_step<15){
        bp=step_calc(bp, ap1, dp1, ap2, dp2);
        temp_step=++temp_step;
        string temp_time=(temp_step*6).ToString();

        gametime.text= temp_time+ "分経過";

        }
        else if (temp_step==15){
            StartCoroutine(ResultSound());
            temp_step=++temp_step;
            gametime.text= "試合終了！";
            nextButton_text.text="結果へ";
        }
        else{
            string temp_score1=score1.ToString();
            string temp_score2=score2.ToString();
            //得点表示への反映
            gamescore_resultMenu.text= temp_score1 + " - " + temp_score2;
            Debug.Log("結果表示");
            JudgeGameResult();
            gameUI.SetActive(false);
            result_menu.SetActive(true);
            ResetGameVaruable();
            DrawResultMember();
        }

        holderName.text=Ball_Holder();
    }

    public void STEP_Atack_Run()
    {
        StartCoroutine(ClickSound());
        
        ap1 = (int)Mathf.Round(ap1 * 1.1f);
        dp1 = (int)Mathf.Round(dp1 * 0.8f);

        if (temp_step<15){
        bp=step_calc(bp, ap1, dp1, ap2, dp2);
        temp_step=++temp_step;
        string temp_time=(temp_step*6).ToString();

        gametime.text= temp_time+ "分経過";

        }
        else if (temp_step==15){
            StartCoroutine(ResultSound());
            temp_step=++temp_step;
            gametime.text= "試合終了！";
            nextButton_text.text="結果へ";
        }
        else{
            string temp_score1=score1.ToString();
            string temp_score2=score2.ToString();
            //得点表示への反映
            gamescore_resultMenu.text= temp_score1 + " - " + temp_score2;
            Debug.Log("結果表示");
            JudgeGameResult();
            gameUI.SetActive(false);
            result_menu.SetActive(true);
            ResetGameVaruable();
            DrawResultMember();
        }
    }

    public void STEP_Deffense_Run()
    {
        StartCoroutine(ClickSound());
        
        ap1 = (int)Mathf.Round(ap1 * 0.8f);
        dp1 = (int)Mathf.Round(dp1 * 1.1f);
        
        if (temp_step<15){
        bp=step_calc(bp, ap1, dp1, ap2, dp2);
        temp_step=++temp_step;
        string temp_time=(temp_step*6).ToString();

        gametime.text= temp_time+ "分経過";

        }
        else if (temp_step==15){
            StartCoroutine(ResultSound());
            temp_step=++temp_step;
            gametime.text= "試合終了！";
            nextButton_text.text="結果へ";
        }
        else
        {
            string temp_score1=score1.ToString();
            string temp_score2=score2.ToString();
            //得点表示への反映
            gamescore_resultMenu.text= temp_score1 + " - " + temp_score2;
            Debug.Log("結果表示");
            JudgeGameResult();
            gameUI.SetActive(false);
            result_menu.SetActive(true);
            ResetGameVaruable();
            DrawResultMember();
        }
    }

    public string Ball_Holder(){
        string holderName;
        GameObject obj = transform.parent.gameObject;
        Calc_Deta definedeta = obj.GetComponent<Calc_Deta>();
        int holder_index=1;//1～11がteam1のGK～ST、12～22がTeam2のGK～ST
        if (turn==1){
            holder_index=11;
        }
        //1～100乱数発生
        System.Random random = new System.Random();
        int randomNumber = random.Next(100);
        int quotient = randomNumber / 10;//商
        int remainder = randomNumber % 10;
        Debug.Log("乱数の商は"+quotient + ",余りは"+remainder);
        holder_index=holder_index+quotient;
        holderName= definedeta[holder_index,0];
        return holderName;
    }





    public void DrawResultMember()
    {
        teamName.text = teamName2.text;
        teamName3.text = teamName4.text;
        teamText.text = teamText2.text;
        teamText3.text = teamText4.text;
    }

    public void ResetGameVaruable()
    {
        turn = 0;
        temp_step = 0;
        bp = 3;
        score1 = 0;
        score2 = 0;
        gametime.text = "試合開始";
        nextButton_text.text = "next";
        gamescore.text = score1 + " - " + score2;
        Debug.Log("現在のturn" + turn + "\n");
        Debug.Log("現在のtemp_step" + temp_step + "\n");
        Debug.Log("現在のbp" + bp + "\n");
        Debug.Log("現在のscore1は" + score1 + "\n");
        Debug.Log("現在のscore2は" + score2 + "\n");
    }

    public void JudgeGameResult()
    {
        if (score1 > score2)
        {
            Debug.Log(teamName2.text + "の勝ち"+ teamName4.text + "の負け");
            int winner = PlayerPrefs.GetInt(teamName2.text + "WinCount") + 1;
            int loser = PlayerPrefs.GetInt(teamName4.text + "LoseCount") + 1;
            PlayerPrefs.SetInt(teamName2.text + "WinCount", winner);
            PlayerPrefs.SetInt(teamName4.text + "LoseCount", loser);
            PlayerPrefs.Save();
        }
        else if (score1 < score2)
        {
            Debug.Log(teamName4.text + "の勝ち" + teamName2.text + "の負け");
            int winner = PlayerPrefs.GetInt(teamName4.text + "WinCount") + 1;
            int loser = PlayerPrefs.GetInt(teamName2.text + "LoseCount") + 1;
            PlayerPrefs.SetInt(teamName4.text + "WinCount", winner);
            PlayerPrefs.SetInt(teamName2.text + "LoseCount", loser);
            PlayerPrefs.Save();
        }
        else if(score1==score2)
        {
            Debug.Log(teamName2.text + "と" + teamName4.text+"は引き分け");
            int drawer1 = PlayerPrefs.GetInt(teamName2.text + "DrawCount") + 1;
            int drawer2 = PlayerPrefs.GetInt(teamName4.text + "DrawCount") + 1;
            PlayerPrefs.SetInt(teamName2.text + "DrawCount", drawer1);
            PlayerPrefs.SetInt(teamName4.text + "DrawCount", drawer2);
            PlayerPrefs.Save();
        }


    }

}
