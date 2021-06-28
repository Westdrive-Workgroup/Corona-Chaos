using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Grid))]
public class PathFinder : MonoBehaviour
{
    private Grid _grid;
    void Awake()
    {
        _grid = GetComponent<Grid>();
    }

    public void FindPath(Vector3 startPos, Vector3 targetPos)
    {
        Node startNode = _grid.GetNodeFromWorldPosition(startPos);
        Node targetNode = _grid.GetNodeFromWorldPosition(targetPos);

        Heap<Node> openSet = new Heap<Node>(_grid.GetMaxGridSize);
        HashSet<Node> closedSet = new HashSet<Node>();
        
        openSet.Add(startNode);
        while (openSet.Count > 0)
        {
             Node currentNode = openSet.RemoveFirst();
            // Node currentNode = openSet[0];
            // for (int i = 0; i < openSet.Count; i++)
            // {
            //     if (openSet[i].fCost < currentNode.fCost ||
            //         (openSet[i].fCost == currentNode.fCost || openSet[i].hCost < currentNode.hCost))
            //     {
            //         currentNode = openSet[i];
            //     }
            // }
            // openSet.Remove(currentNode);
            closedSet.Add(currentNode);

            if (currentNode == targetNode)
            {
                TraceRoute(startNode, targetNode);
                return;
            }

            foreach (Node neighbor in _grid.GetNodeNeighbors(currentNode))
            {
                if (!neighbor.isWalkable || closedSet.Contains(neighbor))
                    continue;
                int movementCostToNeighbor = currentNode.gCost + GetDistance(currentNode, neighbor);
                if (movementCostToNeighbor < currentNode.gCost || !openSet.Contains(neighbor))
                {
                    neighbor.gCost = movementCostToNeighbor;
                    neighbor.hCost = GetDistance(neighbor, targetNode);
                    neighbor.parent = currentNode;
                    if(!openSet.Contains(neighbor))
                        openSet.Add(neighbor);
                }
            }
            
        }
    }

    public void TraceRoute(Node from, Node to)
    {
        List<Node> path = new List<Node>();
        Node currentNode = to;
        while (currentNode != from)
        {
            path.Add(currentNode);
            currentNode = currentNode.parent;
        }
        path.Reverse();
        _grid.path = path;
    }
    public int GetDistance(Node from, Node to)
    {
        int distanceX = Mathf.Abs(to.gridX - from.gridX);
        int distanceY = Mathf.Abs(to.gridY - from.gridY);
        return distanceX > distanceY ? 14 * distanceY + 10 * distanceX : 14 * distanceX + 10 * distanceY;
    }
}
