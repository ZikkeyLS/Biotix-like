using UnityEngine;

public class PlayerData : MonoBehaviour
{
    private int _levelsCompleted;
    public int LevelsCompleted => _levelsCompleted;

    private void OnApplicationQuit()
    {
        SetCompletedLevels();
    }

    public void Initialize()
    {
        GetCompletedLevels();
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
