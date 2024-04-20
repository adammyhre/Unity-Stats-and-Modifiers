using UnityEngine;
using UnityServiceLocator;

public class Bootstrapper : MonoBehaviour {
    void Awake() {
        ServiceLocator.Global.Register<IStatModifierFactory>(new StatModifierFactory());
    }
}