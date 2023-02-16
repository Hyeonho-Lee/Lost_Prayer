using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TileMapGenereator : MonoBehaviour {

    public TileMapSetting[] tileMapSetting;
    TileMapSetting tileMapSet;

    private int tileMapNumber;
    public event System.Action<int> OnNewTileMap;

    public Transform tilePrefab;
    public Transform centerPrefab;
    public Transform endPrefab;
    public Vector2 mapSize;

    [Range(0, 1)]
    public float outlinePercent;
    [Range(0, 1)]
    public float obstaclePercent;

    List<Coord> allTileCoords;
    Queue<Coord> shuffledTileCoords;

    public int seed = 10;
    public float BetweenDistance = 3f;
    private bool endCheck = false;
    Coord mapCentre;

    [System.Serializable]
    public class TileMapSetting {

        public Transform obstaclePrefab;
    }

    public void GenerateMap() {

        allTileCoords = new List<Coord>();
        for (int x = 0; x < mapSize.x; x++)
        {
            for (int y = 0; y < mapSize.y; y++)
            {
                allTileCoords.Add(new Coord(x, y));
            }
        }
        shuffledTileCoords = new Queue<Coord>(Utility.ShuffleArray(allTileCoords.ToArray(), seed));
        mapCentre = new Coord((int)mapSize.x / 2, (int)mapSize.y / 2);

        string holderName = "Generated Map";
        if (transform.Find(holderName)) {

            DestroyImmediate(transform.Find(holderName).gameObject);
        }

        Transform mapHolder = new GameObject(holderName).transform;
        mapHolder.parent = transform;

        for (int x = 0; x < mapSize.x; x++) {
            for (int y = 0; y < mapSize.y; y++) {

                Vector3 tilePosition = CoordToPosition(x, y);
                Transform newTile = Instantiate(tilePrefab, tilePosition * BetweenDistance, Quaternion.Euler(Vector3.right * 0)) as Transform;
                newTile.localScale = Vector3.one * (1 - outlinePercent);
                newTile.parent = mapHolder;
            }
        }

        bool[,] obstacleMap = new bool[(int)mapSize.x, (int)mapSize.y];

        int obstacleCount = (int)(mapSize.x * mapSize.y * obstaclePercent);
        int currentObstacleCount = 0;

        for (int i = 0; i < obstacleCount; i++) {

            Coord randomCoord = GetRandomCoord();
            obstacleMap[randomCoord.x, randomCoord.y] = true;
            currentObstacleCount++;

            if (randomCoord != mapCentre && MapIsFullyAccessible(obstacleMap, currentObstacleCount)) {

                // 가장자리에 생성
            }else {

                // 안에 생성
                if(randomCoord == mapCentre) {

                    Vector3 obstaclePosition = CoordToPosition(randomCoord.x, randomCoord.y);

                    Transform centerObstacle = Instantiate(centerPrefab, obstaclePosition * BetweenDistance + Vector3.up * .5f, Quaternion.identity) as Transform;
                    centerObstacle.parent = mapHolder;
                }else {

						Vector3 obstaclePosition = CoordToPosition(randomCoord.x, randomCoord.y);

                        RandomTileObject();
                        Transform newObstacle = Instantiate(tileMapSet.obstaclePrefab, obstaclePosition * BetweenDistance + Vector3.up * .5f, Quaternion.identity) as Transform;
                        newObstacle.parent = mapHolder;
                }

                Debug.Log(obstacleMap[randomCoord.x, randomCoord.y]);
                obstacleMap[randomCoord.x, randomCoord.y] = false;
                currentObstacleCount--;
                Debug.Log(obstacleMap[randomCoord.x, randomCoord.y]);
            }
        }

    }

    bool MapIsFullyAccessible(bool[,] obstacleMap, int currentObstacleCount) {

        bool[,] mapFlags = new bool[obstacleMap.GetLength(0), obstacleMap.GetLength(1)];
        Queue<Coord> queue = new Queue<Coord>();
        queue.Enqueue(mapCentre);
        mapFlags[mapCentre.x, mapCentre.y] = true;

        int accessibleTileCount = 1;

        while (queue.Count > 0) {

            Coord tile = queue.Dequeue();

            for (int x = -1; x <= 1; x++) {

                for (int y = -1; y <= 1; y++) {

                    int neighbourX = tile.x + x;
                    int neighbourY = tile.y + y;

                    if (x == 0 || y == 0) {

                        if (neighbourX >= 0 && neighbourX < obstacleMap.GetLength(0) && neighbourY >= 0 && neighbourY < obstacleMap.GetLength(1)) {

                            if (!mapFlags[neighbourX, neighbourY] && !obstacleMap[neighbourX, neighbourY]) {

                                mapFlags[neighbourX, neighbourY] = true;
                                queue.Enqueue(new Coord(neighbourX, neighbourY));
                                accessibleTileCount++;
                            }else {

                                endCheck = true;
                            }
                        }
                    }
                }
            }
        }

        int targetAccessibleTileCount = (int)(mapSize.x * mapSize.y - currentObstacleCount);
        return targetAccessibleTileCount == accessibleTileCount;
    }

    Vector3 CoordToPosition(int x, int y) {

        return new Vector3(-mapSize.x / 2 + 0.5f + x, 0, -mapSize.y / 2 + 0.5f + y);
    }

    public Coord GetRandomCoord() {

        Coord randomCoord = shuffledTileCoords.Dequeue();
        shuffledTileCoords.Enqueue(randomCoord);
        return randomCoord;
    }

    public struct Coord {

        public int x;
        public int y;

        public Coord(int _x, int _y) {

            x = _x;
            y = _y;
        }

        public static bool operator ==(Coord c1, Coord c2) {

            return c1.x == c2.x && c1.y == c2.y;
        }

        public static bool operator !=(Coord c1, Coord c2) {

            return !(c1 == c2);
        }

    }

    void RandomTileObject() {

        tileMapNumber = Random.Range(0, tileMapSetting.Length);

        if (tileMapNumber < tileMapSetting.Length) {

            tileMapSet = tileMapSetting[tileMapNumber];

            if (OnNewTileMap != null) {

                OnNewTileMap(tileMapNumber);
            }
        }
    }

    public void RandomTileSpawn() {

        seed = Random.Range(0, 99999);
        GenerateMap();
    }
}