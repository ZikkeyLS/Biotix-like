using UnityEngine;
using UnityEngine.SceneManagement;

public class Level : MonoBehaviour
{
    [SerializeField] private int _level = 1;
    [SerializeField] private int _maxLevels = 5;
    [SerializeField] private string _menuName = "MainMenu";

    [SerializeField] private GameObject _winScreen;
    [SerializeField] private GameObject _loseScreen;
    [SerializeField] private GameObject _pauseScreen;
    [SerializeField] private GameObject _pauseButton;

    public void Win()
    {
        _winScreen.SetActive(true);
        PlayerData.Instance.TryAddLevel(_level);
        ReverseTimeScale();
    }

    public void Lose()
    {
        _loseScreen.SetActive(true);
        ReverseTimeScale();
    }

    public void Restart()
    {
        ReverseTimeScale();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Quit()
    {
        ReverseTimeScale();
        SceneManager.LoadScene(_menuName);
    }

    public void PauseGame()
    {
        ReverseTimeScale();
        _pauseButton.SetActive(false);
        _pauseScreen.SetActive(true);
    }

    public void ContinueGame()
    {
        ReverseTimeScale();
        _pauseButton.SetActive(true);
        _pauseScreen.SetActive(false);
    }

    public void Next()
    {
        if (_level == _maxLevels)
            return;

        ReverseTimeScale();
        SceneManager.LoadScene($"Level{_level + 1}");
    }

    public void ReverseTimeScale()
    {
        Time.timeScale = Time.timeScale == 0 ? 1 : 0;
    }
}
