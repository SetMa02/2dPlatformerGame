using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffEmiter : MonoBehaviour
{
    [SerializeField] private Buff buff;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if(GameManager.Instance.buffReciveContainer.ContainsKey(col.gameObject))
        {
            var reciver = GameManager.Instance.buffReciveContainer[col.gameObject];
            reciver.AddBuff(buff);
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (GameManager.Instance.buffReciveContainer.ContainsKey(col.gameObject))
        {
            var reciver = GameManager.Instance.buffReciveContainer[col.gameObject];
            reciver.RemoveBuff(buff);
        }
    }
}
[System.Serializable]
public class Buff
{
    public BuffType type;
    public float additiveBonus;
    public float multipliveBonus;
}

public enum BuffType : byte
{
    Damage, Force, Armor
}