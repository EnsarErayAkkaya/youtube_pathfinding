using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LevelCreator : MonoBehaviour
{
    [SerializeField] private Vector2Int worldSize;
    [SerializeField] private Grid grid;

    [Header("Obstacles")]
    [SerializeField] private GridEntity obstacle;
    [SerializeField] private int obstacleCount;

    [Header("Tile")]
    [SerializeField] private Tile tile;

    private List<GridEntity> entities = new();
    private List<Tile> tiles = new();

    private void Start()
    {
        //  2 
        //  1
        //  0
        // -1
        // -2, -1, 0, 1, 2
        for (int x = MinX(); x <= MaxX(); x++)
        {
            for (int y = MinX(); y <= MaxX(); y++)
            {
                var tileInstance = Instantiate(tile);
                
                tileInstance.transform.parent = transform;
                tileInstance.transform.position = grid.CellToWorld(new Vector3Int(x, y, 0));

                tiles.Add(tileInstance);
            }
        }
    }



    private void CreateEntity<T>(T prefab, ref List<T> list) where T : GridEntity
    {
        var instance = Instantiate(prefab, transform);

        Vector3Int coord;
        do
        {
            coord = new Vector3Int(Random.Range(MinX(), MaxX() + 1), Random.Range(MinY(), MaxY() + 1), 0);
        }
        while (!IsFree(coord));

        instance.transform.position = grid.CellToWorld(coord);
        instance.Init(coord);

        list.Add(instance);
    }

    public bool IsFree(Vector3Int coord)
    {
        return entities.Any(s => s.Coordinate == coord) == false;
    }

    public int MinX() => -(int)(worldSize.x / 2);
    public int MaxX() => (int)(worldSize.x / 2); // 5 / 2 = 2.5 (int cast) => 2
    public int MinY() => -(int)(worldSize.y / 2);
    public int MaxY() => (int)(worldSize.y / 2);
}
