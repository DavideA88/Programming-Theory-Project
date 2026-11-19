using UnityEngine;

public class Warrior : Hero
{
    protected override int m_HP { get; set; } = 400;
    protected override int attack { get; set; } = 80;

    private bool isBlocking = false;
    private int blockCounter;

    public override void Skill()
    {
        base.Skill();

        // UpdateSkillPoints(-max_SP);

        // DealSkillDamage();

        // Block();

        // EndHeroTurn();
    }

    private void Block()
    {
        isBlocking = true;
        heroAnim.SetBool("IsBlocking", true);

        blockCounter = 2;
    }

    protected override void DealSkillDamage()
    {
        FindAnyObjectByType<Enemy>().ReceiveDamage(CalculateDamage(attack) * 2);
    }

    public override void ReceiveDamage(int damage)
    {
        if (isBlocking && blockCounter > 0)
        {
            heroAnim.SetTrigger("Block_trig");

            UpdateHealthPoints(damage / 2);

            if (HealthPoints <= 0)
            {
                HealthPoints = 0;
                Die();
            }
            
            blockCounter--;
            
            if (blockCounter == 0)
            {
                isBlocking = false;
                heroAnim.SetBool("IsBlocking", false);
            }
        } else
        {
            base.ReceiveDamage(damage);
        }
    }
}
