using UnityEngine;

public class Coin : MonoBehaviour
{
    private LevelManager levelManager;

    private float speed = 7f;
    public bool flipping = false;
    
    void Start()
    {
        levelManager = FindFirstObjectByType<LevelManager>();
    }

    void Update()
    {
        if (Input.touchCount > 0)
        {
            for (int i = 0; i < Input.touchCount; i++)
            {
                Touch touch = Input.GetTouch(i);

                // Check if this touch phase began
                if (touch.phase == TouchPhase.Began)
                {
                    // Convert touch position to world space
                    Vector3 touchPosition = Camera.main.ScreenToWorldPoint(touch.position);

                    // Perform a 2D physics raycast to check for collisions with 2D objects
                    RaycastHit2D hit = Physics2D.Raycast(touchPosition, Vector2.zero);

                    // Check if the raycast hit any collider
                    if (hit.collider != null)
                    {
                        // Check if the collider belongs to the 2D object you want to detect
                        if (hit.collider.CompareTag("Coin")) // Replace "Your2DObjectTag" with the tag of your 2D object
                        {
                            flipping = true;
                            levelManager.FlipCoin();
                        }
                    }
                }
            }
        }

        // if (Input.anyKeyDown && !flipping)
        // {
        //     flipping = true;
        //     levelManager.FlipCoin();
        // }
    }

    private void LateUpdate()
    {
        if (flipping)
        {
            transform.Translate(Vector3.up * speed * Time.deltaTime);
        }
    }
}
