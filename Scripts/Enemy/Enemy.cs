using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    [SerializeField]
    protected int health;

    [SerializeField]
    protected int speed;

    [SerializeField]
    protected int gems;

    [SerializeField]
    protected Transform pointA, pointB;
    protected Vector3 currentTarget;
    protected Animator anim;
    public static bool isHit = false;
    public int rotation_Angle = 0;
    public GameObject loot;
    public bool isDead = false;
    public virtual void Init()
    {
        anim = GetComponentInChildren<Animator>();
    }
    private void Start()
    {
        Init();
    }

    public virtual void Update()
    {
        if (!anim.GetCurrentAnimatorStateInfo(0).IsName("Moss_Idle") && !anim.GetCurrentAnimatorStateInfo(0).IsName("Spider_Idle") && !anim.GetCurrentAnimatorStateInfo(0).IsName("Idle") && !anim.GetCurrentAnimatorStateInfo(0).IsName("Attack") && !anim.GetCurrentAnimatorStateInfo(0).IsName("Hit") && !isDead)
        {
            Movement();
        }
    }

    public virtual void Movement()
    {
        if (!isDead)
        {
            if (transform.position.x == pointA.position.x)
            {
                currentTarget = pointB.position;
                transform.eulerAngles = new Vector3(0, 0, 0);
                rotation_Angle = 0;
                anim.SetTrigger("Idle");
            }
            else if (transform.position.x == pointB.position.x)
            {
                currentTarget = pointA.position;
                transform.eulerAngles = new Vector3(0, 180, 0);
                anim.SetTrigger("Idle");
                rotation_Angle = 180;
            }
            if (!isHit)
            {
                transform.position = Vector3.MoveTowards(transform.position, currentTarget, speed * Time.deltaTime);
            }
        }
    }
    public virtual void Attack(string side)
    {
        if (!anim.GetCurrentAnimatorStateInfo(0).IsName("Attack") && !anim.GetCurrentAnimatorStateInfo(0).IsName("Hit") && !isDead)
        {
            if (side == "right")
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
            }
            else
            {
                transform.eulerAngles = new Vector3(0, 180, 0);
            }
            anim.SetTrigger("Attack");
        }
    }
    public virtual void AttackToWalk()
    {
        if (!isDead)
        {
            transform.eulerAngles = new Vector3(0, rotation_Angle, 0);
        }
    }

    public virtual void Damage()
    {
        health -= PlayerController.attack_power;
        if (gameObject.name != "Spider" && !isDead)
        {
            anim.SetTrigger("Hit");
        }
        if (health < 1 && !isDead)
        {
            isDead = true;
            Instantiate(loot, transform.position, Quaternion.identity);
            anim.SetTrigger("Death");
            Invoke("DestroyThis", 3f);
        }
    }

    void DestroyThis()
    {
        Destroy(gameObject);
    }


}
