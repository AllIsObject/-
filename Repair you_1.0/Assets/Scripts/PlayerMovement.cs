using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed;

    private Rigidbody2D rb;
    private PlayerInfo playerInfo;
    private Animator ani;
    private float moveH, moveV;
    public bool IsCanMove = true;//是否可以移动

    public float MoveSpeed {
        get => moveSpeed;
        set {
            if (value < 0) return;
            moveSpeed = value;
        } 
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        ani = GetComponent<Animator>();
        playerInfo = GetComponent<PlayerInfo>();
    }

    private void FixedUpdate()
    {
        if (!IsCanMove) return;
        moveH = Input.GetAxisRaw(PlayerData.GetMoveStr(playerInfo.playerNum)[0]) * MoveSpeed;
        moveV = Input.GetAxisRaw(PlayerData.GetMoveStr(playerInfo.playerNum)[1]) * MoveSpeed;
        rb.velocity = new Vector2(moveH, moveV);
        
        if (moveH!=0 && Mathf.Sign(transform.localScale.x)!= Mathf.Sign(moveH)) {
        transform.localScale =
                    new Vector3(-1* transform.localScale.x,
                    transform.localScale.y, transform.localScale.z);
        }
        
        //动画
        ani.SetBool("IsMove", rb.velocity!=Vector2.zero);
    }

}
