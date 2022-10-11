using UnityEngine;
using System.Collections.Generic;

public class Pattern : MonoBehaviour
{
    private List<GameObject> shooter;

    [SerializeField]
    private float delay;

    public PatternKind kind;

    [HideInInspector]
    public float end;
    private float defaultEnd;

    public MoveKind moveKind;

    [HideInInspector]
    public float speed;

    public enum PatternKind
    {
        Unlimited, End, Time
    }

    public enum MoveKind
    {
        Stand, Spin, Chase
    }

    private void Awake()
    {
        defaultEnd = end;

        shooter = new List<GameObject>();
        for(int count = 0; count < transform.childCount; count++)
        {
            shooter.Add(transform.GetChild(count).gameObject);
        }
    }

    void Update()
    {
        switch (kind)
        {
            case PatternKind.Unlimited:
                end = 1.0f;
                break;

            case PatternKind.End:
                end -= Time.deltaTime;
                break;
        }
        if (end <= 0.0f)
        {
            End();
        }
    }

    public void Active()
    {
        Invoke("ActiveDelay", delay);
    }

    public void ActiveDelay()
    {
        end = defaultEnd;
        foreach (GameObject temp in shooter)
        {
            temp.GetComponent<ShooterAction>().Active();
            temp.GetComponent<BulletSetting>().Active();
        }
        switch (kind)
        {
            case PatternKind.Time:
                Invoke("End", end);
                break;
        }
    }

    public void End()
    {
        CancelInvoke();
        foreach (GameObject temp in shooter)
        {
            temp.GetComponent<BulletSetting>().End();
        }
        Phase phase = transform.parent.GetComponent<Phase>();
        if (!phase.gameObject.activeSelf)
        {
            return;
        }
        phase.curIndex = Random.Range(0, phase.pattern.Count);
        phase.Active();
    }

    public void Move(Transform transform)
    {
        switch (moveKind)
        {
            case MoveKind.Stand:
                break;

            case MoveKind.Spin:
                transform.Rotate(Vector3.forward, speed * Time.smoothDeltaTime);
                break;

            case MoveKind.Chase:
                if(GameManager.Instance.Player == null)
                {
                    break;
                }
                transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(Vector3.forward, GameManager.Instance.Player.transform.position - transform.position), speed * Time.deltaTime);
                break;
        }
    }
}
