using NUnit.Framework;
using UnityEngine;
using System;

public class Assassin : Hero
{
    protected override int m_HP { get; set; } = 250;
    protected override int attack { get; set; } = 120;

    private bool isDodging = false;
    private int dodgeCounter;
    
    public override void Skill()
    {
        base.Skill();

        // UpdateSkillPoints(-max_SP);

        // DealSkillDamage();

        // Dodge();

        // EndHeroTurn();
    }

    private void Dodge()
    {
        isDodging = true;
        heroAnim.SetBool("IsDodging", true);

        dodgeCounter = 1;
    }

    protected override void DealSkillDamage()
    {
        FindAnyObjectByType<Enemy>().ReceiveDamage(CalculateDamage(attack) * 3);
    }

    public override void ReceiveDamage(int damage)
    {
        if (isDodging && dodgeCounter > 0)
        {
            heroAnim.SetTrigger("Dodge_trig");

            dodgeCounter--;

            if (dodgeCounter == 0)
            {
                isDodging = false;
                heroAnim.SetBool("IsDodging", false);
            }
        } else
        {
            base.ReceiveDamage(damage);
        }
    }
}
