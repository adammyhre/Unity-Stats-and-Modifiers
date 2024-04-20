using System.Collections.Generic;
using System.Linq;

public class StatsMediator {
    readonly List<StatModifier> listModifiers = new();
    readonly Dictionary<StatType, IEnumerable<StatModifier>> modifiersCache = new();
    readonly IStatModifierApplicationOrder order = new NormalStatModifierOrder(); // OR INJECT

    public void PerformQuery(object sender, Query query) {
        if (!modifiersCache.ContainsKey(query.StatType)) {
            modifiersCache[query.StatType] = listModifiers.Where(modifier => modifier.Type == query.StatType).ToList();
        }
        query.Value = order.Apply(modifiersCache[query.StatType], query.Value);
    }

    void InvalidateCache(StatType statType) {
        modifiersCache.Remove(statType);
    }

    public void AddModifier(StatModifier modifier) {
        listModifiers.Add(modifier);
        InvalidateCache(modifier.Type);
        modifier.MarkedForRemoval = false;
        
        modifier.OnDispose += _ => InvalidateCache(modifier.Type);
        modifier.OnDispose += _ => listModifiers.Remove(modifier);
    }

    public void Update(float deltaTime) {
        foreach (var modifier in listModifiers) {
            modifier.Update(deltaTime);
        }
        
        foreach (var modifier in listModifiers.Where(modifier => modifier.MarkedForRemoval).ToList()) {
            modifier.Dispose();
        }
    }
}

public class Query {
    public readonly StatType StatType;
    public int Value;

    public Query(StatType statType, int value) {
        StatType = statType;
        Value = value;
    }
}