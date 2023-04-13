using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoreComponent : MonoBehaviour,ILogicUpdate
{
    protected Core core;

    protected virtual void Awake()
    {
        if (!transform.parent.TryGetComponent<Core>(out core))
            Debug.LogError("There is no core on the parent");
        core.AddComponent(this);
    }
    public virtual void LogicUpdate()
    {
        
    }
}
