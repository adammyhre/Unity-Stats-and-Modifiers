using System;
using UnityEngine;

public class StatModifier : IDisposable {
    readonly StatType type;
    public readonly Sprite icon;
    public bool MarkedForRemoval { get; set; }
    readonly IOperationStrategy strategy;
    
    public event Action<StatModifier> OnDispose = delegate { };
    
    readonly CountdownTimer timer;

    public StatModifier(StatType type, IOperationStrategy strategy, float duration) {
        this.type = type;
        this.strategy = strategy;
        if (duration <= 0) return;
        
        timer = new CountdownTimer(duration);
        timer.OnTimerStop += () => MarkedForRemoval = true;
        timer.Start();
    }
    
    public void Update(float deltaTime) => timer?.Tick(deltaTime);
    
    public void Handle(object sender, Query query) {
        if (query.StatType == type) {
            query.Value = strategy.Calculate(query.Value);
        }
    }

    public void Dispose() {
        OnDispose.Invoke(this);
    }
}