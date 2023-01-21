using UnityEngine;

public class PlayerData : MonoBehaviour
{
    private int _levelsCompleted;
    public int LevelsCompleted => _levelsCompleted;

    public static PlayerData Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this);
            GetCompletedLevels();
            return;
        }
    }

    private void OnApplicationQuit()
    {
        SetCompletedLevels();
    }

    private void SetCompletedLevels()
    {
        PlayerPrefs.SetInt("completedLevels", _levelsCompleted);
    }

    private void GetCompletedLevels()
    {
        _levelsCompleted = PlayerPrefs.GetInt("completedLevels");
    }

    public void TryAddLevel(int id)
    {
        if (_levelsCompleted < id) 
            _levelsCompleted += 1;
    }
}
