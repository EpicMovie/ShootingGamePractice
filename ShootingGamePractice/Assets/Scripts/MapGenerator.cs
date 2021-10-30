using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    public Transform tilePrefab;
    public Transform obstaclePrefab;
    public Vector2 mapSize;

    [Range(0, 1)]
    public float outlinePercent;

    [Range(0, 1)]
    public float obstaclePercent;

    public int seed = 10;

    List<Coord> allTileCoords;
    Queue<Coord> shuffledTileCoords;
    Coord mapCenter;

    void Start()
    {
        GenerateMap();
    }

    public void GenerateMap()
    {
        allTileCoords = new List<Coord>();

        for (int x = 0; x < mapSize.x; x++)
        {
            for (int y = 0; y < mapSize.y; y++)
            {
                allTileCoords.Add(new Coord(x, y));
            }
        }

        shuffledTileCoords = new Queue<Coord>(Utility.ShuffleArray(allTileCoords.ToArray(), seed));

        mapCenter = new Coord((int)(mapSize.x / 2), (int)(mapSize.y / 2));

        string holderName = "Generated Map";

        if(transform.Find(holderName))
        {
            DestroyImmediate(transform.Find(holderName).gameObject);
        }

        Transform mapHolder = new GameObject(holderName).transform;
        mapHolder.parent = transform;

        for(int x = 0; x < mapSize.x; x++)
        {
            for(int y = 0; y < mapSize.y; y++)
            {
                Vector3 tilePosition = CoordToPosition(x, y);
                Transform newTile = Instantiate(tilePrefab, tilePosition, Quaternion.Euler(Vector3.right * 90)) as Transform;

                newTile.localScale = Vector3.one * (1 - outlinePercent);
                newTile.parent = mapHolder;
            }
        }

        bool[,] obstacleMap = new bool[(int)mapSize.x, (int)mapSize.y];

        int obstacleCount = (int)(mapSize.x * mapSize.y * obstaclePercent);
        int curObstacleCount = 0;

        for(int i = 0; i < obstacleCount; i++)
        {
            Coord randCoord = GetRandCoord();

            if (randCoord != mapCenter && IsMapFullyAccessible(obstacleMap, curObstacleCount))
            {
                curObstacleCount++;

                obstacleMap[randCoord.x, randCoord.y] = true;

                Vector3 obstaclePosition = CoordToPosition(randCoord.x, randCoord.y);

                Transform newObstacle = Instantiate(obstaclePrefab, obstaclePosition + Vector3.up * 0.5f, Quaternion.identity);

                newObstacle.localScale = Vector3.one * (1 - outlinePercent);
                newObstacle.parent = mapHolder;
            }
        }
    }

    bool IsMapFullyAccessible(bool[,] obstacleMap, int curObstacleCount)
    {
        
    }

    Vector3 CoordToPosition(int x, int y)
    {
        return new Vector3(-mapSize.x / 2 + 0.5f + x, 0, -mapSize.y + 0.5f + y);
    }

    public Coord GetRandCoord()
    {
        Coord randCoord = shuffledTileCoords.Dequeue();
        shuffledTileCoords.Enqueue(randCoord);

        return randCoord;
    }

    public struct Coord
    {
        public int x;
        public int y;

        public Coord(int _x, int _y)
        {
            x = _x;
            y = _y;
        }

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public bool Equals(Coord coord) => x == coord.x && y == coord.y;
        
        public override int GetHashCode() => (x, y).GetHashCode();

        public static bool operator ==(Coord lhs, Coord rhs) => lhs.Equals(rhs);

        public static bool operator !=(Coord lhs, Coord rhs) => lhs.Equals(rhs) == false;
    }
}
