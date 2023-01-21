using UnityEngine;
using UnityEngine.UI;

public class LevelsUI : MonoBehaviour
{
    [SerializeField] private Button[] _levels;
    [SerializeField] private Sprite _lockedImage;
    [SerializeField] private Color _completedColor;

    private void Start()
    {
        CalculateLevelsUI();
    }

    private void CalculateLevelsUI()
    {
        int levelsCompleted = PlayerData.Instance.LevelsCompleted;

        for (int i = 0; i < _levels.Length; i++)
            ProcessElement(i, levelsCompleted);
    }

    private void ProcessElement(int i, int levelsCompleted)
    {
        Button level = _levels[i];

        if (i + 1 < levelsCompleted)
        {
            level.image.color = _completedColor;
            return;
        }

        if (i > levelsCompleted)
        {
            level.enabled = false;
            level.image.sprite = _lockedImage;
            return;
        }
    }
}
