using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Rings : MonoBehaviour
{
    public List<GameObject> rings;
    
    private const float FADE_SPEED = 2.0f;

    private const float MOVE_SPEED = 100.0f;
    private const float MOVE_TIME = 0.2f;

    void Start()
    {
        foreach(GameObject obj in rings)
        {
            SpriteRenderer renderer = obj.GetComponent<SpriteRenderer>();
            //renderer.color = 
        }

        StartCoroutine(Fade());
        StartCoroutine(Move());
    }
    
    private IEnumerator Fade()
    {
        float alpha = 0.0f;
        while (alpha < 0.9f)
        {
            foreach (GameObject obj in rings)
            {
                SpriteRenderer renderer = obj.GetComponent<SpriteRenderer>();
                Color color = renderer.color;
                color.a = alpha;
                renderer.color = color;
            }
            alpha = Mathf.Lerp(alpha, 1.0f, FADE_SPEED * Time.deltaTime);
            yield return null;
        }
    }

    private IEnumerator Move()
    {
        int index = 0;
        float time = 0.0f;
        while (index < rings.Count)
        {
            int temp = index;
            for (GameObject obj = rings[temp]; temp < rings.Count; temp++)
            {
                obj = rings[temp];
                obj.transform.Translate(Vector3.up * MOVE_SPEED * Time.deltaTime);
            }
            time += Time.deltaTime;
            if (time >= MOVE_TIME)
            {
                time = 0.0f;
                index++;
            }
            yield return null;
        }
    }
}
