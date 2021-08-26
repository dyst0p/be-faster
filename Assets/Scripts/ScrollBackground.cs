using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollBackground : MonoBehaviour
{
    public Player player;
    public float unitPerTexture = 4;

    private Vector2 offset = new Vector2(0, 0);
    private Renderer bgRenderer;

    // Start is called before the first frame update
    void Start()
    {
        bgRenderer = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        offset.x = (offset.x + player.speed.x * Time.deltaTime / unitPerTexture) % 1;
        offset.y = (offset.y + player.speed.y * Time.deltaTime / unitPerTexture) % 1;
        bgRenderer.material.mainTextureOffset = offset;
    }
}
