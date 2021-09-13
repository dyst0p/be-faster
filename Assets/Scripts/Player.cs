using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private GameManager _gameManager;
    public static Player Instance { get; private set; }

    [Header("Movement")]
    [SerializeField]
    private float _maxSpeed;
    [SerializeField]
    private float _maxAccelerate;
    [SerializeField]
    private float _maxTouchRadius;
    [SerializeField]
    private float _friction;
    [SerializeField]
    private float _maxRotationSpeed;

    private Vector3 _accelerate;
    public Vector3 Speed { get; private set; }

    [Header("Fuel")]
    [SerializeField]
    private float _maxFuel;

    private float _fuel = 0;

    [Header("Render")]
    [SerializeField]
    private Sprite[] _sprites;
    [SerializeField]
    private float _maxLightIntensity;
    [SerializeField]
    private GameObject _explosionPrefab;

    private int _spriteIndex;
    private SpriteRenderer _spriteRenderer;
    private Light _backlight;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        _gameManager = GameManager.Instance;
        _spriteIndex = _sprites.Length - 1;
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _backlight = GetComponent<Light>();
    }

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            CalcAccelerate();
        }
        else
        {
            _accelerate = Vector3.zero;
        }

        CalcSpeed();

        _fuel -= Time.deltaTime;
        if (_fuel <= 0)
            _gameManager.GameOver();

        Render();
    }

    void CalcAccelerate()
    {
        Vector3 targetPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        targetPosition.z = 0;

        _accelerate = (targetPosition - transform.position);
        _accelerate = _maxAccelerate * Mathf.Clamp01(_accelerate.magnitude / _maxTouchRadius) * _accelerate.normalized;
    }

    void CalcSpeed()
    {
        Speed += _accelerate * Time.deltaTime;
        Speed = Speed.normalized * Mathf.Clamp(Speed.magnitude - (_friction * Time.deltaTime), 0, _maxSpeed);

        if (Speed.magnitude != 0)
            transform.up = Vector3.RotateTowards(transform.up, Speed, _maxRotationSpeed * Time.deltaTime, 0);
    }

    public void StopMovement()
    {
        Speed = Vector3.zero;
        transform.rotation = Quaternion.identity;
    }

    public void Explosion()
    {
        StopMovement();
        gameObject.SetActive(false);
        Instantiate(_explosionPrefab, transform.position, _explosionPrefab.transform.rotation);
    }

    private void Render()
    {
        float relativeFuel = _fuel / _maxFuel;

        SetSprite(relativeFuel);
        SetBacklight(relativeFuel);
    }

    private void SetSprite(float relativeFuel)
    {
        _spriteIndex = (int)(relativeFuel * (_sprites.Length - 1));
        _spriteRenderer.sprite = _sprites[_spriteIndex];
    }

    private void SetBacklight(float relativeFuel)
    {
        _backlight.intensity = relativeFuel * _maxLightIntensity;
    }

    public void RefillFuel(float addedFuel)
    {
        _fuel = Mathf.Clamp(_fuel + addedFuel, 0, _maxFuel);
    }
    public void RefillFuel() => RefillFuel(1);

    public void FullTank() => RefillFuel(_maxFuel);
}
