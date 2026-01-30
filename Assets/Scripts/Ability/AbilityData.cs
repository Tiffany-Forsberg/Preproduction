using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AbilityData", menuName = "ScriptableObjects/AbilityData")]
public class AbilityData : ScriptableObject
{
    public string Name;
    [SerializeReference] public List<AbilityEffect> Effects;

    private void OnEnable()
    {
        if (string.IsNullOrEmpty(Name))
        {
            Name = name;
        }

        if (Effects == null)
        {
            Effects = new List<AbilityEffect>();
        }
    }
}

[Serializable]
public abstract class AbilityEffect
{
    public abstract void Execute(GameObject caster, GameObject target);
}

[Serializable]
public class DamageEffect : AbilityEffect
{
    public int Amount;
    public override void Execute(GameObject caster, GameObject target)
    {
        //target.GetComponent<Damageable>().ApplyDamage(Amount);
        Debug.Log(caster.name + " dealt " + Amount + " damage to " + target.name);
    }
}

[Serializable]
public class KnockbackEffect : AbilityEffect
{
    public float Force;
    public override void Execute(GameObject caster, GameObject target)
    {
        Vector2 direction = (target.transform.position - caster.transform.position).normalized;
        target.GetComponent<Rigidbody2D>().AddForce(direction * Force, ForceMode2D.Impulse);
        Debug.Log(caster.name + " knocked " + target.name + " with a force of " + Force + " in the direction of " + direction);
    }
}
