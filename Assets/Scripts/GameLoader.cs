using UnityEngine;

public class GameLoader : MonoBehaviour
{
    [SerializeField] private LevelConfig[] _levels;
    [SerializeField] private PlayerData _data;

    public static GameLoader Instance { get; private set; }

    public LevelConfig[] Levels => _levels;
    public PlayerData Data => _data;

    private void Awake()
    {
        if(Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        _data.Initialize();
    }


}
