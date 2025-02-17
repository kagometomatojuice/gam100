using UnityEngine;

public class HumanSpawner2 : MonoBehaviour
{
    public GameObject[] midHumanPrefabs; 
    public Transform humanSpawner2;       
    public float spawnRate = 2f;
    public float minSpawnRate = 1f;
    public float maxSpawnRate = 3f; 
    private float spawnTimer = 0f;
    
    void Start()
    {
        spawnRate = Random.Range(minSpawnRate, maxSpawnRate);
    }


    void Update()
    {
        spawnTimer += Time.deltaTime;
        
        if (spawnTimer >= spawnRate)
        {
            SpawnHuman();
            spawnTimer = 0f;
            spawnRate = Random.Range(minSpawnRate, maxSpawnRate);
        }
    }

    void SpawnHuman()
    {
        int rand = Random.Range(0, midHumanPrefabs.Length);
        GameObject selectedHuman = midHumanPrefabs[rand];
        
        GameObject human = Instantiate(selectedHuman, humanSpawner2.position, Quaternion.identity);
        human.SetActive(true);
        
        HumanBehaviour humanBehaviour = human.GetComponent<HumanBehaviour>();
        if (humanBehaviour != null)
        {
            // set direction to move left
            humanBehaviour.SetDirection(false);
        }
    }
}