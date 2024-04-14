using System;
using UnityEngine;

public class BasicStatModifier : StatModifier {
    readonly StatType type;
    readonly Func<int, int> operation;

    public BasicStatModifier(StatType type, float duration, Func<int, int> operation) : base(duration) {
        this.type = type;
        this.operation = operation;
    }

    public override void Handle(object sender, Query query) {
        if (query.StatType == type) {
            query.Value = operation(query.Value);
        }
    }
}

public abstract class StatModifier : IDisposable {
    public readonly Sprite icon;
    public bool MarkedForRemoval { get; set; }
    
    public event Action<StatModifier> OnDispose = delegate { };
    
    readonly CountdownTimer timer;

    protected StatModifier(float duration) {
        if (duration <= 0) return;
        
        timer = new CountdownTimer(duration);
        timer.OnTimerStop += () => MarkedForRemoval = true;
        timer.Start();
    }
    
    public void Update(float deltaTime) => timer?.Tick(deltaTime);
    
    public abstract void Handle(object sender, Query query);

    public void Dispose() {
        OnDispose.Invoke(this);
    }
}