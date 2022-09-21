using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trophy : MonoBehaviour
{
    [SerializeField] AudioClip winSound;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        SoundManager.PlaySound(winSound);
        GameManager.instance.isWin = true;
    }
}
