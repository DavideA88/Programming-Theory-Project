using UnityEngine;

public class Mage : Hero
{
    protected override int m_HP { get; set; } = 650;
    protected override int attack { get; set; } = 100;

    [SerializeField]
    private ParticleSystem healParticules;
    
    public override void Skill()
    {
        base.Skill();

        // UpdateSkillPoints(-max_SP) called in animation event

        // DealSkillDamage() called in animation event

        // Heal() called in animation event

        // EndHeroTurn() called in animation event
    }

    private void Heal()
    {
        // Restore 100 HP to the mage when using skill and play particules animation
        healParticules.Play();

        UpdateHealthPoints(-200);
    }

    protected override void DealSkillDamage()
    {
        base.DealSkillDamage();
    }
}
