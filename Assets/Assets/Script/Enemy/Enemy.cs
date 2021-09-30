using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    [SerializeField] protected int health;
    [SerializeField] protected float speed;
    [SerializeField] protected int gems;
    [SerializeField] protected Transform pointA, pointB;
    [SerializeField] protected Player Player;

    public GameObject diamondPrefab;

    protected Vector3 CurrentTarget;
    protected Animator Anim;
    protected SpriteRenderer Sprite;     
    protected bool isHit = false;
    protected bool isDead = false;

    private string _idle = "Idle";
    private string _inCombat = "InCombat";
    private float _minDistanceToAttack = 2.0f;


    public virtual void Init()
    {    
        Anim = GetComponentInChildren<Animator>();
        Sprite = GetComponentInChildren<SpriteRenderer>();
        Player = GetComponent<Player>();
    }
    
    private void Start()
    {   
        Init(); 
    }

    public virtual void Update()
    {
        if (Anim.GetCurrentAnimatorStateInfo(0).IsName(_idle) && Anim.GetBool(_inCombat) == false)
        {
            return;
        }
        
        if(isDead == false)
        {
            Movement();
        }
       
    }

    public virtual void Movement()
    {
    
        if (CurrentTarget == pointA.position)
        {
            Sprite.flipX = true;
        }
        else
        {
            Sprite.flipX = false;
        }

        if (transform.position == pointA.position)
        {
            CurrentTarget = pointB.position;
            Anim.SetTrigger(_idle);
        }
        else if (transform.position == pointB.position)
        {
            CurrentTarget = pointA.position;
            Anim.SetTrigger(_idle);
        }

        if (isHit == false)
        {
            transform.position = Vector3.MoveTowards(transform.position, CurrentTarget, speed * Time.deltaTime);
        }

        float distance = Vector3.Distance(transform.localPosition, Player.transform.localPosition);

        if (distance > _minDistanceToAttack)
        {
            isHit = false;
            Anim.SetBool(_inCombat, false);
        }

        Vector3 direction = Player.transform.localPosition - transform.localPosition;

        if (direction.x > 0 && Anim.GetBool(_inCombat) == true)
        {
            Sprite.flipX = false;
        }
        else if (direction.x < 0 && Anim.GetBool(_inCombat) == true)
        {
            Sprite.flipX = true;
        }
    }
}

