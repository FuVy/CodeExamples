using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellGenerator : MonoBehaviour
{
    [SerializeField]
    private Cell _cellPrefab;
    [SerializeField]
    private Vector3 _position = Vector3.zero;
    [SerializeField]
    private Vector2 _cellGaps = Vector2.zero;
    [SerializeField]
    private bool _isBlack = true;

    private Transform _transform;
    private void Awake()
    {
        _transform = transform;
    }
    public Cell[,] InitialGeneration(int rows, int columns)
    {
        Cell[,] cells = new Cell[rows, columns];
        var gap = _cellPrefab.Renderer.size + _cellGaps;
        _position.y = GameSession.Location.CellsY;
        for (int currentRow = 0; currentRow < rows; currentRow++)
        {
            _position.x = 0;
            for (int currentColumn = 0; currentColumn < columns; currentColumn++)
            {
                var cell = GenerateCell(_position, string.Format("[{0}:{1}]", currentRow, currentColumn));
                cell.SetRendererStatus(_isBlack);
                cell.SetCellIndexes(new Vector2Int(currentColumn, currentRow));
                _isBlack = !_isBlack;
                _position.x += gap.x;
                cells[currentRow, currentColumn] = cell;
            }
            _position.y -= gap.y;
        }
        return cells;
    }

    public Cell GenerateCell(Vector3 position, string name)
    {
        var cell = Instantiate(_cellPrefab, position, Quaternion.identity);
        cell.gameObject.name = name;
        cell.transform.parent = _transform;
        return cell;
    }
}
