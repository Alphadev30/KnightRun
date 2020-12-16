using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    public GameObject diamondPrefab;
    public AudioSource onHitPlay;

    [SerializeField]
    protected int health;
    [SerializeField]
    protected int speed;
    [SerializeField]
    protected int gems;
    [SerializeField]
    protected Transform PointA, PointB;

    protected Vector3 currentTarget;
    protected Animator anim;
    protected SpriteRenderer Sprite;

    protected bool isHit = false;
    protected bool isDead = false;

    protected Player player;

    public virtual void init()
    {
        anim = GetComponentInChildren<Animator>();
        Sprite = GetComponentInChildren<SpriteRenderer>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        onHitPlay = GetComponent<AudioSource>();
    }

    public void Start()
    {
        init();
    }

    public virtual void Update()
    {
        // if idle dont move 
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("idle") && anim.GetBool("InCombat") == false)
        {
            return;
        }

        if (isDead == false)
        {
            Movement();
        }
        
    }

    public virtual void Movement()
    {
        if (currentTarget == PointA.position)
        {
            Sprite.flipX = true;
        }
        else if (currentTarget == PointB.position)
        {
            Sprite.flipX = false;
        }

        if (transform.position == PointA.position)
        {
            currentTarget = PointB.position;
            anim.SetTrigger("idle");
        }
        else if (transform.position == PointB.position)
        {
            currentTarget = PointA.position;
            anim.SetTrigger("idle");
        }

        if (isHit == false)
        {
            transform.position = Vector3.MoveTowards(transform.position, currentTarget, speed * Time.deltaTime);
        }


        //Check for distance between player and enemy 
        float distance = Vector3.Distance(transform.localPosition, player.transform.localPosition);
        if (distance > 2.0f)
        {
            isHit = false;
            anim.SetBool("InCombat", false);
        }

    }

}
