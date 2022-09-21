using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelUI : MonoBehaviour
{
    [Header("Panel")]
    [SerializeField] GameObject deadPanel;
    [SerializeField] GameObject winPanel;
    [SerializeField] GameObject pausePanel;

    [Header("Lives")]
    [SerializeField] Sprite heartVisible;
    [SerializeField] Sprite heartInvisible;
    [SerializeField] GameObject[] heart;

    [Header("Coins")]
    [SerializeField] Text coinText;


    private void Awake()
    {
        // Try Again Button (Lose)
        deadPanel.transform.Find("Try Again Button").GetComponent<Button>().onClick.AddListener(delegate
        {
            Loader.Load(Loader.Scene.Start);
        });

        // Try Again Button (Win)
        winPanel.transform.Find("Try Again Button").GetComponent<Button>().onClick.AddListener(delegate
        {
            Loader.Load(Loader.Scene.Start);
        });

        // Back Button (Pause)
        pausePanel.transform.Find("Buttons/Back Button").GetComponent<Button>().onClick.AddListener(delegate
        {
            GameManager.instance.ResumeGame();
            Loader.Load(Loader.Scene.Start);
        });

        // Resume Button (Pause)
        pausePanel.transform.Find("Buttons/Resume Button").GetComponent<Button>().onClick.AddListener(delegate
        {
            GameManager.instance.ResumeGame();
        });
    }

    // Update is called once per frame
    void Update()
    {
        coinText.text = "X " + GameManager.instance.coinCollected.ToString();
        UpdateHeartStatus();
        deadPanel.SetActive(!GameManager.instance.isAlive);
        pausePanel.SetActive(GameManager.instance.isPausing);
        winPanel.SetActive(GameManager.instance.isWin);
    }

    private void UpdateHeartStatus()
    {
        for (int i = 1; i <= 3; i++)
        {
            if (i <= GameManager.instance.playerHealth)
            {
                heart[i - 1].GetComponent<Image>().sprite = heartVisible;
            }
            else
            {
                heart[i - 1].GetComponent<Image>().sprite = heartInvisible;
            }
        }
    }
}
