using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public Player Player;
    [HideInInspector]
    public Enemy Enemy;

    public GameObject pause;
    public RectTransform HPGauge;
    public Image skillGauge;
    public Transform meter;
    public GameObject meterPrefab;

    private float HP_GAUGE_MAX = 1074.0f;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        GameObject obj = Resources.Load("Enemy/" + Sender.Instance.EnemyName, typeof(GameObject)) as GameObject;
        GameObject now = Instantiate(obj);
        Enemy = now.GetComponent<Enemy>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            pause.SetActive(!pause.activeSelf);
        }
    }

    public void Restart()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void SetHPGauge(float HP, float MaxHP)
    {
        Vector2 vector = HPGauge.sizeDelta;
        vector.x = HP / MaxHP * HP_GAUGE_MAX;
        HPGauge.sizeDelta = vector;
    }

    public void SetSkillGauge(float skill, float SKILL_MAX)
    {
        skillGauge.fillAmount = skill / SKILL_MAX;
    }

    public void SetMeter(Vector3 position, float damage)
    {
        GameObject temp = Instantiate(meterPrefab, position, Camera.main.transform.rotation, meter);
        temp.GetComponent<Meter>().Init(damage);
    }
}
