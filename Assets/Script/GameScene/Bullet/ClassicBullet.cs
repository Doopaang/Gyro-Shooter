using UnityEngine;

public class ClassicBullet : Bullet
{
    void Update()
    {
        transform.Translate(Vector3.up * speed * Time.deltaTime);
    }
}
