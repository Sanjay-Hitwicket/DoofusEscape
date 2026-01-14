using System.Collections.Generic;
using UnityEngine;

namespace Systems.Demo_Map {
    public class PerlinTileMapGenerator : MonoBehaviour {
    [SerializeField] private TileMapSettings settings;
    [SerializeField] private List<Tile> tiles = new List<Tile>();
    [SerializeField] private Transform tilesParent;
    [SerializeField] private LineRenderer connectionLine;
    
    // Editor controls
    [Header("Runtime Controls")]
    [SerializeField] private bool autoGenerate = false;
    [SerializeField] private float autoGenerateInterval = 2f;
    private float lastGenerateTime;
    
    private void Start()
    {
        // Create parent object for tiles if it doesn't exist
        if (tilesParent == null)
        {
            GameObject parent = new GameObject("Generated Tiles");
            tilesParent = parent.transform;
            tilesParent.SetParent(transform);
        }
        
        // Setup line renderer for connections
        SetupLineRenderer();
        
        // Generate initial map
        GenerateMap();
    }
    
    // private void Update()
    // {
    //     if (autoGenerate && Time.time - lastGenerateTime > autoGenerateInterval)
    //     {
    //         GenerateMap();
    //         lastGenerateTime = Time.time;
    //     }
    // }
    //
    [ContextMenu("Generate New Map")]
    public void GenerateMap()
    {
        ClearExistingTiles();
        GenerateTilePositions();
        CreateLinearConnections();
        InstantiateTiles();
        UpdateConnectionVisualization();
        
        Debug.Log($"Generated map with {tiles.Count} tiles");
    }
    
    private void GenerateTilePositions()
    {
        tiles.Clear();
        
        // Start position
        Vector3 currentPosition = transform.position;
        
        for (int i = 0; i < settings.tileCount; i++)
        {
            // Use Perlin noise to determine the next position
            Vector3 noisePosition = GenerateNextPosition(currentPosition, i);
            
            Tile newTile = new Tile(noisePosition, i);
            tiles.Add(newTile);
            
            currentPosition = noisePosition;
        }
    }
    
    private Vector3 GenerateNextPosition(Vector3 currentPos, int index)
    {
        if (index == 0)
        {
            return currentPos;
        }
        
        // Generate noise-based direction
        float noiseX = (currentPos.x + settings.noiseOffset.x) * settings.noiseScale;
        float noiseY = (currentPos.z + settings.noiseOffset.y) * settings.noiseScale;
        
        // Use multiple octaves of noise for more interesting patterns
        float angle = Mathf.PerlinNoise(noiseX, noiseY) * 2f * Mathf.PI;
        float angle2 = Mathf.PerlinNoise(noiseX + 100f, noiseY + 100f) * 2f * Mathf.PI;
        
        // Combine angles for more variation
        angle = (angle + angle2 * 0.3f);
        
        // Generate distance using noise
        float distanceNoise = Mathf.PerlinNoise(noiseX + 200f, noiseY + 200f);
        float distance = Mathf.Lerp(settings.minDistance, settings.maxDistance, distanceNoise);
        
        // Calculate horizontal movement
        Vector3 direction = new Vector3(Mathf.Cos(angle), 0, Mathf.Sin(angle));
        Vector3 newPosition = currentPos + direction * distance;
        
        // Add height variation using noise
        float heightNoise = Mathf.PerlinNoise(newPosition.x * settings.noiseScale * 0.5f, newPosition.z * settings.noiseScale * 0.5f);
        newPosition.y += (heightNoise - 0.5f) * settings.heightVariation;
        
        return newPosition;
    }
    
    private void CreateLinearConnections()
    {
        // Create linear chain - each tile connects to the next one
        for (int i = 0; i < tiles.Count - 1; i++)
        {
            tiles[i].nextTile = tiles[i + 1];
        }
        
        // Last tile has no next tile (end of chain)
        if (tiles.Count > 0)
        {
            tiles[tiles.Count - 1].nextTile = null;
        }
    }
    
    private void InstantiateTiles()
    {
        for (int i = 0; i < tiles.Count; i++)
        {
            GameObject tileObj = CreateTileGameObject(tiles[i], i);
            tiles[i].gameObject = tileObj;
        }
    }
    
    private GameObject CreateTileGameObject(Tile tile, int index)
    {
        GameObject tileObj;
        
        if (settings.tilePrefab != null)
        {
            tileObj = Instantiate(settings.tilePrefab, tile.position, Quaternion.identity, tilesParent);
        }
        else
        {
            // Create default cube if no prefab is provided
            tileObj = GameObject.CreatePrimitive(PrimitiveType.Cube);
            tileObj.transform.position = tile.position;
            tileObj.transform.localScale = Vector3.one * settings.tileSize;
            tileObj.transform.SetParent(tilesParent);
            
            // Color tiles based on their position in the chain
            Renderer renderer = tileObj.GetComponent<Renderer>();
            if (renderer != null)
            {
                float t = (float)index / (settings.tileCount - 1);
                Color color = Color.Lerp(Color.green, Color.red, t);
                
                // Create material instance
                Material mat = new Material(Shader.Find("Standard"));
                mat.color = color;
                renderer.material = mat;
            }
        }
        
        tileObj.name = $"Tile_{index}";
        
        // Add tile component for identification
        TileComponent tileComponent = tileObj.GetComponent<TileComponent>();
        if (tileComponent == null)
        {
            tileComponent = tileObj.AddComponent<TileComponent>();
        }
        tileComponent.Initialize(tile);
        
        return tileObj;
    }
    
