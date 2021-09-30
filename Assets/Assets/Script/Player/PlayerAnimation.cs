using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator anim;
    private Animator sword;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponentInChildren<Animator>();
        sword = transform.GetChild(1).GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Move(float move)
    {
        anim.SetFloat("Move", Mathf.Abs(move));
    }

    public void Jump(bool jump)
    {
        anim.SetBool("Jumping", jump);
    }

    public void Attack()
    {
        anim.SetTrigger("Attack");
        sword.SetTrigger("SwordAttack");     
    }

    public void Death()
    {
        anim.SetTrigger("Death");
    }

    public void FireSword(bool fire)
    {
        anim.SetBool("Fire", fire);
    }

    public void Hit()
    {
        anim.SetTrigger("Hit");
    }
}
