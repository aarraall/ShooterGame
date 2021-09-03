using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
public class GameInstaller : MonoInstaller
{
    [SerializeField]
    GameManager _gameManager;
    [SerializeField]
    PoolManager poolManager;

    [SerializeField]
    PlayerController playerController;
    public override void InstallBindings()
    {
        Container.BindInstance(_gameManager).AsSingle();
        Container.BindInstance(poolManager);
        Container.BindInstance(playerController).AsSingle();
    }
}
