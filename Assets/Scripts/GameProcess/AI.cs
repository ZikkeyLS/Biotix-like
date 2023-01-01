using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class AI : MainUnit
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

    protected override void Update()
    {
        base.Update();
    }

    private IEnumerator AnalizeSituation()
    {
        yield return new WaitForSeconds(_analizeDelayInSeconds);

        int halfSum = 0;

        foreach (Node node in _state.AINodes)
            halfSum += node.GetDecreasedValueByTwo();

        Node attackNode = GetAttackNode(halfSum);
        Node defenceNode = GetDefenceNode(halfSum);

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
        else if (neutralNode != null)
        {
            if (playerNode.GetValue() <= neutralNode.GetValue() && halfSum > playerNode.GetValue())
                return playerNode;
            else
                return neutralNode;
        }
        else
            return null;
    }

    private Node GetDefenceNode(int halfSum)
    {
        Node defenceNode = GetMinimalNode(_state.AINodes);

        if (defenceNode.GetValue() < _helpMargin)
            return defenceNode;
        else
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
