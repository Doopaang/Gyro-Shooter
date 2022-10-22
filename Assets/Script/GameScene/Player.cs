using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    GameObject shooter;
    [SerializeField]
    private GameObject gauge;

    private float skill;
    [SerializeField]
    private float SKILL_MAX;
    [SerializeField]
    private float SKILL_UP;
    [SerializeField]
    private float SKILL_DOWN;

    [SerializeField]
    private float ANGLE;

    private float shotSpeed;
    [SerializeField]
    private float SHOT_DEFAULT;

    [SerializeField]
    private float TIME_DEFAULT;
    [SerializeField]
    private float TIME_SLOW;

    void Start()
    {
        shooter.GetComponent<ShooterAction>().Active();
        shooter.GetComponent<BulletSetting>().Active();
    }

    void FixedUpdate()
    {
        Skill();
        Move();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (tag == collision.tag)
        {
            return;
        }

        switch (collision.tag)
        {
            case "Enemy":
                Dead();
                break;
        }
        BulletFactory.Instance.Push(collision.gameObject);
    }

    private void Skill()
    {
        Time.timeScale = TIME_DEFAULT;
        if (Input.GetMouseButton(0))
        {
            if (skill > 0.0f)
            {
                Time.timeScale = TIME_SLOW;

                skill -= SKILL_DOWN;
                if (skill < 0.0f)
                {
                    skill = 0.0f;
                }
            }
        }
        else
        {
            skill += SKILL_UP;
            if (skill > SKILL_MAX)
            {
                skill = SKILL_MAX;
            }
        }
        GameManager.Instance.SetSkillGauge(skill, SKILL_MAX);
    }

    private void Move()
    {
#if UNITY_STANDALONE_WIN
        float pos = Input.mousePosition.x;
        pos -= Screen.width * 0.5f;
        pos = pos / Screen.width;
        float temp = pos * ANGLE * 0.05f;
#endif
#if UNITY_ANDROID
        float temp = Input.acceleration.x * ANGLE;
#endif
#if UNITY_EDITOR
        temp *= 20.0f;
#endif
        Camera.main.transform.RotateAround(Vector3.zero, Vector3.forward, temp);
        transform.RotateAround(Vector3.zero, Vector3.forward, temp);
    }

    private void Dead()
    {
        Time.timeScale = 0.0f;

        Destroy(gameObject);
        GameManager.Instance.Player = null;
    }
}
