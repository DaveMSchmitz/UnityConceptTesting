using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Buff : MonoBehaviour {
    abstract public void setEffect(int e);
    abstract public void setDuration(int d);
    abstract public int getEffect();
    abstract public int getDuration();
    abstract public IEnumerator buff();
}