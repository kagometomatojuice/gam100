using UnityEngine;

public class HumanSpawner : MonoBehaviour
{
    public GameObject[] midHumanPrefabs;  
    public Transform humanSpawner;     
    public float spawnRate = 2f;    
    public float minSpawnRate = 1f; 
    public float maxSpawnRate = 3f; 
    private float spawnTimer = 0f;
    public static int activeHumans = 0;
    public int maxActiveHumans = 5;
    
    void Start()
    {
        spawnRate = Random.Range(minSpawnRate, maxSpawnRate);
    }

    void Update()
    {
        spawnTimer += Time.deltaTime;

        if (spawnTimer >= spawnRate && activeHumans < maxActiveHumans)
        {
            SpawnHuman();
            spawnTimer = 0f;
            spawnRate = Random.Range(minSpawnRate, maxSpawnRate);
        }
    }

    // ReSharper disable Unity.PerformanceAnalysis
    public void SpawnHuman()
    {
        int rand = Random.Range(0, midHumanPrefabs.Length);
        GameObject selectedHuman = midHumanPrefabs[rand];
        
        GameObject human = Instantiate(selectedHuman, humanSpawner.position, Quaternion.identity);
        human.SetActive(true);
        
        activeHumans++;

        HumanBehaviour humanBehaviour = human.GetComponent<HumanBehaviour>();
        if (humanBehaviour != null)
        {
            // set direction to move right
            bool moveRight = humanSpawner.position.x < 0;
            humanBehaviour.SetDirection(true);
            
            SpriteRenderer spriteRenderer = human.GetComponent<SpriteRenderer>();
            if (spriteRenderer != null)
            {
                spriteRenderer.flipX = moveRight;
            }
        }
        
        Destroy(human.gameObject, 30f);
    }
    public static void DecrementActiveHumans()
    {
        activeHumans--;
    }
}