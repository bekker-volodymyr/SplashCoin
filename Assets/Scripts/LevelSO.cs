using UnityEngine;

[CreateAssetMenu(fileName = "LevelData", menuName = "Levels")]
public class LevelSO : ScriptableObject
{
    public float platformSpeed;
    public int coinsCount;

    public bool bigObstacle;
    public bool smallObstacle1;
    public bool smallObstacle2;
}
