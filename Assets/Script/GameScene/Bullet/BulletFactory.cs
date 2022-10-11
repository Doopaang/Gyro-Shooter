using UnityEngine;
using System.Collections.Generic;

public class BulletFactory : MonoBehaviour
{
    public static BulletFactory Instance { get; private set; }

    private List<GameObject> pool;
    public int START_BULLET;

    public GameObject bullet;

    public enum BulletKind
    {
        Classic, Chase, Laser
    }

    void Awake()
    {
        Instance = this;

        pool = new List<GameObject>();
    }

    void Start()
    {
        for (int count = 0; count < START_BULLET; count++)
        {
            CreateItem();
        }
    }

    private void CreateItem()
    {
        GameObject obj = Instantiate(bullet, transform) as GameObject;
        pool.Add(obj);
    }

    public void Push(GameObject obj)
    {
        Destroy(obj.GetComponent<Bullet>());
        obj.transform.localScale = Vector3.one;
        obj.SetActive(false);
        pool.Add(obj);
    }

    private GameObject Pop()
    {
        if (pool.Count == 0)
        {
            CreateItem();
        }
        
        GameObject obj = pool[0];
        pool.RemoveAt(0);
        obj.SetActive(true);
        return obj;
    }
    
    public GameObject GetBullet(BulletKind kind, Transform trans, float speed, float damage, float life)
    {
        GameObject obj = Pop();
        SpriteRenderer renderer = obj.GetComponent<SpriteRenderer>();

        string path = "Sprite/Bullet/" + trans.tag + kind.ToString();
        renderer.sprite = Resources.Load(path, typeof(Sprite)) as Sprite;

        Bullet script = obj.GetComponent<Bullet>();
        switch (kind)
        {
            case BulletKind.Classic:
                script = obj.AddComponent<ClassicBullet>();
                break;

            case BulletKind.Chase:
                script = obj.AddComponent<ChaseBullet>();
                break;

            case BulletKind.Laser:
                script = obj.AddComponent<LaserBullet>();
                break;

            default:
                throw new System.Exception();
        }
        script.Init(trans.tag, speed, damage, life);
        obj.transform.position = trans.position;

        BoxCollider2D coll = obj.GetComponent<BoxCollider2D>();
        coll.size = renderer.size;

        return obj;
    }
}
