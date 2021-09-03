using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
public class PlayerController : MonoBehaviour
{
    GameManager _gameManager;
    InputHandler _inputHandler;

    [SerializeField]
    Gun gun;

    GameState _state;

    public Gun Gun => gun;

    [Inject]
    private void OnInstall(GameManager gameManager)
    {
        _gameManager = gameManager;
        _gameManager.OnGameStateChanged += CustomUpdate;
    }
    public void Initialize()
    {
        gun.Initialize();
        _inputHandler = new InputHandler();
    }

    public void PostInit()
    {
        gun.PostInitialize();
    }

    private void Fire()
    {
        if (Input.GetMouseButtonDown(0))
            gun.Fire();
    }
    private void SetDefault()
    {
        gun.SetDefault();
    }
    private void CustomUpdate(GameState state)
    {
        _state = state;

        switch (state)
        {
            case GameState.Loading:
                Initialize();
                SetDefault();
                break;
            case GameState.Idle:
                gun.PostInitialize();
                break;
            case GameState.Gameplay:
                break;
            case GameState.Win:
                break;
            case GameState.Lose:
                break;
            default:
                break;
        }
    }
    private void Look()
    {
        gun.LookAt(_inputHandler.GunRotation);
        transform.Rotate(_inputHandler.CharRotation);
    }
    private void Move()
    {
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            transform.position += transform.forward * GameConfig.CHAR_SPEED * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            transform.position += -transform.right * GameConfig.CHAR_SPEED * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            transform.position += -transform.forward * GameConfig.CHAR_SPEED * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            transform.position += transform.right * GameConfig.CHAR_SPEED * Time.deltaTime;
        }
    }
    private void SetGunMode()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
            gun.SetGunMode(GunBehaviour.Pistol);
        if (Input.GetKeyDown(KeyCode.Alpha2))
            gun.SetGunMode(GunBehaviour.Shotgun);

        if (Input.GetKeyDown(KeyCode.X))
            gun.SetBulletBehaviour(BulletBehaviour.Delayed);
        if (Input.GetKeyDown(KeyCode.Z))
            gun.SetBulletBehaviour(BulletBehaviour.Big);
        if (Input.GetKeyDown(KeyCode.C))
            gun.SetBulletBehaviour(BulletBehaviour.Crimson);
        if (Input.GetKeyDown(KeyCode.V))
            gun.SetBulletBehaviour(BulletBehaviour.All);
    }
    private void Update()
    {
        if (_state != GameState.Gameplay)
            return;

        _inputHandler.Update();
        Look();
        Move();
        Fire();

        SetGunMode();
    }
}
