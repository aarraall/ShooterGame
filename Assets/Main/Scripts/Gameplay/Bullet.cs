using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
public class Bullet : PoolObject
{
    [SerializeField]
    MeshRenderer meshRenderer;

    [SerializeField]
    Material defaultMaterial, crimsonMaterial;

    private BulletBehaviour behaviour;
    bool isFired;
    bool isDelayed;
    float delay, killTimer;
    Vector3 _direction;
    public override void Initialize()
    {

    }
    public override void Dismiss()
    {
        // TODO : particle can be spawned here

        SetDefault();
        base.Dismiss();
    }

    private void SetDefault()
    {
        isFired = false;
        isDelayed = false;
        delay = 0;
        killTimer = 0;
        meshRenderer.material = defaultMaterial;
        transform.rotation = Quaternion.Euler(0, 0, 0);
        transform.localScale = Vector3.one * GameConfig.DEFAULT_BULLET_SCALE;
    }
    public override void Spawn()
    {
        base.Spawn();
    }
    public void Fire(BulletBehaviour bulletBehaviour, Vector3 direction, bool isSpread)
    {
        behaviour = bulletBehaviour;
        gameObject.SetActive(true);
        _direction = direction;
        if (isSpread)
        {
            transform.LookAt(_direction);
            Quaternion tempRot = transform.rotation;
            transform.Rotate(tempRot.x + Random.Range(-15, 15), tempRot.y + Random.Range(-15, 15), tempRot.z);
        }
        else
        {
            transform.LookAt(_direction);
        }

        switch (behaviour)
        {
            case BulletBehaviour.Big:
                transform.localScale = Vector3.one * GameConfig.BIG_BULLET_SCALE;
                break;
            case BulletBehaviour.Crimson:
                meshRenderer.material = crimsonMaterial;
                break;
            case BulletBehaviour.All:
                transform.localScale = Vector3.one * GameConfig.BIG_BULLET_SCALE;
                meshRenderer.material = crimsonMaterial;
                break;
        }

        isFired = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        isFired = false;
        switch (behaviour)
        {
            case BulletBehaviour.Delayed:
                isDelayed = true;
                break;
            case BulletBehaviour.All:
                isDelayed = true;
                break;
            default:
                Dismiss();
                break;
        }
    }
    private void Update()
    {
        if (isFired)
        {
            killTimer += Time.deltaTime;
            if (killTimer >= GameConfig.BULLET_KILL_TIME)
                Dismiss();
            else
                transform.position += transform.forward * GameConfig.BULLET_SPEED * Time.deltaTime;
        }
        if (isDelayed)
        {
            delay += Time.deltaTime;
            if (delay >= 1)
                Dismiss();
        }
    }
}


public enum BulletBehaviour
{
    Delayed,
    Big,
    Crimson,
    All
}