using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Platform : MonoBehaviour
{

    private LevelManager levelManager;

    public float speed = 0.5f; // Adjust this to control the speed of movement
    private bool movingRight = false;
    private float screenWidth;
    private float platformWidth;

    private void Start()
    {
        levelManager = FindAnyObjectByType<LevelManager>();

        screenWidth = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0f, 0f)).x;

        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        Vector3 screenPos1 = Camera.main.WorldToScreenPoint(spriteRenderer.bounds.min);
        Vector3 screenPos2 = Camera.main.WorldToScreenPoint(spriteRenderer.bounds.max);

        // Calculate the size of the object in pixels
        float widthInPixels = Mathf.Abs(screenPos2.x - screenPos1.x);
        float heightInPixels = Mathf.Abs(screenPos2.y - screenPos1.y);

        // Output the size of the object in pixels
        Debug.Log("Width in pixels: " + widthInPixels + ", Height in pixels: " + heightInPixels);

        platformWidth = Camera.main.ScreenToWorldPoint(new Vector3(widthInPixels, 0f, 0f)).x;

        Debug.Log("platformWidth: " + platformWidth);
    }

    void Update()
    {
        // Move the object right or left based on current direction
        if (movingRight)
            transform.Translate(Vector3.right * speed * Time.deltaTime);
        else
            transform.Translate(Vector3.left * speed * Time.deltaTime);

        // Check if the object has reached the screen edge
        if (transform.position.x >= screenWidth + platformWidth / 2)
        {
            // If at the right edge, change direction to left
            movingRight = false;
        }
        else if (transform.position.x <= -screenWidth - platformWidth / 2)
        {
            // If at the left edge, change direction to right
            movingRight = true;
        }
    }

    public void AddCoin(GameObject coin)
    {
        coin.transform.parent = transform;
        levelManager.CollectCoin();
        Destroy(coin.GetComponent<Coin>());
    }
}
