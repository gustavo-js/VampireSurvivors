using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropRandomizer : MonoBehaviour
{
    public List<GameObject> propSpawnPoints;
    public List<GameObject> propPrefabs;

    // Start is called before the first frame update
    void Start()
    {
        SpawnProps();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SpawnProps()
    {
        foreach (var spawnpoint in propSpawnPoints)
        {
            var random = Random.Range(0, propPrefabs.Count);
            var prop = Instantiate(propPrefabs[random], spawnpoint.transform.position, Quaternion.identity);
            prop.transform.parent = spawnpoint.transform;
        }
    }
}
