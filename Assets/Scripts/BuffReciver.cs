using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BuffReciver : MonoBehaviour
{
    
    public Action OnBuffsChanged;
    private List<Buff> buffs;
    public List<Buff> Buffs
    {
        get { return buffs; }
    }

    private void Start()
    {
        GameManager.Instance.buffReciveContainer.Add(gameObject, this);
        buffs = new List<Buff>();
    }


    public void AddBuff(Buff buff)
    {
        if (!buffs.Contains(buff))
            buffs.Add(buff);

        if (OnBuffsChanged != null)
            OnBuffsChanged();
    }

    public void RemoveBuff(Buff buff )
    {
        if (buffs.Contains(buff))
            buffs.Remove(buff);

        if (OnBuffsChanged != null)
            OnBuffsChanged();
    }

}
