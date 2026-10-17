using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainManager : MonoBehaviour
{
    public static MainManager Instance { get; private set; }
    public enum HeroClass { Warrior, Mage, Assassin }
    public HeroClass playerClass;
    public bool playerTurn = true;
    private Enemy enemy;

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame()
    {
        SceneManager.LoadScene(1);
        
        playerTurn = true;
    }

    public void EndTurn()
    {
        playerTurn = !playerTurn;

        enemy = FindFirstObjectByType<Enemy>().GetComponent<Enemy>();

        enemy.Invoke(nameof(enemy.CheckEnemyTurn), 1);
    }
}
