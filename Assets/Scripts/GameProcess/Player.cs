using System.Collections.Generic;
using UnityEngine;

public class Player : UnitBase
{
    [SerializeField] private PlayerInput _input;

    private bool _clicked = false;
    private List<Node> _selectedNodes = new();

    public static Player Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        OnUpdate();

        if (_input.OnTouchBegan)
        {
            _clicked = true;
            return;
        }

        if (_input.OnTouchExit)
        {
            Attack();

            foreach (Node node in _selectedNodes)
                node.SetLinked(false);

            _selectedNodes.Clear();
            _clicked = false;
            return;
        }
    }

    public void Attack()
    {
        if (_selectedNodes.Count < 2)
            return;

        Node lastNode = _selectedNodes[_selectedNodes.Count - 1];

        foreach (Node node in _selectedNodes)
            if (node != lastNode && node.Unit == UnitType.Player)
                AddUnits(node, lastNode);
    }

    public void ProcessNode(Node node)
    {
        if (!_clicked)
            return;

        if (_selectedNodes.Contains(node))
        {
            _selectedNodes.Remove(node);
        }
        else
        {
            _selectedNodes.Add(node);
            node.SetLinked(true);
        }
    }
}
