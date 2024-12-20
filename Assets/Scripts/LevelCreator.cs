using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LevelCreator : MonoBehaviour
{
    [SerializeField] private Vector2Int worldSize;
    [SerializeField] private Grid grid;

    [Header("Obstacles")]
    [SerializeField] private Obstacle obstacle;
    [SerializeField] private int obstacleCount;

    [Header("Tile")]
    [SerializeField] private Tile tile;

    private List<Obstacle> obstacles = new();
    private List<Tile> tiles = new();

    private void Start()
    {
        for (int x = MinX(); x <= MaxX(); x++)
        {
            for (int y = MinY(); y <= MaxY(); y++)
            {
                var tileInstance = Instantiate(tile);

                var coord = new Vector3Int(x, y, 0);

                tileInstance.transform.parent = transform;
                tileInstance.transform.position = grid.CellToWorld(coord);

                tileInstance.Init(coord);

                tiles.Add(tileInstance);
            }
        }

        for (int i = 0; i < obstacleCount; i++)
        {
            var obstacleInstance = Instantiate(obstacle);

            obstacleInstance.transform.parent = transform;

            Vector3Int coord;
            do
            {
                coord = new Vector3Int(Random.Range(MinX(), MaxX() + 1), Random.Range(MinY(), MaxY() + 1), 0);
            }
            while (!IsFree(coord));

            obstacleInstance.Init(coord);
            obstacleInstance.transform.position = grid.CellToWorld(coord);

            obstacles.Add(obstacleInstance);
        }
    }

    public bool IsFree(Vector3Int coord)
    {
        return obstacles.Any(s => s.Coordinate == coord) == false;
    }

    public int MinX() => -(int)(worldSize.x / 2);
    public int MaxX() => (int)(worldSize.x / 2); // 5 / 2 = 2.5 (int cast) => 2
    public int MinY() => -(int)(worldSize.y / 2);
    public int MaxY() => (int)(worldSize.y / 2);
}
