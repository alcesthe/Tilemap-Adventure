using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPickup : MonoBehaviour
{
    [SerializeField] int value = 1;
    [SerializeField] AudioClip coinSound;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        SoundManager.PlaySound(coinSound);
        GameManager.instance.coinCollected += value;
        Destroy(gameObject);
    }
}
