using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

///<summary>
///游戏总管理
///</summary>
public class GameControl : SingletionBase<GameControl>
{
    GameObject UI;
    GameObject UI_SkillPanel;
    GameObject Grid;
    private GameObject[] players;

    private bool isGameOver = false;
    public override void Init()
    {
        UI = GameObject.Find("Canvas");
        UI_SkillPanel = UI.FindChild("SkillPanel");
        Grid = GameObject.Find("Grid");
        UI.FindChild("GameOver").SetActive(false);
        Grid.FindChild("wall").SetActive(false);
        players = GameObject.FindGameObjectsWithTag("Player");



        var p1 = UI_SkillPanel.FindChild("P1");
        var p2 = UI_SkillPanel.FindChild("P2");
        text_shanxian= p1.FindChild<Text>("text_shanxian");
        text_maozi = p1.FindChild<Text>("text_maozi");
        text_emo = p1.FindChild<Text>("text_emo");

        text_shanxian_p2 = p2.FindChild<Text>("text_shanxian");
        text_maozi_p2 = p2.FindChild<Text>("text_maozi");
        text_emo_p2 = p2.FindChild<Text>("text_emo");

        InvokeRepeating("UpdateSkillPanel",0.5f,0.2f);
    }

    public void GameOver(string win_name) {
        var go = UI.FindChild("GameOver");
        go.FindChild<Text>("text_win").text = win_name;
        go.SetActive(true);
        isGameOver = true;
    }
    public void SetWall(bool IsShow) {
        Grid.FindChild("wall").SetActive(IsShow);
    }
    Text text_shanxian;
    Text text_maozi;
    Text text_emo;

    Text text_shanxian_p2;
    Text text_maozi_p2;
    Text text_emo_p2;

    string color_red = "<color=#ff0000>{0}</color>";
    string color_green= "<color=#00ff00>{0}</color>";
    public void UpdateSkillPanel ()
    {
        if (isGameOver) return;
        var player = players[0];
        var p1_skill=player.GetComponent<PlayerSkill>();
        var time = p1_skill.shanxianNextTime - Time.time;
        text_shanxian.text = time<0 ? string.Format(color_green, 0) :
            string.Format(color_red, string.Format("{0:F}", time)); 
        time = p1_skill.maoziNextTime - Time.time;
        text_maozi.text = time < 0 ? string.Format(color_green, 0) :
            string.Format(color_red, string.Format("{0:F}", time)); 
        time = p1_skill.emoNextTime - Time.time;
        text_emo.text = time < 0 ? string.Format(color_green, 0) :
            string.Format(color_red, string.Format("{0:F}", time)); 

        player = players[1];
        p1_skill = player.GetComponent<PlayerSkill>();
        time = p1_skill.shanxianNextTime - Time.time;
        text_shanxian_p2.text = time < 0 ? string.Format(color_green, 0) :
            string.Format(color_red, string.Format("{0:F}", time));
        time = p1_skill.maoziNextTime - Time.time;
        text_maozi_p2.text = time < 0 ? string.Format(color_green, 0) :
            string.Format(color_red, string.Format("{0:F}", time)); 
        time = p1_skill.emoNextTime - Time.time;
        text_emo_p2.text = time < 0 ? string.Format(color_green, 0) :
            string.Format(color_red, string.Format("{0:F}", time)); 
    }

}
