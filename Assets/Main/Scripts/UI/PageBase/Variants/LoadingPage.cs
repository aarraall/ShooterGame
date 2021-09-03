using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class LoadingPage : Page
{
    [SerializeField]
    Slider loadingSlider;

    [Inject]
    GameManager _gameManager;

    bool isLoading;
    float timeCounter;

    public override void Initialize()
    {
        isLoading = false;
        timeCounter = 0;
    }
    public override void Show()
    {
        base.Show();
        isLoading = true;
    }
    public override void Hide()
    {
        base.Hide();
    }

    private void Update()
    {
        if (isLoading)
        {
            timeCounter += Time.deltaTime;

            if (timeCounter >= 2f)
            {
                isLoading = false;
                _gameManager.SetGameState(GameState.Idle);
            }
            else
                loadingSlider.value += .5f * Time.deltaTime;
        }
    }

    
}
