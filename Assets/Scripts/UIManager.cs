using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI collectedCoins;
    [SerializeField] private TextMeshProUGUI leftCoins;

    public void UpdateCollectedCoins(int value)
    {
        collectedCoins.text = $"Collected\n{value}";
    }

    public void UpdateLeftCoins(int value)
    {
        leftCoins.text = $"Left\n{value}";
    }
}
