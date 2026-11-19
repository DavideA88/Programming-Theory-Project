using UnityEngine;

public class Mage : Hero
{
    protected override int m_HP { get; set; } = 250;
    protected override int attack { get; set; } = 100;

    [SerializeField]
    private ParticleSystem healParticules;
    
    public override void Skill()
    {
        base.Skill();

        // UpdateSkillPoints(-max_SP);

        // DealSkillDamage();

        // Heal();

        // EndHeroTurn();
    }

    private void Heal()
    {
        healParticules.Play();

        UpdateHealthPoints(-100);
    }

    protected override void DealSkillDamage()
    {
        base.DealSkillDamage();
    }
}
