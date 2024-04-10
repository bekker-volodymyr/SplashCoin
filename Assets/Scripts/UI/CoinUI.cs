using UnityEngine;
using UnityEngine.UI;

public class CoinUI : MonoBehaviour
{
    [SerializeField] private Image image;
    [SerializeField] private GameObject failSprite;

    public void SetCollected(bool collected)
    {
        if(collected)
        {
            image.color = Color.white;
        }
        else
        {
            image.color = Color.white;
            failSprite.SetActive(true);
        }
    }
}
