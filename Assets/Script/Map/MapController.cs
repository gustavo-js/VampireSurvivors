using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapController : MonoBehaviour
{
    public List<GameObject> terrainChunks;
    public GameObject player;
    public float checkerRadius;
    Vector2 noTerrainPosition;
    public LayerMask terrainMask;
    public GameObject currentChunk;
    PlayerMovement playerMovement;

    [Header("Optimization")]
    public List<GameObject> spawnedChunks;
    GameObject latestChunk;
    public float maxOptimizationDistance;
    float optimizationDistance;
    float optimizerCooldown;
    public float optimizerCooldownDuration;

    // Start is called before the first frame update
    void Start()
    {
        playerMovement = FindObjectOfType<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckChunk();
        ChunkOptimizer();
    }

    void CheckChunk()
    {
        if (!currentChunk)
        {
            return;
        }
        // Player is moving only right
        if (playerMovement.moveDirection.x > 0 && playerMovement.moveDirection.y == 0)
        {
            if (!Physics2D.OverlapCircle(currentChunk.transform.Find("Right").position, checkerRadius, terrainMask))
            {
                noTerrainPosition = currentChunk.transform.Find("Right").position;
                SpawnChunk();
            }
        }
        // Player is moving only left
        else if (playerMovement.moveDirection.x < 0 && playerMovement.moveDirection.y == 0)
        {
            if (!Physics2D.OverlapCircle(currentChunk.transform.Find("Left").position, checkerRadius, terrainMask))
            {
                noTerrainPosition = currentChunk.transform.Find("Left").position;
                SpawnChunk();
            }
        }
        // Player is moving only up
        else if (playerMovement.moveDirection.y > 0 && playerMovement.moveDirection.x == 0)
        {
            if (!Physics2D.OverlapCircle(currentChunk.transform.Find("Up").position, checkerRadius, terrainMask))
            {
                noTerrainPosition = currentChunk.transform.Find("Up").position;
                SpawnChunk();
            }
        }
        // Player is moving only down
        else if (playerMovement.moveDirection.y < 0 && playerMovement.moveDirection.x == 0)
        {
            if (!Physics2D.OverlapCircle(currentChunk.transform.Find("Down").position, checkerRadius, terrainMask))
            {
                noTerrainPosition = currentChunk.transform.Find("Down").position;
                SpawnChunk();
            }
        }
        // Player is moving right up
        else if (playerMovement.moveDirection.y > 0 && playerMovement.moveDirection.x > 0)
        {
            if (!Physics2D.OverlapCircle(currentChunk.transform.Find("Right Up").position, checkerRadius, terrainMask))
            {
                noTerrainPosition = currentChunk.transform.Find("Right Up").position;
                SpawnChunk();
            }
        }
        // Player is moving right down
        else if (playerMovement.moveDirection.y < 0 && playerMovement.moveDirection.x > 0)
        {
            if (!Physics2D.OverlapCircle(currentChunk.transform.Find("Right Down").position, checkerRadius, terrainMask))
            {
                noTerrainPosition = currentChunk.transform.Find("Right Down").position;
                SpawnChunk();
            }
        }
        // Player is moving left up
        else if (playerMovement.moveDirection.y > 0 && playerMovement.moveDirection.x < 0)
        {
            if (!Physics2D.OverlapCircle(currentChunk.transform.Find("Left Up").position, checkerRadius, terrainMask))
            {
                noTerrainPosition = currentChunk.transform.Find("Left Up").position;
                SpawnChunk();
            }
        }
        // Player is moving left down
        else if (playerMovement.moveDirection.y < 0 && playerMovement.moveDirection.x < 0)
        {
            if (!Physics2D.OverlapCircle(currentChunk.transform.Find("Left Down").position, checkerRadius, terrainMask))
            {
                noTerrainPosition = currentChunk.transform.Find("Left Down").position;
                SpawnChunk();
            }
        }
    }

    void SpawnChunk()
    {
        var random = Random.Range(0, terrainChunks.Count);
        latestChunk = Instantiate(terrainChunks[random], noTerrainPosition, Quaternion.identity);
        spawnedChunks.Add(latestChunk);
    }

    void ChunkOptimizer()
    {
        optimizerCooldown -= Time.deltaTime;

        if (optimizerCooldown <= 0f)
        {
            optimizerCooldown = optimizerCooldownDuration;
        }
        else
        {
            return;
        }

        foreach (var chunk in spawnedChunks)
        {
            optimizationDistance = Vector3.Distance(player.transform.position, chunk.transform.position);
            chunk.SetActive(optimizationDistance < maxOptimizationDistance);
        }
    }
}
