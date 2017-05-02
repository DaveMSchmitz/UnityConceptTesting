using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class _Buff : MonoBehaviour {
    private PlayerAttack atk;
    private HealthController health;
    private PlayerMovementController movement;

    abstract public void effect();
    abstract public void duration();
}