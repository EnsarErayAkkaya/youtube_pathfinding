using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridEntity : MonoBehaviour
{
    public Vector3Int coordinate;
    public Vector3Int Coordinate => coordinate;

    public void Init(Vector3Int coord)
    {
        this.coordinate = coord;
    }
}
