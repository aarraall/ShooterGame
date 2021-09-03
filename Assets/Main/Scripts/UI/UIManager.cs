using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class UIManager : MonoBehaviour
{
    GameManager _gameManager;

    [SerializeField]
    List<Page> pages = new List<Page>();

    [Inject]
    private void OnInstall(GameManager gameManager)
    {
        _gameManager = gameManager;
        _gameManager.OnGameStateChanged += CustomUpdate;
    }

    private void Initialize()
    {
        pages.ForEach(page => page.Initialize());
    }

    private void Show(int index)
    {
        pages[index].Show();
    }

    private void HideAll()
    {
        pages.ForEach(page => page.Hide());
    }
    private void CustomUpdate(GameState state)
    {
        HideAll();
        switch (state)
        {
            case GameState.Loading:
                Initialize();
                Show(0);
                break;
            case GameState.Idle:
                Show(1);
                break;
            case GameState.Gameplay:
                Show(2);
                break;
            case GameState.Win:
                Show(3);
                break;
            case GameState.Lose:
                Show(4);
                break;
        }
    }
}
