using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartUI : MonoBehaviour
{
    private void Awake()
    {
        // Start Button
        transform.Find("Start Button").GetComponent<Button>().onClick.AddListener(delegate
        {
            Loader.Load(Loader.Scene.Level_1);
        });

        // Start Button
        transform.Find("Quit Button").GetComponent<Button>().onClick.AddListener(delegate
        {
            Loader.LoadExit();
        });
    }
}
