using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
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
    protected int m_HP { get; set; } = 400;

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

    protected int max_SP { get; set; } = 100;

    protected int attack { get; set; } = 40;

    protected Animator enemyAnim;
    private SpawnManager spawnManager;
    private TextMeshProUGUI enemyNameText;
    private Slider healthBar;
    private Slider skillBar;
    private bool isEnemyAlive;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        enemyAnim = GetComponent<Animator>();
        spawnManager = GameObject.Find("Spawn Manager").GetComponent<SpawnManager>();

        enemyNameText = GameObject.Find("Enemy Name").GetComponent<TextMeshProUGUI>();
        healthBar = GameObject.Find("Enemy Health Bar").GetComponent<Slider>();
        skillBar = GameObject.Find("Enemy Skill Bar").GetComponent<Slider>();

        enemyNameText.text = gameObject.name.Replace("(Clone)", "");

        healthBar.maxValue = HealthPoints;
        healthBar.value = HealthPoints;

        skillBar.maxValue = max_SP;
        skillBar.value = SkillPoints;

        isEnemyAlive = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CheckEnemyTurn()
    {
        if (!MainManager.Instance.playerTurn && isEnemyAlive)
        {
            if (SkillPoints == max_SP)
            {
                Skill();
            } else
            {
                Attack();
            }
        }
    }

    protected void Attack()
    {
        StartAttackAnimation();

        // DealDamage();

        // UpdateSkillPoints(20);

        // EndEnemyTurn();
    }

    private void StartAttackAnimation()
    {
        enemyAnim.SetTrigger("Attack_trig");
    }

    private void DealDamage()
    {
        FindAnyObjectByType<Hero>().ReceiveDamage(CalculateDamage(attack));
    }

    private void UpdateHealthPoints(int damage)
    {
        HealthPoints -= damage;
        healthBar.value = HealthPoints;
    }

    private void UpdateSkillPoints(int value)
    {
        SkillPoints += value;
        skillBar.value = SkillPoints;
    }

    private void Skill()
    {
        StartSkillAnimation();

        // UpdateSkillPoints(-max_SP);

        // DealSkillDamage();

        // EndEnemyTurn();
    }

    public virtual void StartSkillAnimation()
    {
        enemyAnim.SetTrigger("Skill_trig");
    }

    protected virtual void DealSkillDamage()
    {
        FindAnyObjectByType<Hero>().ReceiveDamage(CalculateDamage(attack) * 2);
    }

    public void ReceiveDamage(int damage)
    {
        enemyAnim.SetTrigger("Hurt_trig");

        UpdateHealthPoints(damage);

        if (HealthPoints <= 0)
        {
            HealthPoints = 0;
            Die();
        }
    }

    protected void Die()
    {
        isEnemyAlive = false;
        enemyAnim.SetBool("IsAlive", false);
        enemyAnim.SetTrigger("Die_trig");

        // EndEnemyTurn();
    }

    public int CalculateDamage(int baseAttack)
    {
        int damage = (int)Math.Round(baseAttack * UnityEngine.Random.Range(0.85f, 1.15f));

        return damage;
    }

    private void EndEnemyTurn()
    {
        MainManager.Instance.UpdateUI();
        MainManager.Instance.EndTurn();
        
        if (!isEnemyAlive)
        {
            Destroy(gameObject);
            spawnManager.SpawnEnemy();
        }
    }
}
