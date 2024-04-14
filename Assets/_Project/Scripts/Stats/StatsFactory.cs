using Malevolent;

public class StatsFactory {
    public Stats Create(BaseStats baseStats) {
        Preconditions.CheckNotNull(baseStats, nameof(baseStats));
        return new Stats(new StatsMediator(), baseStats);
    }
}