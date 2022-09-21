using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    private float fixedDeltaTime;

    public float playerHealth = 3;
    public int coinCollected = 0;

    public bool isAlive = true;
    public bool isWin = false;
    public bool isPausing = false;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Update()
    {
        if (playerHealth <= 0)
        {
            isAlive = false;
        }

        if (CrossPlatformInputManager.GetButtonDown("Cancel"))
        {
            if (!isPausing)
            {
                PauseGame();
            }
            else
            {
                ResumeGame();
            }
        }
    }

    private void Start()
    {
        fixedDeltaTime = Time.fixedDeltaTime;
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
        Time.fixedDeltaTime = 0;
        isPausing = true;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
        Time.fixedDeltaTime = fixedDeltaTime;
        isPausing = false;
    }

    public void GameStateReset()
    {
        playerHealth = 3;
        coinCollected = 0;

        isAlive = true;
        isWin = false;
    }
}
