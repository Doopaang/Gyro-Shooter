using UnityEngine;
using System.Collections.Generic;

public class Phase : MonoBehaviour
{
    public List<Pattern> pattern;
    [HideInInspector]
    public int curIndex;

    public float HP;
    
    public void Active()
    {
        gameObject.SetActive(true);
        foreach (Pattern temp in pattern)
        {
            temp.gameObject.SetActive(false);
        }
        pattern[curIndex].gameObject.SetActive(true);
        pattern[curIndex].Active();
    }

    public void End()
    {
        gameObject.SetActive(false);
        foreach (Pattern temp in pattern)
        {
            temp.End();
            temp.gameObject.SetActive(false);
        }
    }
}
