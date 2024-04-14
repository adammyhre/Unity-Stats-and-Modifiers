public enum StatType { Attack, Defense }

public class Stats {
    readonly StatsMediator mediator;
    readonly BaseStats baseStats;
    
    public StatsMediator Mediator => mediator;
    
    public int Attack {
        get {
            var q = new Query(StatType.Attack, baseStats.attack);
            mediator.PerformQuery(this, q);
            return q.Value;
        }
    }
    
    public int Defense {
        get {
            var q = new Query(StatType.Defense, baseStats.defense);
            mediator.PerformQuery(this, q);
            return q.Value;
        }
    }
    
    public Stats(StatsMediator mediator, BaseStats baseStats) {
        this.mediator = mediator;
        this.baseStats = baseStats;
    }
    
    public override string ToString() => $"Attack: {Attack}, Defense: {Defense}";
}