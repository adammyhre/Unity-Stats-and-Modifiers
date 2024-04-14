using System;
using System.Collections.Generic;

public class StatsMediator {
    readonly LinkedList<StatModifier> modifiers = new();

    public event EventHandler<Query> Queries;
    // NOTE: Delegates are invoked in the order they are added to the event.
    public void PerformQuery(object sender, Query query) => Queries?.Invoke(sender, query);

    public void AddModifier(StatModifier modifier) {
        modifiers.AddLast(modifier);
        modifier.MarkedForRemoval = false;
        Queries += modifier.Handle;
        
        modifier.OnDispose += _ => {
            modifiers.Remove(modifier);
            Queries -= modifier.Handle;
        };
    }

    public void Update(float deltaTime) {
        // Update all modifiers with deltaTime
        var node = modifiers.First;
        while (node != null) {
            var modifier = node.Value;
            modifier.Update(deltaTime);
            node = node.Next;
        }

        // Dispose any that are finished, a.k.a Mark and Sweep
        node = modifiers.First;
        while (node != null) {
            var nextNode = node.Next;

            if (node.Value.MarkedForRemoval) {
                node.Value.Dispose();
            }

            node = nextNode;
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