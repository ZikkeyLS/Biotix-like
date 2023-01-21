using UnityEngine;

[CreateAssetMenu(fileName = "Level", menuName = "Game/Configurations", order = 1)]
public class LevelConfig : ScriptableObject
{
    public int _levelIndex = 0;
    [Range(1f, 10f)] public float _difficulty = 1;
    [Range(1f, 10f)] public float _growDelay = 1f;
    [Range(1f, 10f)] public float _growValue = 1f;
    public Vector2[] _playerNodes;
    public Vector2[] _enemyNodes;
    public Vector2[] _neutralNodes;
}