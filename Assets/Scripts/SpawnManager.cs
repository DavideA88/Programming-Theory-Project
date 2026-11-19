using UnityEngine;
using static MainManager;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] playerPrefabs;
    public GameObject[] enemyPrefabs;
    public Transform heroSpawnPoint;
    public Transform enemySpawnPoint;
    private int index;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SpawnHero();
        SpawnEnemy();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnHero()
    {
        if (Instance.playerClass == HeroClass.Warrior)
        {
            index = 0;
        } else if (Instance.playerClass == HeroClass.Mage)
        {
            index = 1;
        } else if (Instance.playerClass == HeroClass.Assassin)
        {
            index = 2;
        }

        Instantiate(playerPrefabs[index], heroSpawnPoint);
    }

    public void SpawnEnemy()
    {
        Instantiate(enemyPrefabs[Random.Range(0, 8)], enemySpawnPoint);
    }
}
