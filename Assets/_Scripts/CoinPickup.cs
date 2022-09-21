using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPickup : MonoBehaviour
{
    [SerializeField] float value = 1;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameManager.instance.coinCollected += 5;
        Destroy(gameObject);
    }
}
