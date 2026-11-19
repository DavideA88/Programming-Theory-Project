using System;
using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public abstract class Hero : MonoBehaviour
{
    public int HealthPoints {
        get { return m_HP; }
        set {
            if (value < 0)
            {
                m_HP = 0;
            } else
            {
                m_HP = value;
            }
        }
    }
    protected abstract int m_HP { get; set; }

    public int SkillPoints {
        get { return m_SP; }
        set {
            if (value > max_SP)
            {
                m_SP = max_SP;
            } else
            {
                m_SP = value;
            }
        }
    }
    protected int m_SP = 0;

    public int max_SP { get; protected set; } = 100;

    protected abstract int attack { get; set; }

    protected bool isPlayerAlive;

    protected Animator heroAnim;
    private TextMeshProUGUI heroClassText;
    private TextMeshProUGUI HPtext;
    private TextMeshProUGUI SPtext;
    private Slider healthBar;
    private Slider skillBar;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        heroAnim = GetComponent<Animator>();

        heroClassText = GameObject.Find("Hero Class").GetComponent<TextMeshProUGUI>();
        HPtext = GameObject.Find("HP").GetComponent<TextMeshProUGUI>();
        SPtext = GameObject.Find("SP").GetComponent<TextMeshProUGUI>();
        healthBar = GameObject.Find("Player Health Bar").GetComponent<Slider>();
        skillBar = GameObject.Find("Player Skill Bar").GetComponent<Slider>();

        heroClassText.text = MainManager.Instance.playerClass.ToString();
        HPtext.text = "HP : " + HealthPoints + " / " + HealthPoints;
        SPtext.text = "SP : " + SkillPoints + " / " + max_SP;

        healthBar.maxValue = HealthPoints;
        healthBar.value = HealthPoints;

        skillBar.maxValue = max_SP;
        skillBar.value = SkillPoints;

        isPlayerAlive = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public virtual void Attack()
    {
        StartAttackAnimation();

        MainManager.Instance.UpdateUI();
        
        // DealDamage();

        // UpdateSkillPoints(20);

        // EndHeroTurn();
    }

    private void StartAttackAnimation()
    {
        heroAnim.SetTrigger("Attack_trig");
    }

    private void DealDamage()
    {
        FindAnyObjectByType<Enemy>().ReceiveDamage(CalculateDamage(attack));
    }

    protected void UpdateHealthPoints(int damage)
    {
        HealthPoints -= damage;
        healthBar.value = HealthPoints;
        HPtext.text = "HP : " + healthBar.value.ToString() + " / " + healthBar.maxValue.ToString();
    }

    private void UpdateSkillPoints(int value)
    {
        SkillPoints += value;
        skillBar.value = SkillPoints;
        SPtext.text = "SP : " + skillBar.value.ToString() + " / " + skillBar.maxValue.ToString();
    }

    public virtual void Skill()
    {
        StartSkillAnimation();

        MainManager.Instance.UpdateUI();

        // UpdateSkillPoints(-max_SP);

        // DealSkillDamage();

        // EndHeroTurn();
    }

    public virtual void StartSkillAnimation()
    {
        heroAnim.SetTrigger("Skill_trig");
    }

    protected virtual void DealSkillDamage()
    {
        FindAnyObjectByType<Enemy>().ReceiveDamage((int)Math.Round(CalculateDamage(attack) * 1.5f));
    }

    public virtual void ReceiveDamage(int damage)
    {
        heroAnim.SetTrigger("Hurt_trig");
        
        UpdateHealthPoints(damage);

        if (HealthPoints <= 0)
        {
            HealthPoints = 0;
            Die();
        }
    }

    protected void Die()
    {
        isPlayerAlive = false;
        heroAnim.SetBool("IsAlive", false);
        heroAnim.SetTrigger("Die_trig");

        // EndHeroTurn();
    }

    protected int CalculateDamage(int baseAttack)
    {
        int damage = (int)Math.Round(baseAttack * UnityEngine.Random.Range(0.85f, 1.15f));

        return damage;
    }

    private void EndHeroTurn()
    {
        if (isPlayerAlive)
        {
            MainManager.Instance.EndTurn();
        } else
        {
            MainManager.Instance.GameOver();
        }
    }
}