    private void SetupLineRenderer()
    {
        if (connectionLine == null)
        {
            GameObject lineObj = new GameObject("Connection Line");
            lineObj.transform.SetParent(transform);
            connectionLine = lineObj.AddComponent<LineRenderer>();
        }
        
        connectionLine.material = settings.connectionLineMaterial ?? CreateDefaultLineMaterial();
        connectionLine.startColor = Color.yellow;
        connectionLine.endColor = Color.yellow;
        connectionLine.startWidth = 0.1f;
        connectionLine.endWidth = 0.1f;
        connectionLine.useWorldSpace = true;
        connectionLine.enabled = settings.showConnections;
    }
    
    private Material CreateDefaultLineMaterial()
    {
        Material mat = new Material(Shader.Find("Sprites/Default"));
        mat.color = Color.yellow;
        return mat;
    }
    
    private void UpdateConnectionVisualization()
    {
        if (connectionLine == null || !settings.showConnections) return;
        
        List<Vector3> linePoints = new List<Vector3>();
        
        foreach (Tile tile in tiles)
        {
            linePoints.Add(tile.position);
        }
        
        connectionLine.positionCount = linePoints.Count;
        connectionLine.SetPositions(linePoints.ToArray());
        connectionLine.enabled = settings.showConnections && linePoints.Count > 1;
    }
    
    private void ClearExistingTiles()
    {
        // Destroy existing tile GameObjects
        for (int i = 0; i < tiles.Count; i++)
        {
            if (tiles[i].gameObject != null)
            {
                if (Application.isPlaying)
                    Destroy(tiles[i].gameObject);
                else
                    DestroyImmediate(tiles[i].gameObject);
            }
        }
        
        // Clear existing tiles from parent
        if (tilesParent != null)
        {
            for (int i = tilesParent.childCount - 1; i >= 0; i--)
            {
                if (Application.isPlaying)
                    Destroy(tilesParent.GetChild(i).gameObject);
                else
                    DestroyImmediate(tilesParent.GetChild(i).gameObject);
            }
        }
        
        tiles.Clear();
    }
    
    // Public methods for external access
    public List<Tile> GetTiles() => tiles;
    public Tile GetStartTile() => tiles.Count > 0 ? tiles[0] : null;
    public Tile GetEndTile() => tiles.Count > 0 ? tiles[tiles.Count - 1] : null;
    
    public List<Vector3> GetTilePath()
    {
        List<Vector3> path = new List<Vector3>();
        foreach (Tile tile in tiles)
        {
            path.Add(tile.position);
        }
        return path;
    }
    
    // Validation methods
    public bool ValidateDistances()
    {
        for (int i = 0; i < tiles.Count - 1; i++)
        {
            float distance = tiles[i].DistanceTo(tiles[i + 1]);
            if (distance < settings.minDistance || distance > settings.maxDistance)
            {
                Debug.LogWarning($"Tile {i} distance validation failed: {distance}");
                return false;
            }
        }
        return true;
    }
    
    private void OnDrawGizmos()
    {
        if (!settings.showGizmos || tiles == null) return;
        
        // Draw tiles
        Gizmos.color = Color.cyan;
        for (int i = 0; i < tiles.Count; i++)
        {
            Gizmos.DrawWireCube(tiles[i].position, Vector3.one * settings.tileSize);
            
            // Draw tile index
            #if UNITY_EDITOR
            UnityEditor.Handles.Label(tiles[i].position + Vector3.up * (settings.tileSize * 0.7f), i.ToString());
            #endif
        }
        
        // Draw connections
        Gizmos.color = Color.yellow;
        for (int i = 0; i < tiles.Count - 1; i++)
        {
            Gizmos.DrawLine(tiles[i].position, tiles[i + 1].position);
            
            // Draw distance labels
            #if UNITY_EDITOR
            Vector3 midPoint = (tiles[i].position + tiles[i + 1].position) * 0.5f;
            float distance = tiles[i].DistanceTo(tiles[i + 1]);
            UnityEditor.Handles.Label(midPoint, distance.ToString("F1"));
            #endif
        }
        
        // Draw start and end markers
        if (tiles.Count > 0)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawSphere(tiles[0].position, settings.tileSize * 0.3f);
            
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(tiles[tiles.Count - 1].position, settings.tileSize * 0.3f);
        }
    }
}
}