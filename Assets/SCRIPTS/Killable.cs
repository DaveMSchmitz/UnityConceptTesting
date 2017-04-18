using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Killable : MonoBehaviour{

    abstract public void killed();
    abstract public void healthChanged();
}
