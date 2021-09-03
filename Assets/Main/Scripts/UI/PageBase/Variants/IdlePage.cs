using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class IdlePage : Page
{
    GameManager _gameManager;
    [Inject]
    private void OnInstall(GameManager gameManager)
    {
        _gameManager = gameManager;
    }
    public override void Initialize()
    {

    }
    public override void Show()
    {
        base.Show();
    }
    public override void Hide()
    {
        base.Hide();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _gameManager.SetGameState(GameState.Gameplay);
        }
    }

    
}
