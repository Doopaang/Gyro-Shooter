using UnityEngine;
using System.Collections.Generic;

public class Enemy : MonoBehaviour
{
    private float MaxHP = 0;
    private float HP = 0;

    public List<Phase> phase;
    private int curIndex = 0;

    void Start()
    {
        GameManager.Instance.Enemy = this;

        foreach (Phase temp in phase)
        {
            MaxHP += temp.HP;
            temp.gameObject.SetActive(false);
        }
        HP = MaxHP;
        GameManager.Instance.SetHPGauge(HP, MaxHP);
        phase[curIndex].gameObject.SetActive(true);
        phase[curIndex].Active();
    }

    void Update()
    {
        if (phase[curIndex].HP <= 0.0f)
        {
            if (curIndex < phase.Count - 1)
            {
                NextPhase();
            }
            else
            {
                Destroy(gameObject);
            }
        }
        Move();
    }
    
    private void NextPhase()
    {
        phase[curIndex].End();
        curIndex++;
        phase[curIndex].Active();
    }

    private void Move()
    {
        phase[curIndex].pattern[phase[curIndex].curIndex].Move(transform);
    }

    public void Damage(Vector3 position, float damage)
    {
        phase[curIndex].HP -= damage;
        HP -= damage;
        GameManager.Instance.SetHPGauge(HP, MaxHP);
        GameManager.Instance.SetMeter(position, damage);
    }
}
