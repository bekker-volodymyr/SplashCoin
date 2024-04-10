using UnityEngine;

public class Obstacle : MonoBehaviour
{
    private LevelManager levelManager;

    [SerializeField] private bool movingRight;
    public float speed;
    private float screenWidth;
    private float obstacleWidth;

    [SerializeField] private GameObject child;

    private void Start()
    {
        levelManager = FindObjectOfType<LevelManager>();

        screenWidth = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0f, 0f)).x;

        SpriteRenderer spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        Vector3 screenPos1 = Camera.main.WorldToScreenPoint(spriteRenderer.bounds.min);
        Vector3 screenPos2 = Camera.main.WorldToScreenPoint(spriteRenderer.bounds.max);

        float widthInPixels = Mathf.Abs(screenPos2.x - screenPos1.x);

        obstacleWidth = Camera.main.ScreenToWorldPoint(new Vector3(widthInPixels, 0f, 0f)).x;

        Debug.Log("platformWidth: " + obstacleWidth);
    }

    void Update()
    {
        // Move the object right or left based on current direction
        if (movingRight)
            transform.Translate(Vector3.right * speed * Time.deltaTime);
        else
            transform.Translate(Vector3.left * speed * Time.deltaTime);

        // Check if the object has reached the screen edge
        if (transform.position.x >= screenWidth + obstacleWidth / 3)
        {
            // If at the right edge, change direction to left
            movingRight = false;
        }
        else if (transform.position.x <= -screenWidth - obstacleWidth / 3)
        {
            // If at the left edge, change direction to right
            movingRight = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Coin"))
        {
            Debug.Log("collision " + collision);
            levelManager.DestroyCoin();
            levelManager.CoinFail();
            //levelManager.CheckEndLevel();
        }
    }
}
