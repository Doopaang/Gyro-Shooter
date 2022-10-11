using UnityEngine;

public class LaserBullet : Bullet
{
    void Update()
    {
        transform.Translate(Vector3.up * speed * Time.deltaTime);
    }

    public void SetBullet(float floatValue)
    {
        Vector3 scale = transform.localScale;
        scale.y = floatValue;
        transform.localScale = scale;
    }
}
