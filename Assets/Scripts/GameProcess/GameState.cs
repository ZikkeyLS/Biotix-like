using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(Level))]
public class GameState : MonoBehaviour
{
    public static GameState Instance { get; private set; }

    public List<Node> AINodes { get; private set; } = new();
    public List<Node> PlayerNodes { get; private set; } = new();
    public List<Node> NeutralNodes { get; private set; } = new();

    [SerializeField] private int _increasePerSecond = 1;
    [SerializeField] private float _moveSpeed = 3;
    [SerializeField] private float _unitSpawnSpeed = 0.5f;

    public int GetIncreasePerSecond() => _increasePerSecond;
    public float GetMoveSpeed() => _moveSpeed;
    public float GetUnitSpawnSpeed() => _unitSpawnSpeed;

    private Level _level;
    private Node[] _nodes;


    private void Awake()
    {
        Instance = this;

        _level = GetComponent<Level>();
        _nodes = FindObjectsOfType<Node>();

        foreach (Node node in _nodes)
            WriteInCorrectCategory(node);

        Node.OnNodeChanged += GetUpdatedNodes;
    }

    private void OnDisable()
    {
        Node.OnNodeChanged -= GetUpdatedNodes;
    }

    private void GetUpdatedNodes(Node node)
    {
        if (AINodes.Contains(node))
            AINodes.Remove(node);
        else if (PlayerNodes.Contains(node))
            PlayerNodes.Remove(node);
        else
            NeutralNodes.Remove(node);

        WriteInCorrectCategory(node);

        if (AINodes.Count == 0)
            _level.Win();
        if (PlayerNodes.Count == 0)
            _level.Lose();
    }

    private void WriteInCorrectCategory(Node node)
    {
        if (node.Unit == UnitType.AI)
            AINodes.Add(node);
        else if (node.Unit == UnitType.Player)
            PlayerNodes.Add(node);
        else
            NeutralNodes.Add(node);
    }
}
