using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Name : MonoBehaviour
{
    [SerializeField]
    private Text nameText;

    private const float FONT_SIZE = 50.0f;
    private const float WIDTH_PLUS = 156.0f;

    private const float SPEED = 1000.0f;
    
    public void Active(string name)
    {
        gameObject.SetActive(true);
        nameText.text = name;
        Vector2 size = nameText.rectTransform.sizeDelta;
        size.x = name.Length * FONT_SIZE;
        nameText.rectTransform.sizeDelta = size;

        StartCoroutine(Fill());
    }

    private IEnumerator Fill()
    {
        RectTransform rectTransform = GetComponent<RectTransform>();
        Vector2 size = rectTransform.sizeDelta;
        size.x = 0.0f;
        while (size.x < nameText.rectTransform.sizeDelta.x + WIDTH_PLUS)
        {
            rectTransform.sizeDelta = size;
            size.x += SPEED * Time.smoothDeltaTime;
            yield return null;
        }
    }
}
