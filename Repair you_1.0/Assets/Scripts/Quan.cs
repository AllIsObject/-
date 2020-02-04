using System.Collections;
using System.Collections.Generic;
using UnityEngine;

///<summary>
///圆圈的控制
///间隔变小，保持一段时间，再变大
///</summary>
public class Quan : MonoBehaviour
{
    [SerializeField] private float ChangSpeed;
    [SerializeField] private float ChangSpeedChang;//速度增大值
    [SerializeField] private float ChangMin;
    [SerializeField] private float ChangMax;
    [SerializeField] private float MinTime;//最小圈的停留时间
    [SerializeField] private float MaxTime;//最大圈的停留时间
    [SerializeField] private float Time;//一个周期的停留时间
    [SerializeField] private float TuiNum;//推力大小

    private Transform pos;
    private GameObject[] players;
    private bool isMin = false;
    private GameObject go_quan_min;
    private SpriteRenderer render;
    private void Awake()
    {
        go_quan_min = GameObject.Find("quan_min");
    }
    private void Start()
    {
        pos = gameObject.FindChild<Transform>("pos");
        render = gameObject.GetComponent<SpriteRenderer>();
        players = GameObject.FindGameObjectsWithTag("Player");
        StartCoroutine(QuanChange());
    }
    IEnumerator QuanChange()
    {
        while (true)
        {
            //先放到最大
            transform.localScale = Vector3.one * ChangMax;
            yield return new WaitForSeconds(Time);
           
            //逐渐变小
            while (true)
            {
                if (transform.localScale.x <= ChangMin) break;
                transform.localScale = transform.localScale - Vector3.one * (ChangSpeed+ ChangSpeedChang);
                yield return new WaitForSeconds(0.05f);
            }
            transform.localScale = Vector3.one * ChangMin;
            isMin = true;
            //打开碰撞体
            GameControl.Instance.SetWall(true);
            go_quan_min.SetActive(false);
            render.enabled = false;
            //停留
            yield return new WaitForSeconds(MinTime);

            isMin = false;
            //关闭碰撞体
            GameControl.Instance.SetWall(false);
            go_quan_min.SetActive(true);
            render.enabled = true;

            //直接变大
            transform.localScale = Vector3.one * ChangMax;
            //向外推
            for (int i=0;i< players.Length;i++) {

                players[i].GetComponent<PlayerSkill>()
                    .Shanxian(new Vector2(TuiNum, 0) ,0.4f);
            }

            ////逐渐变大
            //while (true)
            //{
            //    if (transform.localScale.x >= ChangMax) break;
            //    transform.localScale = transform.localScale + Vector3.one * ChangSpeed;
            //    yield return new WaitForSeconds(0.05f);
            //}
            
            //停留
            yield return new WaitForSeconds(MaxTime);
        }
       
    }
    private void Update()
    {
        //  通过半径计算，
        if (isMin) return;
        float r = Vector3.Distance(pos.position, transform.position);
        for(int i=0;i< players.Length;i++)
        {
            float _r = Vector3.Distance(players[i].transform.position, transform.position);
            if (Mathf.Abs(r - _r)<0.1f) {
                string win_name = "";
                for (int j = 0; j < players.Length; j++)
                {
                    if (players[i] != players[j])
                    {
                        var info=players[j].GetComponent<PlayerInfo>();
                        win_name=info.playerNum.ToString();
                        break;
                    }
                }
                //游戏结束
                GameControl.Instance.GameOver(win_name);
            }
        }

    }

}
