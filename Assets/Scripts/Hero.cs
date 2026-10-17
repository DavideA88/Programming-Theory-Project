using System;
using System.Collections;
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

    protected int max_SP { get; set; } = 100;

    protected abstract int attack { get; set; }

    protected Animator heroAnim;
    private Slider healthBar;
    private Slider skillBar;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        heroAnim = GetComponent<Animator>();

        healthBar = GameObject.Find("Player Health Bar").GetComponent<Slider>();
        skillBar = GameObject.Find("Player Skill Bar").GetComponent<Slider>();

        healthBar.maxValue = HealthPoints;
        healthBar.value = HealthPoints;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public virtual void Attack()
    {
        StartAttackAnimation();
        
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

    public virtual void Skill()
    {
        UpdateSkillPoints(-max_SP);

        EndHeroTurn();
    }

    public void ReceiveDamage(int damage)
    {
        heroAnim.SetTrigger("Hurt_trig");
        
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
        heroAnim.SetBool("IsAlive", false);
        heroAnim.SetTrigger("Die_trig");
    }

    protected int CalculateDamage(int baseAttack)
    {
        int damage = (int)Math.Round(baseAttack * UnityEngine.Random.Range(0.75f, 1.25f));

        return damage;
    }

    private void EndHeroTurn()
    {
        MainManager.Instance.EndTurn();
    }
}
