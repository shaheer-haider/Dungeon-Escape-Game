using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static bool Playing;
    public GameObject joyStick;
    private FixedJoystick joyStickComponent;
    Rigidbody2D rb;
    Animator anim;
    float hForce;
    float jumpForce = 7f;
    float speed = 4f;
    public LayerMask ground;
    private enum State { idle, running, jumping }
    int state = (int)State.idle;
    public static int attack_power;
    public static int collectedDiamonds = 0;
    public static int totalDiamonds = 0;

    // sounds
    public AudioClip jumpAudio;
    public AudioClip AttackAudio;
    public AudioClip HeavyAttackAudio;
    public AudioClip WalkAudio;
    // public AudioClip death;
    // public AudioClip Hit;

    private AudioSource audioSource;


    Animator arc_anim;
    public static float playerHealth = 10f;
    void Start()
    {
        Playing = true;
        audioSource = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        arc_anim = transform.GetChild(0).GetComponent<Animator>();
        if (!PlayerPrefs.HasKey("TotalDiamonds"))
        {
            PlayerPrefs.SetInt("TotalDiamonds", 0);
        }
        joyStickComponent = joyStick.GetComponent<FixedJoystick>();
    }

    private void Update()
    {
        Move();
        if (Input.GetKeyDown("space"))
        {
            Jump();
        }
        if (rb.velocity.x == 0 && rb.velocity.y == 0)
        {
            IdleAnimation();
        }
        anim.SetInteger("State", state);
        if (Input.GetKeyDown(KeyCode.K) && rb.IsTouchingLayers(ground))
        {
            Attack();
        }
        if (Input.GetKeyDown(KeyCode.L) && rb.IsTouchingLayers(ground) && GameManager.instance.hasFireSword == "True")
        {
            HeavyAttack();
        }
    }

    void Move()
    {
        hForce = joyStickComponent.Horizontal;
        if (SystemInfo.deviceType == DeviceType.Desktop && hForce == 0)
        {
            hForce = Input.GetAxis("Horizontal");
        }

        rb.velocity = new Vector2(hForce * speed, rb.velocity.y);
        if (hForce != 0)
        {
            RunningAnimation();
        }
    }
    void RunningAnimation()
    {
        if (hForce < 0)
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
        }
        else if (hForce > 0)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
        if (rb.IsTouchingLayers(ground))
        {
            state = (int)State.running;
            if (!audioSource.isPlaying)
            {
                audioSource.clip = WalkAudio;
                audioSource.Play();
            }
        }
    }

    void IdleAnimation()
    {
        state = (int)State.idle;
    }

    public void Jump()
    {
        if (rb.IsTouchingLayers(ground))
        {
            rb.velocity = new Vector2(0, jumpForce);
            state = (int)State.jumping;
            audioSource.clip = jumpAudio;
            audioSource.Play();
        }
    }
    public void Attack()
    {
        // firesword use nahi ho rahi ho to
        if (!anim.GetCurrentAnimatorStateInfo(0).IsName("Player_FireSword") && !anim.GetCurrentAnimatorStateInfo(0).IsName("Player_Attack"))
        {
            anim.SetTrigger("Attack");
            Invoke("ShowArc", 0.2f);
            attack_power = 1;
            audioSource.clip = AttackAudio;
            audioSource.Play();
        }
    }
    public void HeavyAttack()
    {
        if (!anim.GetCurrentAnimatorStateInfo(0).IsName("Player_FireSword") && !anim.GetCurrentAnimatorStateInfo(0).IsName("Player_Attack"))
        {
            anim.SetTrigger("FireSword");
            attack_power = 2;
            audioSource.clip = HeavyAttackAudio;
            audioSource.Play();
        }
    }

    void ShowArc()
    {
        arc_anim.SetTrigger("Show");
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "SkeletonAttack" || other.gameObject.tag == "SpiderAttack")
        {
            playerHealth--;
            UIManager.instance.decreaseHealth(1);
        }
        else if (other.gameObject.tag == "MossAttack")
        {
            playerHealth -= 2f;
            UIManager.instance.decreaseHealth(2);
        }
        else if (other.gameObject.tag == "Diamond")
        {
            collectedDiamonds++;
            PlayerPrefs.SetInt("TotalDiamonds", PlayerPrefs.GetInt("TotalDiamonds") + 1);
            Destroy(other.gameObject);
        }
        if (playerHealth < 1)
        {
            Destroy(transform.parent.gameObject);
            Playing = false;
        }
    }
}
