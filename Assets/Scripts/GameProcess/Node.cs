using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class Node : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public static Action<Node> OnNodeChanged;

    [SerializeField] private UnitType _unit = UnitType.None;
    [SerializeField] private GameObject _selectionCircle;
    [SerializeField] private int _value;
    [SerializeField] private int _increaseDelay = 1;

    [Header("Linked Resources")]
    [SerializeField] private NodeUI _ui;

    private bool _entered;
    private bool _linked;

    private const int ParityOffset = 1;
    private const int ParityMinInitialValue = 2;
    private const int ParityMinValue = 0;

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

        yield return new WaitForSeconds(_increaseDelay);

        StartCoroutine(InceaseValue());
    }

    public void SetLinked(bool value)
    {
        _linked = value;
        _selectionCircle.SetActive(value);
    }

    public void IncreaseValue()
    {
        ++_value;
        _ui.UpdateText(_value);
    }

    public void DecreaseValue()
    {
        --_value;
        _value = Mathf.Clamp(_value, 0, int.MaxValue);
        _ui.UpdateText(_value);
    }

    public int GetDecreasedValueByTwo()
    {
        if (_value < ParityMinInitialValue)
            return ParityMinValue;
        else if(GetParity(_value))
            return DevideByTwo(_value);
        else
            return DevideByTwo(_value - ParityOffset) + ParityOffset;
    }

    private bool GetParity(int value) => value % 2 == 0;
    private int DevideByTwo(int value) => value / 2;

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
