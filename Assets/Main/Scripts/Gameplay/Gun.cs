using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

using Random = UnityEngine.Random;

public class Gun : MonoBehaviour
{
    [SerializeField]
    Transform firePoint;

    PoolManager _poolManager;

    public List<Bullet> Magazine { get; private set; }
    public GunBehaviour GunBehaviour { get; private set; }
    public BulletBehaviour BulletBehaviour { get; private set; }

    public event Action<int> OnMagazineDataChange;
    public event Action<int> OnFire;
    public event Action<GunBehaviour> OnGunModeChange;
    public event Action<BulletBehaviour> OnBulletModeChange;

    [Inject]
    private void OnInstall(PoolManager poolManager)
    {
        _poolManager = poolManager;
    }
    public void Initialize()
    {
        GunBehaviour = GunBehaviour.Pistol;
        Magazine = new List<Bullet>();
    }
    public void PostInitialize()
    {
        OnFire?.Invoke(0);
        ReloadMagazine();
        SetGunMode(GunBehaviour.Pistol);
        SetBulletBehaviour(BulletBehaviour.Delayed);
    }

    public void Fire()
    {
        switch (GunBehaviour)
        {
            case GunBehaviour.Shotgun:
                OnShotgunFire();
                break;
            case GunBehaviour.Pistol:
                OnPistolFire();
                break;
        }
    }
    private void AddBullet(Bullet bullet)
    {
        Magazine.Add(bullet);
        OnMagazineDataChange?.Invoke(Magazine.Count);
    }
    private void RemoveBullet(Bullet bullet)
    {
        Magazine.Remove(bullet);
        OnMagazineDataChange?.Invoke(Magazine.Count);
    }
    private Bullet GetBullet()
    {
        Magazine[Magazine.Count - 1].transform.position = firePoint.transform.position;
        Magazine[Magazine.Count - 1].transform.LookAt(firePoint);
        return Magazine[Magazine.Count - 1];

    }
    private void OnShotgunFire()
    {
        if (Magazine.Count - GameConfig.SHOTGUN_FIRE_RATE < 0)
        {
            ReloadMagazine();
        }

        for (int i = 0; i < GameConfig.SHOTGUN_FIRE_RATE; i++)
        {
            Bullet tempBullet = GetBullet();
            tempBullet.Fire(BulletBehaviour, firePoint.forward, true);
            RemoveBullet(tempBullet);
        }

        if (Magazine.Count == 0)
        {
            ReloadMagazine();
        }
        OnFire?.Invoke(GameConfig.SHOTGUN_FIRE_RATE);
    }
    private void OnPistolFire()
    {
        Bullet tempBullet = GetBullet();
        tempBullet.Fire(BulletBehaviour, firePoint.forward, false);
        RemoveBullet(tempBullet);
        OnFire?.Invoke(1);
        if (Magazine.Count == 0)
        {
            ReloadMagazine();
        }
    }
    private void ReloadMagazine()
    {
        int tempCount = Magazine.Count;
        for (int i = 0; i < GameConfig.MAGAZINE_LIMIT - tempCount; i++)
        {
            Bullet tmpBullet = (Bullet)_poolManager.GetPoolObject(0, transform);
            AddBullet(tmpBullet);
        }
    }
    public void LookAt(Quaternion rotation)
    {
        transform.localRotation = rotation;
    }
    public void SetGunMode(GunBehaviour behaviour)
    {
        GunBehaviour = behaviour;
        OnGunModeChange?.Invoke(GunBehaviour);
    }
    public void SetBulletBehaviour(BulletBehaviour behaviour)
    {
        BulletBehaviour = behaviour;
        OnBulletModeChange?.Invoke(BulletBehaviour);
    }
    public void SetDefault()
    {
        Initialize();
    }
}

public enum GunBehaviour
{
    Shotgun,
    Pistol
}
