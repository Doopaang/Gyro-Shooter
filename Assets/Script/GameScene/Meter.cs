using UnityEngine;
using UnityEngine.UI;

public class Meter : MonoBehaviour
{
    private Text text;
    private Color color;

    private float speed = 150.0f;
    private const float DOWN_SPEED = 5.0f;

    private const float FADE_SPEED = 5.0f;

    void Update()
    {
        speed -= DOWN_SPEED;

        transform.Translate(Vector3.up * speed * Time.deltaTime);
        if (speed < 0.0f)
        {
            color.a -= FADE_SPEED * Time.deltaTime;
            text.color = color;
            if (color.a <= 0.0f)
            {
                Destroy(gameObject);
            }
        }
    }

    public void Init(float value)
    {
        text = GetComponent<Text>();
        color = text.color;

        text.text = "-" + value;
    }
}
