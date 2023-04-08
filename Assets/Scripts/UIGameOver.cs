using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using TMPro;

public class UIGameOver : MonoBehaviour
{

    public int displayerPoints;
    public TextMeshProUGUI pointsUI;

    // Start is called before the first frame update
    void Start()
    {
        GameManager.Instance.OnGameStateUpdated.AddListener(GameStateUpdated);
        
    }
    private void GameStateUpdated(GameManager.GameState newState)
    {
        if(newState == GameManager.GameState.GameOver)
        {
            displayerPoints = 0;
            StartCoroutine(DisplayPointsCoroutine());
        }
    }
    private void OnDestroy()
    {
        GameManager.Instance.OnGameStateUpdated.RemoveListener(GameStateUpdated);
    }

    IEnumerator DisplayPointsCoroutine()
    {
        while(displayerPoints < GameManager.Instance.Points) 
        {
            displayerPoints++;
            pointsUI.text = displayerPoints.ToString();
            yield return new WaitForFixedUpdate();  
        }
        displayerPoints = GameManager.Instance.Points;
        pointsUI.text = displayerPoints.ToString();
        yield return null;
    }

    public void PlayAgainBTNClicked()
    {
        GameManager.Instance.RestartGame();
    }

    public void ExitGameBTNClicked()
    {
        GameManager.Instance.ExitGame();
    }

}
