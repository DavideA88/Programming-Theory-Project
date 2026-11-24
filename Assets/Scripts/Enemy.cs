using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    // ENCAPSULATION
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
    protected int m_HP { get; set; } = 1000;

    // ENCAPSULATION
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

    // ENCAPSULATION
    protected int max_SP { get; set; } = 100;

    // ENCAPSULATION
    protected int attack { get; set; } = 50;

    protected Animator enemyAnim;
    private SpawnManager spawnManager;
    private TextMeshProUGUI enemyNameText;
    private Slider healthBar;
    private Slider skillBar;
    private bool isEnemyAlive;
    private float waveMultiplier;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        enemyAnim = GetComponent<Animator>();
        spawnManager = GameObject.Find("Spawn Manager").GetComponent<SpawnManager>();

        // Initiating enemy stats
        enemyNameText = GameObject.Find("Enemy Name").GetComponent<TextMeshProUGUI>();
        healthBar = GameObject.Find("Enemy Health Bar").GetComponent<Slider>();
        skillBar = GameObject.Find("Enemy Skill Bar").GetComponent<Slider>();

        enemyNameText.text = gameObject.name.Replace("(Clone)", "");

        waveMultiplier = 1 + 0.1f * (MainManager.Instance.waveNumber - 1);

        HealthPoints = (int)Math.Round(HealthPoints * waveMultiplier);
        attack = (int)Math.Round(attack * waveMultiplier);

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
        // Check at the end of every turn if enemy has to play and can use skill
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

        // DealDamage() called in animation event

        // UpdateSkillPoints(20) called in animation event

        // EndEnemyTurn() called in animation event
    }

    // ABSTRACTION
    private void StartAttackAnimation()
    {
        enemyAnim.SetTrigger("Attack_trig");
    }

    // ABSTRACTION
    private void DealDamage()
    {
        // Dealing damage to hero based on attack and random modifier
        FindAnyObjectByType<Hero>().ReceiveDamage(CalculateDamage(attack));
    }

    private void UpdateHealthPoints(int damage)
    {
        // Update HP and UI
        HealthPoints -= damage;
        healthBar.value = HealthPoints;
    }

    private void UpdateSkillPoints(int value)
    {
        // Update SP and UI
        SkillPoints += value;
        skillBar.value = SkillPoints;
    }

    private void Skill()
    {
        StartSkillAnimation();

        // UpdateSkillPoints(-max_SP) called in animation event

        // DealSkillDamage() called in animation event

        // EndEnemyTurn() called in animation event
    }

    // ABSTRACTION
    public virtual void StartSkillAnimation()
    {
        enemyAnim.SetTrigger("Skill_trig");
    }

    // ABSTRACTION
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

        // EndEnemyTurn() called in animation event
    }

    public int CalculateDamage(int baseAttack)
    {
        // Adding a random modifier to attack damage
        int damage = (int)Math.Round(baseAttack * UnityEngine.Random.Range(0.85f, 1.15f));

        return damage;
    }

    private void EndEnemyTurn()
    {
        // At the end of the turn, enable action buttons for the player
        MainManager.Instance.UpdateUI();
        MainManager.Instance.EndTurn();
        
        // Check if enemy is alive, otherwise spawn another enemy
        if (!isEnemyAlive)
        {
            Destroy(gameObject);
            spawnManager.SpawnEnemy();
        }
    }
}
