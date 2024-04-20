using System;

public interface IStatModifierFactory {
    StatModifier Create(OperatorType operatorType, StatType type, int value, float duration);
}

public class StatModifierFactory : IStatModifierFactory {
    public StatModifier Create(OperatorType operatorType, StatType type, int value, float duration) {
        return operatorType switch {
            OperatorType.Add => new BasicStatModifier(type, duration, v => v + value),
            OperatorType.Multiply => new BasicStatModifier(type, duration, v => v * value),
            _ => throw new ArgumentOutOfRangeException()
        };
    }
}