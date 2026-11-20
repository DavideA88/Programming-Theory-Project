using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using static MainManager;

public class MenuUIHandler : MonoBehaviour
{
    private MainManager mainManager;
    public GameObject gameOverScreen;
    public AudioMixer masterMixer;

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

    public void SkillButton()
    {
        Hero hero = FindFirstObjectByType<Hero>();

        if (hero.SkillPoints == hero.max_SP)
        {
            hero.Skill();
        }
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void SetGlobalVolume(float volumeLevel)
    {
        masterMixer.SetFloat("masterVolume", Mathf.Log(volumeLevel) * 10);
    }

    public void SetMusicVolume(float volumeLevel)
    {
        masterMixer.SetFloat("musicVolume", Mathf.Log(volumeLevel) * 10);
    }

    public void SetSoundsVolume(float volumeLevel)
    {
        masterMixer.SetFloat("soundsVolume", Mathf.Log(volumeLevel) * 10);
    }
}
