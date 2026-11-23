using TMPro;
using UnityEditor.Rendering;
using UnityEngine;
using static MainManager;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] playerPrefabs;
    public GameObject[] enemyPrefabs;
    public Transform heroSpawnPoint;
    public Transform enemySpawnPoint;
    public TextMeshProUGUI waveNumberText;
    private int index;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SpawnHero();
        SpawnEnemy();

        waveNumberText.text = "Wave " + Instance.waveNumber;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnHero()
    {
        // Spawning hero prefab based on class chosen by the player
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
        // Spawning a random enemy prefab
        Instantiate(enemyPrefabs[Random.Range(0, 8)], enemySpawnPoint);

        Instance.waveNumber++;
        waveNumberText.text = "Wave " + Instance.waveNumber;
    }
}
