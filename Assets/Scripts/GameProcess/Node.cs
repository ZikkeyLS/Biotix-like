using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class Node : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public static Action<Node> OnNodeChanged;

    [SerializeField] private UnitType _unit = UnitType.None;
    [SerializeField] private int _value;
    [SerializeField] private GameObject _selectionCircle;

    [Header("Linked Resources")]
    [SerializeField] private NodeUI _ui;

    private bool _entered;
    private bool _linked;

    public UnitType Unit => _unit;
    public NodeUI UI => _ui;
    public int GetValue() => _value;

    private void Start()
    {
        _ui.UpdateVisual(_unit);
        _ui.UpdateText(_value);

        StartCoroutine(InceaseValue());
    }

    private void Update()
    {
        if(_entered && !_linked)
            Player.Instance.ProcessNode(this);
    }

    private IEnumerator InceaseValue()
    {
        yield return new WaitUntil(() => Unit != UnitType.None);

        _value += GameState.Instance.GetIncreasePerSecond();
        _ui.UpdateText(_value);

        yield return new WaitForSeconds(1);

        StartCoroutine(InceaseValue());
    }

    public void SetLinked(bool value)
    {
        _linked = value;
        _selectionCircle.SetActive(value);
    }

    public void IncreaseValue()
    {
        _value += 1;
        _ui.UpdateText(_value);
    }

    public void DecreaseValue()
    {
        _value -= 1;
        _value = Mathf.Clamp(_value, 0, int.MaxValue);

        _ui.UpdateText(_value);
    }

    public int GetDecreasedValueByTwo()
    {
        int initialValue = _value;
        int tempValue;

        if (_value <= 1)
            return 0;
        else if(_value % 2 == 0)
            tempValue = _value / 2;
        else
            tempValue = ((initialValue - 1) / 2) + 1;

        return tempValue;
    }

    public void ChangeUnitType(UnitType type)
    {
        _unit = type;
        _ui.UpdateVisual(type);
        OnNodeChanged.Invoke(this);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        _entered = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _entered = false;
    }
}
