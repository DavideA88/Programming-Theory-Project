using System.Collections;
using System.Linq;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainManager : MonoBehaviour
{
    // ENCAPSULATION
    public static MainManager Instance { get; private set; }
    public enum HeroClass { Warrior, Mage, Assassin }
    public HeroClass playerClass;
    public bool playerTurn = true;
    public int waveNumber;
    private Enemy enemy;
    private MenuUIHandler menuUI;

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

        waveNumber = 0;
    }

    public void GameOver()
    {
        // Get the Game Over Screen GameObject from the script on the second scene
        menuUI = GameObject.Find("Canvas").GetComponent<MenuUIHandler>();
        
        menuUI.gameOverScreen.SetActive(true);

        // Switch music when player dies
        GameObject.Find("Background Music").GetComponent<AudioSource>().Stop();
        GameObject.Find("Game Over Music").GetComponent<AudioSource>().Play();
    }

    public void EndTurn()
    {
        // Switch turns after action and check if enemy has to attack
        playerTurn = !playerTurn;

        enemy = FindFirstObjectByType<Enemy>().GetComponent<Enemy>();

        enemy.Invoke(nameof(enemy.CheckEnemyTurn), 1);        
    }

    public void UpdateUI()
    {
        // Disable action buttons after player has attacked
        GameObject[] buttons = GameObject.FindGameObjectsWithTag("ActionButton");

        foreach (var item in buttons)
        {
            if (playerTurn || (item.name == "Skill Button" && FindFirstObjectByType<Hero>().GetComponent<Hero>().SkillPoints < 100))
            {
                item.GetComponent<Button>().interactable = false;

                item.GetComponentInChildren<TextMeshProUGUI>().alpha = 0.5f;
            } else
            {
                item.GetComponent<Button>().interactable = true;

                item.GetComponentInChildren<TextMeshProUGUI>().alpha = 1;
            }
        }
    }
}
