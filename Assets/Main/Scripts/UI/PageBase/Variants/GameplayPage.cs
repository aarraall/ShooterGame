using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class GameplayPage : Page
{
    PlayerController _playerController;

    [SerializeField]
    GameObject helpBar;
    [SerializeField]
    Text firedText, magazineText, gunModeTxt, bulletModeTxt;


    bool helpBarToggle;
    [Inject]
    private void OnInstall(PlayerController playerController)
    {
        _playerController = playerController;
    }
    public override void Initialize()
    {
        SubscribeBulletEvents();
    }

    private void SubscribeBulletEvents()
    {
        _playerController.Gun.OnMagazineDataChange += OnMagazineDataChange;
        _playerController.Gun.OnFire += OnFire;
        _playerController.Gun.OnBulletModeChange += OnBulletModeChange;
        _playerController.Gun.OnGunModeChange += OnGunModeChange;
    }

    private void OnMagazineDataChange(int amount)
    {
        magazineText.text = "Magazine : " + amount.ToString();
    }
    private void OnFire(int amount)
    {
        firedText.text = "Last Fired Ammo : " + amount.ToString();
    }

    private void OnGunModeChange(GunBehaviour gunBehaviour)
    {
        gunModeTxt.text = "Gun mode : " + gunBehaviour.ToString();
    }
    private void OnBulletModeChange(BulletBehaviour bulletBehaviour)
    {
        bulletModeTxt.text = "Bullet mode : " + bulletBehaviour.ToString();
    }

    private void OpenHelp()
    {
        helpBarToggle = !helpBarToggle;
        helpBar.SetActive(helpBarToggle);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            OpenHelp();
        }
    }
}
