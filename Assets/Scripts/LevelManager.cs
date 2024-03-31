using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private Platform platform;
    [SerializeField] private GameObject coin;

    [SerializeField] private Vector3 targetPos;

    [SerializeField] private GameObject coinPrefab;

    [SerializeField] private UIManager uiManager;
    [SerializeField] private int coinsTotal;
    private int coinsLeft;
    private int coinsCollected;

    private void Start()
    {
        coin = Instantiate(coinPrefab);
        coinsLeft = coinsTotal;
        coinsCollected = 0;

        uiManager.UpdateCollectedCoins(coinsCollected);
        uiManager.UpdateLeftCoins(coinsLeft);
    }

    private void Update()
    {
        if ((coin.transform.position - targetPos).sqrMagnitude < 0.01f)
        {
            if((platform.transform.position - targetPos).sqrMagnitude < 0.5f)
            {
                platform.AddCoin(coin);
            }
            else
            {
                Destroy(coin);
            }

            if (coinsLeft != 0)
                coin = Instantiate(coinPrefab);
            else
                EndLevel();
        }
    }

    public void FlipCoin()
    {
        coinsLeft--;
        uiManager.UpdateLeftCoins(coinsLeft);
    }

    public void CollectCoin()
    {
        coinsCollected++;
        uiManager.UpdateCollectedCoins(coinsCollected);

    }

    public void EndLevel()
    {
        Time.timeScale = 0f;
    }

}
