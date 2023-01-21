using System.Collections.Generic;
using UnityEngine;

public class Player : UnitBase
{
    public static Player Instance { get; private set; }

    [SerializeField] private PlayerInput _input;

    private bool _clicked = false;
    private List<Node> _selectedNodes = new();

    private void Awake()
    {
        Instance = this;
    }

    protected void Update()
    {
        OnUpdate();

        if (_input.OnTouchBegan)
        {
            _clicked = true;
        }
        else if (_input.OnTouchExit)
        {
            Attack();

            foreach (Node node in _selectedNodes)
                node.SetLinked(false);

            _selectedNodes.Clear();
            _clicked = false;
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
