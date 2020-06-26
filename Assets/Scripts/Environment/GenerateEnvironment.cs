using System.Collections.Generic;
using UnityEngine;

public class GenerateEnvironment : MonoBehaviour
{
    public static GenerateEnvironment instance = null;

    public static int minBound => 0 - instance.height / 2;

    [Range(0, 100)]
    [SerializeField] private int randomFillPercent;
    [SerializeField] private int width;
    [SerializeField] private int height;
    [SerializeField] private string seed;
    [SerializeField] private bool useRandomSeed;
    [SerializeField] private List<Sprite> tileSprites;
    [SerializeField] private Sprite stoneSprite;
    [SerializeField] private Sprite gravelSprite;
    [SerializeField]
    private GameObject
        enemyPrefab,
        bottomEdgePrefab,
        topEdgePrefab;
    private List<GameObject> currentTiles = new List<GameObject>();
    private int[,] map;
    GameObject tileParent = null;

    private void Start()
    {
        GenerateMap();
        instance = this;
    }
    private void GenerateMap()
    {
        map = new int[width, height];
        RandomFillMap();

        for (int i = 0; i < 5; i++)
            SmoothMap();

        int borderSize = 1;
        int[,] borderedMap = new int[width + borderSize * 2, height + borderSize * 2];

        for (int x = 0; x < borderedMap.GetLength(0); x++)
        {
            for (int y = 0; y < borderedMap.GetLength(1); y++)
            {
                if (x >= borderSize && x < width + borderSize && y >= borderSize && y < height + borderSize)
                    borderedMap[x, y] = map[x - borderSize, y - borderSize];
                else
                    borderedMap[x, y] = 1;
            }
        }

        SpawnTiles();
    }
    private void RandomFillMap()
    {
        if (useRandomSeed)
            seed = Time.time.ToString();

        System.Random rand = new System.Random(seed.GetHashCode());

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                if (x == 0 || x == width - 1 || y == 0 || y == height - 1)
                    map[x, y] = 1;
                else
                    map[x, y] = (rand.Next(0, 100) < randomFillPercent) ? 1 : 0;
            }
        }
    }
    private void SmoothMap()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                int neighbourWallTiles = GetSurroundingWallCount(x, y);

                if (neighbourWallTiles > 4)
                    map[x, y] = 1;
                else if (neighbourWallTiles < 4)
                    map[x, y] = 0;
            }
        }
    }
    private int GetSurroundingWallCount(int gridX, int gridY)
    {
        int wallCount = 0;
        for (int neighbourX = gridX - 1; neighbourX <= gridX + 1; neighbourX++)
        {
            for (int neighbourY = gridY - 1; neighbourY <= gridY + 1; neighbourY++)
            {
                if (neighbourX >= 0 && neighbourX < width && neighbourY >= 0 && neighbourY < height)
                {
                    if (neighbourX != gridX || neighbourY != gridY)
                        wallCount += map[neighbourX, neighbourY];
                }
                else
                    wallCount++;
            }
        }

        return wallCount;
    }
    private void SpawnTiles()
    {
        currentTiles.ForEach(x => Destroy(x));
        currentTiles.Clear();

        if (tileParent != null)
            Destroy(tileParent);

        if (map == null)
            return;

        tileParent = new GameObject("Tiles");

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                Vector3 pos = new Vector3(-width / 2 + x + .5f, -height / 2 + y + .5f, 0);

                if (map[x, y] == 0)
                {
                    if (Random.Range(0, 30) == 0)
                    {
                        var enemyGo = Instantiate(enemyPrefab, pos, Quaternion.identity);
                        enemyGo.GetComponent<SpriteRenderer>().color = new Color(Random.Range(0.5f, 1.0f), Random.Range(0.5f, 1.0f), Random.Range(0.5f, 1.0f));
                    }

                    if (y < height - 1 && map[x, y + 1] == 1)
                    {
                        var edgeGo = Instantiate(bottomEdgePrefab, pos, Quaternion.identity);
                        currentTiles.Add(edgeGo);
                    }
                    if (y > 0 && map[x, y - 1] == 1)
                    {
                        var edgeGo = Instantiate(topEdgePrefab, pos, Quaternion.identity);
                        currentTiles.Add(edgeGo);
                    }
                    continue;
                }



                var go = new GameObject("Tile (" + pos.x + "," + pos.y + ")");
                go.transform.parent = tileParent.transform;
                go.transform.position = pos;

                var sr = go.AddComponent<SpriteRenderer>();
                sr.sprite = tileSprites[0];

                var col = go.AddComponent<BoxCollider2D>();
                col.size = Vector2.one * 0.9f;

                var rb = go.AddComponent<Rigidbody2D>();
                rb.constraints = RigidbodyConstraints2D.FreezeAll;

                var weighting = Random.Range(0, 100);
                if (weighting < 50)
                {
                    go.AddComponent<TileFallCheck>();
                    sr.sprite = gravelSprite;
                    go.tag = "Tile";
                }
                else if (weighting < 90)
                    go.tag = "Tile";
                else
                {
                    sr.sprite = stoneSprite;
                    go.tag = "Indestructible";
                }
                currentTiles.Add(go);
            }
        }

        foreach (var go in currentTiles)
        {
            if (go.TryGetComponent<EdgeRemoveCheck>(out var erc))
                erc.StartChecking();
        }
    }
}