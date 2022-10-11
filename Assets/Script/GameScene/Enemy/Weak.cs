using UnityEngine;

public class Weak : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (tag == collision.tag)
        {
            return;
        }

        switch (collision.tag)
        {
            case "Player":
                Bullet bullet = collision.GetComponent<Bullet>();
                if (bullet.live)
                {
                    bullet.live = false;
                    GameManager.Instance.Enemy.Damage(collision.transform.position, bullet.damage * 2.0f);
                    BulletFactory.Instance.Push(collision.gameObject);
                }
                break;
        }
    }
}
