using UnityEngine;
using static MainManager;

public class MenuUIHandler : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
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

        StartGame();
    }
}
