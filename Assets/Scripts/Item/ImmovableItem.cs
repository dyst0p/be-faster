using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImmovableItem : Item
{
    public override void Move()
    {
        Vector3 pos = transform.position;
        pos -= PlayerData.Speed * Time.deltaTime;
        if (pos.x < -XBound)
            pos.x += 2 * XBound;
        else if (pos.x > XBound)
            pos.x -= 2 * XBound;
        if (pos.y < -XBound)
            pos.y += 2 * YBound;
        else if (pos.y > YBound)
            pos.y -= 2 * YBound;
        transform.position = pos;
    }
    // Update is called once per frame
    void LateUpdate()
    {
        Move();
    }
}
