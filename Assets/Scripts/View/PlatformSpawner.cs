using System.Collections.Generic;
using UnityEngine;

public class PlatformSpawner : MonoBehaviour {
    [SerializeField] private GameObject platformPrefab;
    [SerializeField] private int platformCount = 20;
    [SerializeField] private float platformSpacing = 2f; // Controls distance between platforms
    [SerializeField] private float maxJumpDistance = 3f;

    private HashSet<Vector3> occupiedPositions = new HashSet<Vector3>();

    // Allowable movement directions
    private Vector3[] directionOffsets = new Vector3[]
    {
        Vector3.left,
        Vector3.right,
        Vector3.forward,
        Vector3.forward + Vector3.up,
        Vector3.down
    };

    private void Start()
    {
        GeneratePlatforms(transform.position);
    }

    private void GeneratePlatforms(Vector3 startPos)
    {
        Vector3 currentPos = startPos;
        occupiedPositions.Add(Round(currentPos));
        Instantiate(platformPrefab, currentPos, Quaternion.identity);

        for (int i = 1; i < platformCount; i++)
        {
            Vector3 nextPos = GetNextValidPosition(currentPos);

            if (nextPos != Vector3.zero)
            {
                Instantiate(platformPrefab, nextPos, Quaternion.identity);
                occupiedPositions.Add(Round(nextPos));
                currentPos = nextPos;
            }
            else
            {
                Debug.Log("No valid position found. Ending generation early.");
                break;
            }
        }
    }

    private Vector3 GetNextValidPosition(Vector3 from)
    {
        List<Vector3> candidates = new List<Vector3>();

        foreach (Vector3 dir in directionOffsets)
        {
            Vector3 candidate = from + dir.normalized * platformSpacing;
            float distance = Vector3.Distance(from, candidate);

            if (distance <= maxJumpDistance &&
                !occupiedPositions.Contains(Round(candidate)) &&
                !IsDirectlyAbove(candidate))
            {
                candidates.Add(candidate);
            }
        }

        if (candidates.Count > 0)
        {
            return candidates[Random.Range(0, candidates.Count)];
        }

        return Vector3.zero;
    }

    private bool IsDirectlyAbove(Vector3 pos)
    {
        Vector3 below = new Vector3(pos.x, pos.y - platformSpacing, pos.z);
        return occupiedPositions.Contains(Round(below));
    }

    private Vector3 Round(Vector3 v)
    {
        // Round to 1 decimal to avoid floating point inaccuracies
        return new Vector3(
            Mathf.Round(v.x * 10f) / 10f,
            Mathf.Round(v.y * 10f) / 10f,
            Mathf.Round(v.z * 10f) / 10f
        );
    }
}
