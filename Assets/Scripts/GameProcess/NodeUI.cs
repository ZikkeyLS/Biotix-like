using UnityEngine;

public class NodeUI : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _main;
    [SerializeField] private SpriteRenderer _selectionCircle;
    [SerializeField] private TMPro.TMP_Text _count;

    [SerializeField] private UnitPreset[] _presets;

    public Color GetColorByType(UnitType type)
    {
        for (int i = 0; i < _presets.Length; i++)
        {
            UnitPreset preset = _presets[i];

            if (preset.Type != type)
                continue;

            return preset.UnitColor;
        }

        return Color.white;
    }

    public void UpdateVisual(UnitType type)
    {
        Color color = GetColorByType(type);

        _main.color = color;
        _selectionCircle.color = color;
        _count.color = color;
    }

    public void UpdateText(int value)
    {
        _count.text = value.ToString();
    }
}
