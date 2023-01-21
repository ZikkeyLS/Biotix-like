using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class AI : UnitBase
{
    [Header("AI")]
    [SerializeField] private GameState _state;
    [SerializeField] private float _analizeDelayInSeconds = 5f;
    [SerializeField] private int _maximalAttackMargin = 8;
    [SerializeField] private int _helpMargin = 5;
     
    private void Awake()
    {
        StartCoroutine(AnalizeSituation());
    }

    protected void Update()
    {
        OnUpdate();
    }

    private IEnumerator AnalizeSituation()
    {
        yield return new WaitForSeconds(_analizeDelayInSeconds);

        int halfSum = 0;

        foreach (Node node in _state.AINodes)
            halfSum += node.GetDecreasedValueByTwo();

        Node attackNode = GetAttackNode(halfSum);
        Node defenceNode = GetDefenceNode();

        if (defenceNode != null)
            foreach (Node node in _state.AINodes)
                AddUnits(node, defenceNode);

        if (attackNode.GetValue() - halfSum <= _maximalAttackMargin)
            foreach (Node node in _state.AINodes)
                AddUnits(node, attackNode);

        StartCoroutine(AnalizeSituation());
    }

    private Node GetAttackNode(int halfSum)
    {
        Node playerNode = SortTypedNodes(_state.PlayerNodes, halfSum);
        Node neutralNode = SortTypedNodes(_state.NeutralNodes, halfSum);

        if (neutralNode == null)
            return playerNode;

        if (neutralNode != null)
        {
            if (playerNode.GetValue() <= neutralNode.GetValue() && halfSum > playerNode.GetValue())
                return playerNode;

            return neutralNode;
        }

        return null;
    }

    private Node GetDefenceNode()
    {
        Node defenceNode = GetMinimalNode(_state.AINodes);

        if (defenceNode.GetValue() < _helpMargin)
            return defenceNode;

        return null;
    }

    private Node SortTypedNodes(List<Node> nodes, int halfSum)
    {
        // Try get max possible node
        Node lastSortedNode = GetMaximalNode(nodes, halfSum);

        // Try get minimal node
        if (lastSortedNode == null)
            lastSortedNode = GetMinimalNode(nodes);

        return lastSortedNode;
    }

    private Node GetMaximalNode(List<Node> nodes, int halfSum)
    {
        Node sortedNode = null;
        int sortedValue = int.MaxValue;

        foreach (Node node in nodes)
            if (sortedValue < node.GetValue() && node.GetValue() <= halfSum)
            {
                sortedValue = node.GetValue();
                sortedNode = node;
            }

        return sortedNode;
    }

    private Node GetMinimalNode(List<Node> nodes)
    {
        Node sortedNode = null;
        int sortedValue = int.MaxValue;

        foreach (Node node in nodes)
            if (sortedValue >= node.GetValue())
            {
                sortedValue = node.GetValue();
                sortedNode = node;
            }

        return sortedNode;
    }
}
