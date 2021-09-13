using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollBackground : MonoBehaviour
{
    [SerializeField]
    private float _unitPerTexture = 4;

    [SerializeField] private Player _player;
    private Vector2 _offset = new Vector2();
    private Renderer _bgRenderer;

    void Start()
    {
        _bgRenderer = GetComponent<Renderer>();
        _player = Player.Instance;
    }

    void LateUpdate()
    {
        Vector3 playerSpeed = _player.Speed;
        _offset.x = (_offset.x + playerSpeed.x * Time.deltaTime / _unitPerTexture) % 1;
        _offset.y = (_offset.y + playerSpeed.y * Time.deltaTime / _unitPerTexture) % 1;
        _bgRenderer.material.mainTextureOffset = _offset;
    }
}
