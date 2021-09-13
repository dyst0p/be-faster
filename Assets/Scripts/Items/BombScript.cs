using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombScript : Item
{
    internal override void CollideWithPlayer()
    {
        GameManager.Instance.GameOver();
    }
}
