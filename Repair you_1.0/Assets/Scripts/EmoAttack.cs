using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 恶魔元气蛋
/// </summary>
public class EmoAttack :MonoBehaviour
{
    [SerializeField] private float lifeTimer;
    [SerializeField] private GameObject go_weapon;


    private List<GameObject> players = new List<GameObject>();




    private void Start()
    {
        Destroy(gameObject, lifeTimer);
    }
    private void OnDestroy()
    {
        players.Clear();
        players = null;
    }
    public void AttackCall() {
        //判断是否有接触敌人
        players.ForEach(a=> {
            //给他/她戴帽子
            var go =Instantiate(go_weapon);
            go.GetComponent<Weapon>().Dai(a);

            //给自己减帽子
            var info=transform.parent.GetComponent<PlayerInfo>();
            info.MaoziNum--;
        });
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            players.Add(collision.gameObject);

        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        

    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            players.Remove(collision.gameObject);

        }
    }
}

