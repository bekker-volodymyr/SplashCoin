using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayBtnHandler : MonoBehaviour
{
    public void PlayBtn_Click()
    {
        SceneManager.LoadScene(1);
    }
}
