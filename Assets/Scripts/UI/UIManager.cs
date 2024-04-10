using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    private GameObject coinsPanel;
    [SerializeField] private GameObject coinsPanelPrefab;
    [SerializeField] private GameObject coinUiPrefab;

    private List<GameObject> coins;

    public void SetUpPanel(int coinsCount)
    {
        if(coinsPanel != null)
        {
            Destroy(coinsPanel);
        }

        if(coins is null)
        {
            coins = new List<GameObject>();
        }
        else
        {
            coins.Clear();
        }

        coinsPanel = Instantiate(coinsPanelPrefab);
        coinsPanel.GetComponent<RectTransform>().SetParent(transform, false);

        for (int i = 0; i < coinsCount; i++)
        {
            GameObject coin = Instantiate(coinUiPrefab);
            coins.Add(coin);
            coin.GetComponent<RectTransform>().SetParent(coinsPanel.transform, false);
        }
    }

    public void SetCoinCollected(bool collected, int coinNumber)
    {
        Debug.Log(coinNumber);
        coins[coinNumber - 1].GetComponent<CoinUI>().SetCollected(collected);
    }
}
