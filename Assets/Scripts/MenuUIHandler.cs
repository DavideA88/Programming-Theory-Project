using UnityEngine;
using UnityEngine.SceneManagement;
using static MainManager;

public class MenuUIHandler : MonoBehaviour
{
    private MainManager mainManager;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        mainManager = GameObject.Find("Main Manager").GetComponent<MainManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChooseClass(string heroClass)
    {
        switch (heroClass)
        {
            case "Warrior":
            Instance.playerClass = HeroClass.Warrior;
            break;

            case "Mage":
            Instance.playerClass = HeroClass.Mage;
            break;

            case "Assassin":
            Instance.playerClass = HeroClass.Assassin;
            break;
        }

        mainManager.StartGame();
    }

    public void AttackButton()
    {
        FindFirstObjectByType<Hero>().Attack();
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
