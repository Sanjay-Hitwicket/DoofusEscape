using UnityEngine;

public class TileComponent : MonoBehaviour{
    [SerializeField] private Tile tileData;
    [SerializeField] private bool isStart = false;
    [SerializeField] private bool isEnd = false;
    
    public Tile TileData => tileData;
    public bool IsStart => isStart;
    public bool IsEnd => isEnd;
    
    public void Initialize(Tile tile)
    {
        tileData = tile;
        isStart = tile.index == 0;
        // Note: We'd need to know the total count to determine if it's the end
    }
    
    public void SetAsEnd()
    {
        isEnd = true;
    }
    
    public Tile GetNextTile()
    {
        return tileData?.nextTile;
    }
    
    public float GetDistanceToNext()
    {
        if (tileData?.nextTile != null)
        {
            return tileData.DistanceTo(tileData.nextTile);
        }
        return -1f;
    }
    
    private void OnDrawGizmos()
    {
        if (tileData == null) return;
        
        // Draw connection to next tile
        if (tileData.nextTile != null)
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawLine(transform.position, tileData.nextTile.position);
            
            // Draw arrow pointing to next tile
            Vector3 direction = (tileData.nextTile.position - transform.position).normalized;
            Vector3 arrowPos = transform.position + direction * 1f;
            Gizmos.DrawRay(arrowPos, direction * 0.5f);
        }
    }
}

[System.Serializable]
public class TileMapSettings
{
    [Header("Generation Settings")]
    public int tileCount = 15;
    public float minDistance = 8f;
    public float maxDistance = 15f;
    public float tileSize = 2f;
    
    [Header("Perlin Noise Settings")]
    public float noiseScale = 0.1f;
    public float heightVariation = 5f;
    public Vector2 noiseOffset = Vector2.zero;
    
    [Header("Visual Settings")]
    public GameObject tilePrefab;
    public Material connectionLineMaterial;
    public bool showConnections = true;
    public bool showGizmos = true;
}

[System.Serializable]
public class Tile
{
    public Vector3 position;
    public int index;
    public Tile nextTile;
    public GameObject gameObject;
    
    public Tile(Vector3 pos, int idx)
    {
        position = pos;
        index = idx;
        nextTile = null;
        gameObject = null;
    }
    
    public float DistanceTo(Tile other)
    {
        return Vector3.Distance(position, other.position);
    }
}