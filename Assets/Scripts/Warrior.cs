using UnityEngine;

public class Warrior : Hero
{
    // INHERITANCE
    protected override int m_HP { get; set; } = 1200;
    protected override int attack { get; set; } = 80;

    private bool isBlocking = false;
    private int blockCounter;

    // POLYMORPHISM
    public override void Skill()
    {
        base.Skill();

        // UpdateSkillPoints(-max_SP) called in animation event

        // DealSkillDamage() called in animation event

        // Block() called in animation event

        // EndHeroTurn() called in animation event
    }

    private void Block()
    {
        // Warrior enters blocking state, dividing damage by 2, for 2 turns
        isBlocking = true;
        heroAnim.SetBool("IsBlocking", true);

        blockCounter = 2;
    }

    // POLYMORPHISM
    protected override void DealSkillDamage()
    {
        FindAnyObjectByType<Enemy>().ReceiveDamage(CalculateDamage(attack) * 2);
    }

    // POLYMORPHISM
    public override void ReceiveDamage(int damage)
    {
        // Check if warrior is in blocking state, and divide damage by 2
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
            
            // Disable blocking state after 2 turns
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
