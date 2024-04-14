using System;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class StatModifierPickup : Pickup {
    public enum OperatorType { Add, Multiply }
    
    // TODO Move configuration to ScriptableObject
    [SerializeField] StatType type = StatType.Attack;
    [SerializeField] OperatorType operatorType = OperatorType.Add;
    [SerializeField] int value = 10;
    [SerializeField] float duration = 5f;

    protected override void ApplyPickupEffect(Entity entity) {
        StatModifier modifier = operatorType switch {
            OperatorType.Add => new BasicStatModifier(type, duration, v => v + value),
            OperatorType.Multiply => new BasicStatModifier(type, duration, v => v * value),
            _ => throw new ArgumentOutOfRangeException()
        };
        
        entity.Stats.Mediator.AddModifier(modifier);
    }
}