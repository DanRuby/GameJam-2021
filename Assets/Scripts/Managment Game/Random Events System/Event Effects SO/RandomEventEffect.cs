using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Base class for creating unique random event effects
/// </summary>
public abstract class RandomEventEffect : ScriptableObject
{
    public abstract void Activate();
    public abstract void Deactivate();
}
