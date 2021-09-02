using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class RandomEventEffect : ScriptableObject
{
    public abstract void Activate();
    public abstract void Deactivate();
}
