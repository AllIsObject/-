using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private float rotateSpeed;
    [SerializeField] private float jianshaoSpeed;//减少的速度
    [SerializeField] private float moveSpeed;
    

    private bool isMove;
    private Vector2 pos;
    private GameObject player;//所拥有的玩家

    private bool isDai=false;//是否被带帽子
    private void Start()
    {
      
    }
    private void OnDestroy()
    {
        //加移动速度
        if (isDai && player) {
            player.GetComponent<PlayerMovement>().MoveSpeed += jianshaoSpeed;
        }
    }
    private void Update()
    {
        
        if(!isDai && isMove)
        {
            SelfRotation();
            ThrowWeapon();//MARKER If click, Throw the weapon
        }
    }

    private void SelfRotation()
    {
        if (isMove)//STEP 02
        {
            transform.Rotate(0, 0, rotateSpeed * Time.deltaTime);//STEP 01 Weapon Rotation
        }
        else
        {
            transform.Rotate(0, 0, 0);
        }
    }
    public void Init(GameObject player)
    {
        isDai = false;
        isMove = false;
        transform.localPosition = Vector3.zero;
        transform.Rotate(0, 0, 0);
        this.player = player;
    }
    public void Dai(GameObject target) {
        //降低玩家移动速度，戴帽子
        transform.parent = target.gameObject.FindChild<Transform>("weapomed_pos");

        target.gameObject.GetComponent<PlayerMovement>().MoveSpeed -= jianshaoSpeed;
        if(this.player)
            this.player.GetComponent<PlayerSkill>().Weapon = null;

        isDai = true;
        isMove = false;
        
        transform.localRotation=Quaternion.identity;
        Vector3 pos=Vector3.zero;
        //增高
        if (transform.parent.childCount>0) {
            float hight = 0;
            //取最高的高度
            for (int i=0;i< transform.parent.childCount;i++) {
                if (hight == 0 || transform.parent.GetChild(i).transform.localPosition.y > hight)
                    hight = transform.parent.GetChild(i).transform.localPosition.y;
            }
            pos = new Vector3(0, hight + 0.2f, 0);
            Debug.Log(pos);
        }
        
        transform.localPosition = pos;
        this.player = target;
    }
    public void Move(Vector2 pos)
    {
        if (pos == Vector2.zero)
        {
            pos = new Vector3(player.transform.localScale.x, 0, 0) * moveSpeed;
        }
        this.pos = new Vector2(pos.x * 9999, pos.y * 9999);
        isMove = true;
        transform.parent = null;
    }
    private void ThrowWeapon()
    {

        isMove = true;
        transform.position = Vector2.MoveTowards(transform.position, pos, moveSpeed * Time.deltaTime);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.tag == "floor" && isMove)//FIXME isDamage
        {
            isMove = false;
            Destroy(gameObject);
        } else if (other.tag == "Player" && other.gameObject!= player && !isDai && isMove) {
            

            //判断帽子数量，如果大于3则切换恶魔
            var info= other.gameObject.GetComponent<PlayerInfo>();
            ////如果恶魔此时放技能则抵消一次攻击
            //if (info.isEmo) {
            //    var emoAttack=other.gameObject.GetComponentInChildren<EmoAttack>();
            //    if(emoAttack!=null)
            //        return;
            //}

            if (info.MaoziNum > other.gameObject.GetComponent<PlayerSkill>().emoNum)
            {
                //恶魔变身
                info.IsEmo = true;
                Destroy(gameObject);
                return;
            }


            Dai(other.gameObject);
        }
    }
}
