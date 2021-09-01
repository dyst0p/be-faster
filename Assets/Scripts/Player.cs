using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public GameManager gameManager;

    [SerializeField] float maxSpeed = 1f;
    [SerializeField] float maxAccelerate = 2f;
    [SerializeField] float maxTouchRadius = 1f;
    [SerializeField] float friction = 1f;
    [SerializeField] float maxRotationSpeed = 0.001f;

    public Vector3 accelerate;
    public Vector3 speed;

    [SerializeField] Sprite[] sprite;
    private int spriteIndex;
    private SpriteRenderer spriteRenderer;

    [SerializeField] float maxFuel = 30;
    float fuel = 0;

    private Light backlight;
    [SerializeField] float maxLightIntensity = 5.0f;

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
            CalcAccelerate();
        }
        else
        {
            accelerate = Vector3.zero;
        }

        CalcSpeed();

        fuel -= Time.deltaTime;
        if (fuel <= 0)
            gameManager.GameOver();

        BacklightDisplay();
    }

    void CalcAccelerate()
    {
        Vector3 targetPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        targetPosition.z = 0;

        accelerate = (targetPosition - transform.position);
        accelerate = maxAccelerate * Mathf.Clamp01(accelerate.magnitude / maxTouchRadius) * accelerate.normalized;
    }

    void CalcSpeed()
    {
        speed += accelerate * Time.deltaTime;
        speed = speed.normalized * Mathf.Clamp(speed.magnitude - friction * Time.deltaTime, 0, maxSpeed);

        if (speed.magnitude != 0)
            transform.up = Vector3.RotateTowards(transform.up, speed, maxRotationSpeed * Time.deltaTime, 0);
    }

    void BacklightDisplay()
    {
        float relativeFuel = fuel / maxFuel;

        spriteIndex = (int)(relativeFuel * (sprite.Length - 1));
        spriteRenderer.sprite = sprite[spriteIndex];
        backlight.intensity = relativeFuel * maxLightIntensity;
    }

    public void RefillFuel() => fuel++;
    public void RefillFuel(float fuel)
    {
        this.fuel = Mathf.Clamp(this.fuel + fuel, 0, maxFuel);
    }

    public void FullTank() => RefillFuel(maxFuel);
}
