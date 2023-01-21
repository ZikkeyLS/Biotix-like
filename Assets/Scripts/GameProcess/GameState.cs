using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(Level))]
public class GameState : MonoBehaviour
{
    [SerializeField] private int _increasePerSecond = 1;
    [SerializeField] private float _moveSpeed = 3;
    [SerializeField] private float _unitSpawnSpeed = 0.5f;

    private Level _level;
    private Node[] _nodes;

    public static GameState Instance { get; private set; }

    public List<Node> AINodes { get; private set; } = new();
    public List<Node> PlayerNodes { get; private set; } = new();
    public List<Node> NeutralNodes { get; private set; } = new();

    public int GetIncreasePerSecond() => _increasePerSecond;
    public float GetMoveSpeed() => _moveSpeed;
    public float GetUnitSpawnSpeed() => _unitSpawnSpeed;


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
        RemoveIncorrectNode(node);
        WriteInCorrectCategory(node);

        if (AINodes.Count == 0)
        {
            _level.Win();
            return;
        }

        if (PlayerNodes.Count == 0)
        {
            _level.Lose();
            return;
        }
    }

    private void RemoveIncorrectNode(Node node)
    {
        if (AINodes.Contains(node))
        {
            AINodes.Remove(node);
            return;
        }

        if (PlayerNodes.Contains(node))
        {
            PlayerNodes.Remove(node);
            return;
        }

        NeutralNodes.Remove(node);
    }

    private void WriteInCorrectCategory(Node node)
    {
        if (node.Unit == UnitType.AI)
        {
            AINodes.Add(node);
            return;
        }

        if (node.Unit == UnitType.Player)
        {
            PlayerNodes.Add(node);
            return;
        }

        NeutralNodes.Add(node);
    }
}
