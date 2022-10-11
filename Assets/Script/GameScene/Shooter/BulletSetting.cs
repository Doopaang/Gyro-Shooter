using UnityEngine;
using System.Collections;

public class BulletSetting : MonoBehaviour
{
    public BulletFactory.BulletKind kind;
    public bool shotAwake;
    [HideInInspector]
    public float waitTime = 0.0f;

    [SerializeField]
    private float bulletDelay;
    [SerializeField]
    private float bulletSpeed;
    [SerializeField]
    private float bulletDamage;
    [SerializeField]
    private float bulletLife;
    
    [HideInInspector]
    public float m_paramFloat;
    public float ParamFloat
    {
        get
        {
            return m_paramFloat;
        }
        set
        {
            m_paramFloat = value;
        }
    }
    [HideInInspector]
    public BulletFactory.BulletKind paramKind;

    public void Active()
    {
        Invoke("ActiveDelay", waitTime);
    }

    public void ActiveDelay()
    {
        StartCoroutine(Shot());
    }

    public void End()
    {
        CancelInvoke("ActiveDelay");
        StopCoroutine(Shot());
    }

    private IEnumerator Shot()
    {
        while (true)
        {
            GameObject bullet = BulletFactory.Instance.GetBullet(kind, transform, bulletSpeed, bulletDamage, bulletLife);
            bullet.transform.rotation = transform.rotation;
            switch (kind)
            {
                case BulletFactory.BulletKind.Classic:
                    break;

                case BulletFactory.BulletKind.Chase:
                    {
                        ChaseBullet script = bullet.GetComponent<ChaseBullet>();
                        script.SetBullet(ParamFloat);
                        break;
                    }

                case BulletFactory.BulletKind.Laser:
                    {
                        LaserBullet script = bullet.GetComponent<LaserBullet>();
                        script.SetBullet(ParamFloat);
                        break;
                    }
            }
            if (tag == "Player")
            {
                yield return new WaitForSecondsRealtime(bulletDelay);
            }
            else
            {
                yield return new WaitForSeconds(bulletDelay);
            }
        }
    }
}
