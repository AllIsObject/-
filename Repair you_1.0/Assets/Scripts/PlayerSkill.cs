using System.Collections;
using System.Collections.Generic;
using UnityEngine;

///<summary>
///玩家技能类
///</summary>
public class PlayerSkill : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float moveTime;
    [SerializeField] private GameObject go_weapon;
    [SerializeField] private GameObject go_emoAttack;
    //技能CD
    [SerializeField] private float shanxianTime;
    [SerializeField] private float maoziTime;
    [SerializeField] private float emoTime;

    [SerializeField] public int emoNum;//切换恶魔需要的帽子数量

    public float shanxianNextTime=0;
    public float maoziNextTime = 0;
    public float emoNextTime = 0;

    private Rigidbody2D rb;
    private PlayerMovement playerMovement;
    private PlayerInfo playerInfo;
    private float moveH, moveV;
    private Weapon weapon;

    public Weapon Weapon { get => weapon; set => weapon = value; }

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerMovement = GetComponent<PlayerMovement>();
        playerInfo = GetComponent<PlayerInfo>();

    }

    private void Update()
    {
        if (!Weapon)//初始化武器
        {
            var go = Instantiate(go_weapon, gameObject.FindChild("weapom_pos").transform);
            var weapon = go.GetComponent<Weapon>();
            weapon.Init(gameObject);
            this.Weapon = weapon;
        }
    }
    private void FixedUpdate()
    {
        moveH = Input.GetAxisRaw(PlayerData.GetMoveStr(playerInfo.playerNum)[0] );
        moveV = Input.GetAxisRaw(PlayerData.GetMoveStr(playerInfo.playerNum)[1]);
  

        if (Input.GetKeyDown(PlayerData.GetShanxianStr(playerInfo.playerNum)))
        {
            if (playerInfo.IsEmo)
            {
                //恶魔攻击
                EmoAttack();
            }
            else {
                Shanxian(new Vector2(moveH * moveSpeed, moveV * moveSpeed), moveTime);
            }

        }
        if (Input.GetKeyDown(PlayerData.GetChuiziStr(playerInfo.playerNum)))
        {
            if (playerInfo.IsEmo)
                return;
            MoveWeapon(new Vector2(moveH , moveV ));
        }
    }
    /// <summary>
    /// 闪现
    /// </summary>
    /// <param name="pos">方向和大小</param>
    public void Shanxian(Vector2 pos,float moveTime) {
        if (shanxianNextTime > Time.time) return;
        if (pos == Vector2.zero)
        {
            pos = new Vector3(transform.localScale.x, 0, 0) * moveSpeed;
        }
        //CD计时
        shanxianNextTime = Time.time+ shanxianTime;

        playerMovement.IsCanMove = false;
        rb.velocity = pos;
        //计时1秒

        StartCoroutine(TimeShanxian(moveTime));
    }

    public void EmoAttack() {
        if (emoNextTime > Time.time) return;
        emoNextTime = Time.time + emoTime;
        
        Instantiate(go_emoAttack, transform.position, Quaternion.identity,transform);
    }
    IEnumerator TimeShanxian(float moveTime) {

        yield return new WaitForSeconds(moveTime);
        playerMovement.IsCanMove = true;
        rb.velocity = Vector2.zero;
    }
    void MoveWeapon(Vector2 pos) {

        if ( maoziNextTime > Time.time) return;
        maoziNextTime = Time.time + maoziTime;
        Weapon.Move(pos);
    }
}
