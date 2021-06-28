using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Node : IHeapItem<Node>
{
    // just because we like to use heaps later on 
    private int heapIndex; 
    // node information
    public Vector3 worldPosition;
    public bool isWalkable;

    public int gridX;
    public int gridY;
    
    // Path fiding information
    public Node parent;
    public int gCost; // immediate cost of moving to a neighbor
    public int hCost; // distance to target location

    public Node(Vector3 worldPos, bool walkable, int x, int y)
    {
        worldPosition = worldPos;
        isWalkable = walkable;
        gridX = x;
        gridY = y; 
    }
    public int fCost
    {
        get
        {
            return gCost + hCost;
        }
    }
    

    public int HeapIndex
    {
        get
        {
            return heapIndex;
        }
        set
        {
            heapIndex = value;
        }
    }

    public int CompareTo(Node other)
    {
        int compare = fCost.CompareTo(other.fCost);
        if (compare == 0)
            compare = hCost.CompareTo(other.hCost);
        return -compare;
    }
    
    
    
}
