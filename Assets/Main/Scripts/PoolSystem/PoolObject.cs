using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PoolObject : MonoBehaviour
{
    public string Name;
    public bool InUse { get; private set; }
    public abstract void Initialize();

    public virtual void Spawn()
    {
        InUse = true;
    }
    public virtual void Dismiss()
    {
        InUse = false;
        gameObject.SetActive(false);
    }
}
