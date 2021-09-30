using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderAnimationEvent : MonoBehaviour
{
    private Spider spider;

    public void Start()
    {
        spider = GetComponentInParent<Spider>();
    }
    public void Fire()
    {
        //Debug.Log("Spider should fire");
        spider.Attack();
    }
}
