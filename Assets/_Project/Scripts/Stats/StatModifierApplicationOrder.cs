using System.Collections.Generic;
using System.Linq;

public interface IStatModifierApplicationOrder {
    int Apply(IEnumerable<StatModifier> statModifiers, int baseValue);
}

public class NormalStatModifierOrder : IStatModifierApplicationOrder {
    public int Apply(IEnumerable<StatModifier> statModifiers, int baseValue) {
        var allModifiers = statModifiers.ToList();

        foreach (var modifier in allModifiers.Where(modifier => modifier.Strategy is AddOperation)) {
            baseValue = modifier.Strategy.Calculate(baseValue);
        }
        
        foreach (var modifier in allModifiers.Where(modifier => modifier.Strategy is MultiplyOperation)) {
            baseValue = modifier.Strategy.Calculate(baseValue);
        }
        
        return baseValue;
    }
}