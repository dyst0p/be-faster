using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item : MonoBehaviour
{
    public float XBound { get; set; }
    public float YBound { get; set; }
    public Player PlayerData { get; set; }

    private void Awake()
    {
        XBound = 18;
        YBound = 11;
        PlayerData = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    public abstract void Move();
}
