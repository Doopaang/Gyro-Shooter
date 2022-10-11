using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class MainManager : MonoBehaviour
{
    public static MainManager Instance { get; private set; }

    [SerializeField]
    private GameObject menuButton;
    [SerializeField]
    private GameObject menu;
    [SerializeField]
    private GameObject leftArrow;
    [SerializeField]
    private GameObject rightArrow;

    [SerializeField]
    private GameObject enemy;
    [SerializeField]
    private GameObject nameObj;

    private const float FADE_SPEED = 1.0f;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        enemy.SetActive(false);
        nameObj.SetActive(false);
        StartCoroutine(Fade(menuButton));
        StartCoroutine(Fade(leftArrow));
        StartCoroutine(Fade(rightArrow));
    }

    public void EndName()
    {
        nameObj.SetActive(false);
    }

    public void StartName()
    {
        nameObj.GetComponent<Name>().Active(enemy.name);
    }

    public void Menu()
    {
        menu.SetActive(!menu.activeSelf);
    }

    public void GameStart()
    {
        Sender sender = new Sender(enemy.name);

        SceneManager.LoadScene("GameScene");
    }

    private IEnumerator Fade(GameObject obj)
    {
        float alpha = 0.0f;
        bool end = false;
        while (alpha < 0.9f)
        {
            Image image = obj.GetComponent<Image>();
            Color color = image.color;
            color.a = alpha;
            image.color = color;
            alpha = Mathf.Lerp(alpha, 1.0f, FADE_SPEED * Time.deltaTime);
            if (alpha >= 0.5f &&
                end == false)
            {
                end = true;
                enemy.GetComponent<EnemyList>().Active();
            }
            yield return null;
        }
    }
}
