using UnityEngine;

public abstract class Bullet : MonoBehaviour
{
    protected float speed;
    [HideInInspector]
    public float damage;

    [HideInInspector]
    public bool live;

    public void Init(string tag, float speed, float damage, float life)
    {
        this.tag = tag;
        this.speed = speed;
        this.damage = damage;

        live = true;

        Invoke("Destroy", life);
    }

    private void Destroy()
    {
        BulletFactory.Instance.Push(gameObject);
    }
}