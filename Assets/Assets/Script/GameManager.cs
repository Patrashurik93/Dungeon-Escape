using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                Debug.LogError("Game manager is null");
            }
            return instance;
        }   
    }

    public bool HasKeyToCastle { get; set; }

    public bool HasFlyingBoots { get; set; }

    public bool HasFlameSword { get; set; }

    public Player Player { get; private set; }

    public void Awake()
    {
        instance = this;
        Player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }
}
