using UnityEngine;
using UnityEngine.SceneManagement;

public class Level : MonoBehaviour
{
    [SerializeField] private int _level = 1;
    [SerializeField] private int _maxLevels = 5;
    [SerializeField] private string _menuName = "MainMenu";

    [SerializeField] private GameScreen _win;
    [SerializeField] private GameScreen _lose;
    [SerializeField] private GameScreen _pause;

    private const float InitialTimeScale = 1;
    private const float PausedTimeScale = 0;
    private const float NextLevelOffset = 1;

    public void Win()
    {
        _win.ChangeState();
        GameLoader.Instance.Data.TryAddLevel(_level);
        ReverseTimeScale();
    }

    public void Lose()
    {
        _lose.ChangeState();
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
        _pause.ChangeState();
    }

    public void ContinueGame()
    {
        ReverseTimeScale();
        _pause.ChangeState();
    }

    public void Next()
    {
        if (_level == _maxLevels)
            return;

        ReverseTimeScale();
        SceneManager.LoadScene($"Level{_level + NextLevelOffset}");
    }

    public void ReverseTimeScale()
    {
        Time.timeScale = Time.timeScale == PausedTimeScale ? InitialTimeScale : PausedTimeScale;
    }
}
