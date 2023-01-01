using UnityEngine;

public class PlayerData : MonoBehaviour
{
    public static PlayerData Instance { get; private set; }

    private int _levelsCompleted;
    public int LevelsCompleted => _levelsCompleted;

    private void Awake()
    {
        if (Instance != null)
            Destroy(gameObject);
        else
        {
            Instance = this;
            DontDestroyOnLoad(this);

            _levelsCompleted = PlayerPrefs.GetInt("completedLevels");
        }
    }

    private void OnApplicationQuit()
    {
        PlayerPrefs.SetInt("completedLevels", _levelsCompleted);
    }

    public void TryAddLevel(int id)
    {
        if (_levelsCompleted < id) 
            _levelsCompleted += 1;
    }
}
