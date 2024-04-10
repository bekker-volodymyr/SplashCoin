using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoudButton : MonoBehaviour
{
    [SerializeField] private Sprite soundOn;
    [SerializeField] private Sprite soundOff;
    [SerializeField] private AudioSource audioSource;

    private Image image;

    private bool isSoundOn = true;

    private void Start()
    {
        image = GetComponent<Image>();
        audioSource = FindAnyObjectByType<AudioSource>();
    }

    public void ToggleSound()
    {
        isSoundOn = !isSoundOn;
        image.sprite = isSoundOn ? soundOn : soundOff;
        audioSource.volume = isSoundOn ? 1 : 0;
        Debug.Log("Sound On: " +  isSoundOn);
    }
}
