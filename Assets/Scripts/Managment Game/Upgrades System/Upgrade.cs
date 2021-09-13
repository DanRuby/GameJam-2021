using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Base class for upgrades
/// </summary>
public abstract class Upgrade : ScriptableObject
{
    public abstract void Activate();
}
