using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    private bool canAttack = true;
    private void OnTriggerEnter2D(Collider2D other)
    {
        IDamageabale hit = other.GetComponent<IDamageabale>();

        if (hit != null && canAttack)
        {
            //Debug.Log("Hit: " + other.name);
            hit.Damage();
            canAttack = false;
            StartCoroutine(AttackDelay());
        }

        IEnumerator AttackDelay()
        { 
            yield return new WaitForSeconds(0.5f);
            canAttack = true;
        }
    }
}
