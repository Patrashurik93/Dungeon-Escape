using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spider : Enemy, IDamageabale
{
    public GameObject acidEffectPrefab;
    public int Health { get; set; }

    //Для ввода стартовых параметров
    public override void Init()
    {
        base.Init();
        Health = base.health;
    }

    public override void Update()
    {
      //stop  
    }

    public void Damage()
    {
        if (isDead == true)
            return;
        if (GameManager.Instance.HasFlameSword)
            Health -= 5;
        else
            Health--;
        if (Health < 1)
        {
            isDead = true;
            anim.SetTrigger("Death");
            GameObject diamond = Instantiate(diamondPrefab, transform.position, Quaternion.identity) as GameObject;
            diamond.GetComponent<Diamond>().gems = base.gems;
        }
    }
    public override void Movement()
    {
       //stop


    }
    public void Attack()
    {
        Instantiate(acidEffectPrefab, transform.position, Quaternion.identity);
    }
}
