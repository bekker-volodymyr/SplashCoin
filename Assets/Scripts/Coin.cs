using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Coin : MonoBehaviour
{
    private LevelManager levelManager;

    private float speed = 5f;
    private bool flipping = false;
    
    void Start()
    {
        levelManager = FindFirstObjectByType<LevelManager>();
    }

    void Update()
    {
        if(Input.anyKeyDown)
        {
            flipping = true;
            levelManager.FlipCoin();
        }
    }

    private void LateUpdate()
    {
        if (flipping)
        {
            transform.Translate(Vector3.up * speed * Time.deltaTime);
        }
    }
}
