using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public GameManager gameManager;

    public float maxSpeed = 1f;
    public float maxAccelerate = 2f;
    public float maxTouchRadius = 1f;
    public float friction = 1f;
    public float maxRotationSpeed = 0.001f;

    public Vector3 accelerate;
    public Vector3 speed;

    public Sprite[] sprite;
    private int spriteIndex;
    private SpriteRenderer spriteRenderer;

    public float maxFuel = 33;
    public float fuel = 0;

    private Light backlight;
    public float maxLightIntensity = 5.0f;

    private void Start()
    {
        spriteIndex = sprite.Length - 1;
        spriteRenderer = GetComponent<SpriteRenderer>();
        backlight = GetComponent<Light>();
    }

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            
            Vector3 targetPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            targetPosition.z = 0;

            accelerate = (targetPosition - transform.position);
            accelerate = maxAccelerate * Mathf.Clamp01(accelerate.magnitude / maxTouchRadius) * accelerate.normalized;
        }
        else
        {
            accelerate = Vector3.zero;
        }

        speed += accelerate * Time.deltaTime;
        speed = speed.normalized * Mathf.Clamp(speed.magnitude - friction * Time.deltaTime, 0, maxSpeed);
        //transform.Translate(speed * Time.deltaTime,Space.World);
        if (speed.magnitude != 0)
            transform.up = Vector3.RotateTowards(transform.up, speed, maxRotationSpeed * Time.deltaTime, 0);

        fuel -= Time.deltaTime;
        if (fuel <= 0)
            gameManager.GameOver();

        float relativeFuel = fuel / maxFuel;
        spriteIndex = (int)(relativeFuel * (sprite.Length - 1));
        spriteRenderer.sprite = sprite[spriteIndex];
        backlight.intensity = relativeFuel * maxLightIntensity;
    }
}
