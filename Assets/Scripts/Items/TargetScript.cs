using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetScript : Item
{
    [SerializeField]
    private GameObject _effect;

    internal override void CollideWithPlayer()
    {
        GameManager.Instance.AddScore();
        GameManager.Instance.Spawn();
        _player.RefillFuel();
        Instantiate(_effect, transform.position, _effect.transform.rotation);
    }
}
