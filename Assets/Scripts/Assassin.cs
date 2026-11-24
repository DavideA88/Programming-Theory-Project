using NUnit.Framework;
using UnityEngine;
using System;

public class Assassin : Hero
{
    // INHERITANCE
    protected override int m_HP { get; set; } = 800;
    protected override int attack { get; set; } = 120;

    private bool isDodging = false;
    private int dodgeCounter;
    
    // POLYMORPHISM
    public override void Skill()
    {
        base.Skill();

        // UpdateSkillPoints(-max_SP) called in animation event

        // DealSkillDamage() called in animation event

        // Dodge() called in animation event

        // EndHeroTurn() called in animation event
    }

    private void Dodge()
    {
        // Assassin enters dodging state, cancelling the next attack from enemy
        isDodging = true;
        heroAnim.SetBool("IsDodging", true);

        dodgeCounter = 1;
    }

    // POLYMORPHISM
    protected override void DealSkillDamage()
    {
        FindAnyObjectByType<Enemy>().ReceiveDamage(CalculateDamage(attack) * 3);
    }

    // POLYMORPHISM
    public override void ReceiveDamage(int damage)
    {
        // Check if assassin is in dodging state, and don't deal any damage
        if (isDodging && dodgeCounter > 0)
        {
            heroAnim.SetTrigger("Dodge_trig");

            dodgeCounter--;

            // Disable dodging state after 1 turn
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
