using UnityEngine;

public class ShooterAction : MonoBehaviour
{
    public ShooterKind kind;
    
    [HideInInspector]
    public float value;

    private Quaternion defaultRotation;
    private Vector3 vec;
    private Quaternion rotation;

    public enum ShooterKind
    {
        Stand, Spin, Chase, Rand
    }

    void Awake()
    {
        defaultRotation = transform.localRotation;
    }
    
    void Update()
    {
        switch (kind)
        {
            case ShooterKind.Stand:
                Stand();
                break;

            case ShooterKind.Spin:
                Spin();
                break;

            case ShooterKind.Chase:
                Chase();
                break;

            case ShooterKind.Rand:
                Rand();
                break;
        }
    }

    private void Init()
    {
        switch (kind)
        {
            case ShooterKind.Stand:
                break;

            case ShooterKind.Spin:
                if (GameManager.Instance.Player)
                {
                    transform.rotation = Quaternion.LookRotation(Vector3.forward, GameManager.Instance.Player.transform.position - transform.position);
                }
                rotation = transform.rotation;
                break;

            case ShooterKind.Chase:
            case ShooterKind.Rand:
                if (GameManager.Instance.Player)
                {
                    transform.rotation = Quaternion.LookRotation(Vector3.forward, GameManager.Instance.Player.transform.position - transform.position);
                }
                break;
        }
        Update();
    }

    public void Active()
    {
        transform.localRotation = defaultRotation;
        Init();
    }
    
    private void Stand()
    {
        vec = defaultRotation.eulerAngles;
        transform.rotation = transform.parent.transform.rotation;
        transform.Rotate(vec);
    }

    private void Spin()
    {
        transform.rotation = rotation;
        transform.Rotate(Vector3.forward, value * Time.deltaTime);
        rotation = transform.rotation;
    }

    private void Chase()
    {
        if (GameManager.Instance.Player != null)
        {
            transform.rotation = Quaternion.LookRotation(Vector3.forward, GameManager.Instance.Player.transform.position - transform.position);
            transform.Rotate(Vector3.forward, value);
        }
    }

    private void Rand()
    {
        if (GameManager.Instance.Player)
        {
            transform.rotation = Quaternion.LookRotation(Vector3.forward, GameManager.Instance.Player.transform.position - transform.position);
        }
        transform.Rotate(Vector3.forward, Random.Range(-value * 0.5f, value * 0.5f));
    }
}
