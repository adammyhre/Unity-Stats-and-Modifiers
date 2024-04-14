using Sirenix.OdinInspector;
using UnityEngine;

public abstract class Entity : MonoBehaviour, IVisitable {
    [SerializeField, InlineEditor, Required] BaseStats baseStats;
    public Stats Stats { get; private set; }

    void Awake() {
        Stats = new Stats(new StatsMediator(), baseStats);
    }

    public void Update() {
        Stats.Mediator.Update(Time.deltaTime);
    }
    
    public void Accept(IVisitor visitor) => visitor.Visit(this);
}