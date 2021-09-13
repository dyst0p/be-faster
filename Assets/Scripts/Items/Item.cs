using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    [Header("Bounds")]
    [SerializeField]
    internal float _xBound = 18;
    [SerializeField]
    internal float _yBound = 11;

    internal Player _player;

    private void Start()
    {
        _player = Player.Instance;
    }

    void LateUpdate()
    {
        Move();
    }

    internal void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.gameObject == _player.gameObject)
        {
            
            CollideWithPlayer();
        }
        
        Destroy(gameObject);
    }

    internal void Move()
    {
        transform.position -= CalcOffset();
        // if the item is out of bounds, we return it from the back side
        ReturnToPlayZone();
    }

    internal virtual Vector3 CalcOffset()
    {
        Vector3 offset = _player.Speed * Time.deltaTime;
        return offset;
    }
    
    internal void ReturnToPlayZone()
    {
        Vector3 pos = transform.position;

        if (pos.x < -_xBound)
            pos.x += 2 * _xBound;
        else if (pos.x > _xBound)
            pos.x -= 2 * _xBound;

        if (pos.y < -_yBound)
            pos.y += 2 * _yBound;
        else if (pos.y > _yBound)
            pos.y -= 2 * _yBound;

        transform.position = pos;
    }

    internal virtual void CollideWithPlayer() { }
}
