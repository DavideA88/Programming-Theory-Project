using System;
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
    private Slider healthBar;
    private Slider skillBar;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        enemyAnim = GetComponent<Animator>();

        healthBar = GameObject.Find("Enemy Health Bar").GetComponent<Slider>();
        skillBar = GameObject.Find("Enemy Skill Bar").GetComponent<Slider>();

        healthBar.maxValue = HealthPoints;
        healthBar.value = HealthPoints;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CheckEnemyTurn()
    {
        if (!MainManager.Instance.playerTurn)
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
        FindAnyObjectByType<Hero>().ReceiveDamage(CalculateDamage(attack) * 2);

        UpdateSkillPoints(-max_SP);

        EndEnemyTurn();
    }

    public void ReceiveDamage(int damage)
    {
        enemyAnim.SetTrigger("Hurt_trig");

        UpdateHealthPoints(damage);
        UpdateSkillPoints(20);

        if (HealthPoints <= 0)
        {
            HealthPoints = 0;
            Die();
        }
    }

    protected void Die()
    {
        enemyAnim.SetBool("IsAlive", false);
        enemyAnim.SetTrigger("Die_trig");
    }

    public int CalculateDamage(int baseAttack)
    {
        int damage = (int)Math.Round(baseAttack * UnityEngine.Random.Range(0.75f, 1.25f));

        return damage;
    }

    private void EndEnemyTurn()
    {
        MainManager.Instance.EndTurn();
    }
}
