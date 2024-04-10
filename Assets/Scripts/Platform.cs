using UnityEngine;

public class Platform : MonoBehaviour
{
    private LevelManager levelManager;

    public float speed = 0.5f;
    private bool movingRight = false;
    private float screenWidth;
    private float platformWidth;

    [SerializeField] private GameObject child;

    private void Start()
    {
        levelManager = FindAnyObjectByType<LevelManager>();

        screenWidth = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0f, 0f)).x / 2;

        SpriteRenderer spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        Vector3 screenPos1 = Camera.main.WorldToScreenPoint(spriteRenderer.bounds.min);
        Vector3 screenPos2 = Camera.main.WorldToScreenPoint(spriteRenderer.bounds.max);

        float widthInPixels = Mathf.Abs(screenPos2.x - screenPos1.x);

        platformWidth = -Camera.main.ScreenToWorldPoint(new Vector3(widthInPixels, 0f, 0f)).x;
    }

    void Update()
    {
        if (movingRight)
            transform.Translate(Vector3.right * speed * Time.deltaTime);
        else
            transform.Translate(Vector3.left * speed * Time.deltaTime);

        if (transform.position.x >= screenWidth)
        {
            movingRight = false;
        }
        else if (transform.position.x <= -screenWidth)
        {
            movingRight = true;
        }
    }

    public void AddCoin(GameObject coin)
    {
        coin.transform.parent = child.transform;
        Destroy(coin.GetComponent<Coin>());
    }
}
