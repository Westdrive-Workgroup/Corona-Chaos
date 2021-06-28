using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEditor;
using UnityEngine;

public class Grid : MonoBehaviour
{
    // for testing
    public Transform player;
    
    public Transform target;

    public List<Node> path;
    //grid stuff
    public Node[,] grid;
    public Vector2 gridWorldSize;
    public LayerMask unwalkableMask;
    public float nodeRadius;

    // column and row indices in the grid
    private int _gridSizeX;
    private int _gridSizeY;

    private float _nodeDiameter;
    public void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position,new Vector3(gridWorldSize.x,1f,gridWorldSize.y));
        if (grid != null)
        {
            
            foreach (Node node in grid)
            {
                // variable = condition ? ture : false
                Gizmos.color = node.isWalkable ? Color.green : Color.red;
                Gizmos.DrawCube(node.worldPosition, Vector3.one * (_nodeDiameter - 0.1f));
                if (player != null)
                {
                    Gizmos.color = Color.cyan;
                    Gizmos.DrawCube(GetNodeFromWorldPosition(player.position).worldPosition, Vector3.one * (_nodeDiameter - 0.1f)); 
                }
            }
            if (path != null)
            {
                Gizmos.color = Color.black;
                foreach (Node node in path)
                {
                    Gizmos.DrawCube(node.worldPosition, Vector3.one * (_nodeDiameter - 0.1f));
                }
            }
        }
    }
    public int GetMaxGridSize
    {
        get
        {
            return _gridSizeX * _gridSizeY;
        }
    }

    public Node GetNodeFromWorldPosition(Vector3 worldPos)
    {
        float X0_1 = Mathf.Clamp01((worldPos.x + gridWorldSize.x / 2) / gridWorldSize.x);
        float Y0_1 = Mathf.Clamp01((worldPos.z + gridWorldSize.y / 2) / gridWorldSize.y);
        int x = Mathf.RoundToInt(X0_1 * (_gridSizeX - 1));
        int y = Mathf.RoundToInt(Y0_1 * (_gridSizeY - 1));
        return grid[x, y];
    }

    public List<Node> GetNodeNeighbors(Node node)
    {
        List<Node> neighbors = new List<Node>();

        for (int x = -1; x <= 1; x++)
        {
            for (int y = -1; y <= 1; y++)
            {
                if (x == 0 && y == 0)
                    continue;
                int neighborX = node.gridX + x;
                int neighborY = node.gridY + y;
                if(neighborX >= 0 && neighborX < _gridSizeX && neighborY >= 0 && neighborY < _gridSizeY)
                    neighbors.Add(grid[neighborX,neighborY]);
            }
        }

        return neighbors;
    }
    
    // Start is called before the first frame update
    
    void Start()
    {
        _nodeDiameter = 2 * nodeRadius;
        _gridSizeX = Mathf.RoundToInt(gridWorldSize.x / _nodeDiameter);
        _gridSizeY = Mathf.RoundToInt(gridWorldSize.y / _nodeDiameter);
        CreateGrid();
    }

    void CreateGrid()
    {
        grid = new Node[_gridSizeX, _gridSizeY];
        Vector3 gridBottomLeftWorldPosition = transform.position - Vector3.right * (gridWorldSize.x / 2) -
                                              Vector3.forward * (gridWorldSize.y / 2);
        for (int x = 0; x < _gridSizeX; x++)
        {
            for (int y = 0; y < _gridSizeY; y++)
            {
                Vector3 nodeWorldPosition = gridBottomLeftWorldPosition +
                                            Vector3.right * (x * _nodeDiameter + nodeRadius) +
                                            Vector3.forward * (y * _nodeDiameter + nodeRadius);
                bool walkable = !Physics.CheckSphere(nodeWorldPosition,nodeRadius,unwalkableMask);
                grid[x, y] = new Node(nodeWorldPosition, walkable, x, y);
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (player != null && target != null)
            GetComponent<PathFinder>().FindPath(player.position, target.position);
    }
}
