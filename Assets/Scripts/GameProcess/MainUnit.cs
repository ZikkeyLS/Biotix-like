using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainUnit : MonoBehaviour
{
    [SerializeField] private UnitType _type;
    [SerializeField] private GameObject _unit;

    private List<TargetedPatricle> _units = new();

    protected virtual void Update()
    {
        foreach (TargetedPatricle patricle in _units.ToArray())
            TryUpdateUnit(patricle);
    }

    public void AddUnits(Node from, Node to)
    {
        int count = from.GetDecreasedValueByTwo();

        if(count > 0)
            StartCoroutine(AddUnit(count, from, to));
    }

    private IEnumerator AddUnit(int nextCount, Node from, Node to)
    {
        yield return new WaitForSeconds(GameState.Instance.GetUnitSpawnSpeed());

        from.DecreaseValue();

        SpriteRenderer unit = Instantiate(_unit, transform).GetComponent<SpriteRenderer>();
        unit.transform.position = GetClearVector(unit.transform, from.transform);
        unit.color = from.UI.GetColorByType(from.Unit);

        _units.Add(new TargetedPatricle(unit.transform, to));

        if (nextCount - 1 != 0 && from.GetValue() > 0)
            StartCoroutine(AddUnit(nextCount - 1, from, to));
    }

    public void TryUpdateUnit(TargetedPatricle unit)
    {
        unit.Unit.position = Vector3.MoveTowards(unit.Unit.position, UnitTarget(unit), Time.deltaTime * GameState.Instance.GetMoveSpeed());

        Vector2 unitPosition = unit.Unit.position;
        Vector2 targetPosition = unit.Target.transform.position;

        // we don't want to check Z axis.
        if(unitPosition == targetPosition)
            DeleteUnit(unit);
    }

    private Vector3 UnitTarget(TargetedPatricle unit)
    {
        return GetClearVector(unit.Unit, unit.Target.transform);
    }

    private Vector3 GetClearVector(Transform initial, Transform target)
    {
        Vector3 finalTarget = target.position;
        finalTarget.z = initial.position.z;

        return finalTarget;
    }

    public void DeleteUnit(TargetedPatricle patricle)
    {
        if (patricle.Target.Unit == _type)
        {
            patricle.Target.IncreaseValue();
        }
        else
        {
            patricle.Target.DecreaseValue();

            if (patricle.Target.GetValue() <= 0)
                patricle.Target.ChangeUnitType(_type);
        }


        Destroy(patricle.Unit.gameObject);
        _units.Remove(patricle);
    }
}
