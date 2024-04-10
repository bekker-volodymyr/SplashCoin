using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public int currentLevel;
    [SerializeField] private LevelSO[] levelsData;

    private Vector3 targetPos = new Vector3(0, 3.39f, 0);
    private Vector3 platformStartPos;

    #region Level Objects

    private Platform platform;
    private GameObject coin;
    private GameObject bigObstacle = null;
    private GameObject smallObstacle1 = null;
    private GameObject smallObstacle2 = null;

    #endregion

    #region Prefabs

    [SerializeField] private GameObject coinPrefab;
    [SerializeField] private GameObject platformPrefab;
    [SerializeField] private GameObject bigObstaclePrefab;
    [SerializeField] private GameObject smallObstacle1Prefab;
    [SerializeField] private GameObject smallObstacle2Prefab;
    
    #endregion

    [SerializeField] private UIManager uiManager;

    #region Stats values

    private int coinsTotal;
    private int coinsLeft;
    private int coinsCollected;
    private int perfectFlips = 0;
    private int coinsTotalCollected = 0;
    private int currentCoin = 0;

    #endregion

    [SerializeField] private EndLevelUIManager endLevelUiManager;

    private bool isEndLevel = false;

    private void Start()
    {
        Time.timeScale = 1f;
        LevelSetup();
    }

    private void Update()
    {
        if(!isEndLevel)
        {
            if (coin is null && currentCoin <= coinsTotal)
            {
                CreateCoin();
            }

            if (coin.transform.position.y >= targetPos.y && Mathf.Abs(platform.transform.position.x - platformStartPos.x) < 0.55f)
            {
                CollectCoin();
                coin = null;
                if(Mathf.Abs(platform.transform.position.x - platformStartPos.x) < 0.1f) perfectFlips++;
            }
            else if(coin.transform.position.y >= targetPos.y && Mathf.Abs(platform.transform.position.x - platformStartPos.x) > 0.55f)
            {
                DestroyCoin();
                CoinFail();
            }

            if(currentCoin > coinsTotal)
            {
                isEndLevel = true;
                EndLevel();
            }
        }
    }

    public void FlipCoin()
    {
        coinsLeft--;
    }

    public void CollectCoin()
    {
        platform.AddCoin(coin);

        uiManager.SetCoinCollected(true, currentCoin);

        coinsCollected++;
    }

    private void LevelSetup()
    {
        DestroyCoin();

        platform = Instantiate(platformPrefab).GetComponent<Platform>();
        platformStartPos = platform.transform.position;

        platform.speed = levelsData[currentLevel - 1].platformSpeed;
        coinsTotal = levelsData[currentLevel - 1].coinsCount;

        currentCoin = 0;
        CreateCoin();
        
        coinsLeft = coinsTotal;
        perfectFlips = 0;
        coinsCollected = 0;

        uiManager.SetUpPanel(coinsTotal);

        if (levelsData[currentLevel - 1].bigObstacle == true)
        {
            bigObstacle = Instantiate(bigObstaclePrefab);
        }

        if (levelsData[currentLevel - 1].smallObstacle1 == true)
        {
            smallObstacle1 = Instantiate(smallObstacle1Prefab);
        }

        if (levelsData[currentLevel - 1].smallObstacle2 == true)
        {
            smallObstacle2 = Instantiate(smallObstacle2Prefab);
        }
    }

    public void EndLevel()
    {
        Time.timeScale = 0f;
        DestroyObjects();
        endLevelUiManager.ShowEndLevelMenu(currentLevel, coinsCollected, coinsTotal, perfectFlips, coinsTotalCollected);
    }

    public void NextLevel()
    {
        coinsTotalCollected += coinsCollected;

        currentLevel++;

        LevelSetup();

        isEndLevel = false;
        Time.timeScale = 1f;
    }

    public void Replay()
    {
        LevelSetup();

        isEndLevel = false;
        Time.timeScale = 1f;
    }

    public void ReplayAll()
    {
        currentLevel = 1;

        LevelSetup();

        isEndLevel = false;
        Time.timeScale = 1f;
    }

    private void DestroyObjects()
    {
        Destroy(platform.gameObject);

        if (bigObstacle != null)
        {
            Destroy(bigObstacle.gameObject);
            bigObstacle = null;
        }

        if (smallObstacle1 != null)
        {
            Destroy(smallObstacle1);
            smallObstacle1 = null;
        }

        if (smallObstacle2 != null)
        {
            Destroy(smallObstacle2);
            smallObstacle2 = null;
        }
    }

    public void CreateCoin()
    {
        currentCoin++;
        coin = Instantiate(coinPrefab);
    }

    public void DestroyCoin()
    {
        if (coin != null)
        {
            Destroy(coin);
            coin = null;
        }
    }

    public void CoinFail()
    {
        uiManager.SetCoinCollected(false, currentCoin);
    }

    public void CheckEndLevel()
    {
        if(coinsLeft == 0)
        {
            isEndLevel = true;
            EndLevel();
        }
    }
}
