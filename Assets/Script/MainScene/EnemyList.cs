using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class EnemyList : MonoBehaviour
{
    [SerializeField]
    private List<Sprite> enemy;
    private int index = 0;

    private Vector3 defaultPosition;

    private const float FILL_SPEED = 2.0f;

    private float time = 0.0f;
    private const float TIME_SPEED = 0.4f;
    private const float MOVE_SPEED = 2.0f;

    void Start()
    {
        defaultPosition = transform.position;
    }
    
    public void Active()
    {
        StopAllCoroutines();
        gameObject.SetActive(true);

        StartCoroutine(Print());
        StartCoroutine(Move());
    }

    private IEnumerator Print()
    {
        bool end = false;

        MainManager.Instance.EndName();

        Image image = GetComponent<Image>();
        image.sprite = enemy[index];
        image.fillAmount = 0.0f;
        name = image.sprite.name;

        RectTransform rectTransform = GetComponent<RectTransform>();
        rectTransform.sizeDelta = image.sprite.rect.size * 0.5f;

        while (image.fillAmount < 1.0f)
        {
            image.fillAmount += FILL_SPEED * Time.smoothDeltaTime;
            if (image.fillAmount >= 0.5f &&
                end == false)
            {
                end = true;
                MainManager.Instance.StartName();
            }
            yield return null;
        }
    }

    private IEnumerator Move()
    {
        float time = 0.0f;
        while(true)
        {
            time += Time.deltaTime;
            transform.Translate(Vector3.up * TIME_SPEED * Mathf.Sin(MOVE_SPEED * time));
            yield return new WaitForFixedUpdate();
        }
    }

    public void NextEnemy()
    {
        if (index < enemy.Count - 1)
        {
            index++;
        }
        else
        {
            index = 0;
        }
        StartCoroutine(Print());
    }

    public void PrevEnemy()
    {
        if (index > 0)
        {
            index--;
        }
        else
        {
            index = enemy.Count - 1;
        }
        StartCoroutine(Print());
    }

    public string GetEnemy()
    {
        return enemy[index].name;
    }
}
