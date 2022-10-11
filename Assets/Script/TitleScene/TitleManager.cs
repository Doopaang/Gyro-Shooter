using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Rendering;

public class TitleManager : MonoBehaviour
{
    [SerializeField]
    private GameObject text;
    private const float BLINK = 0.5f;
    private const float BLINK_SHORT = 0.1f;

    private const float CHANGE = 0.7f;
    
    void Start()
    {
        Screen.SetResolution(Screen.width, Screen.width / 16 * 9, Screen.fullScreen);
        Invoke("Blink", BLINK);
    }
    
    void Update()
    {
        if(!SplashScreen.isFinished)
        {
            return;
        }

        if(Input.GetMouseButtonDown(0))
        {
            CancelInvoke("Blink");
            Invoke("BlinkShort", BLINK_SHORT);
            Invoke("ChangeScene", CHANGE);
        }
    }

    private void Blink()
    {
        text.SetActive(!text.activeSelf);
        Invoke("Blink", BLINK);
    }

    private void BlinkShort()
    {
        text.SetActive(!text.activeSelf);
        Invoke("BlinkShort", BLINK_SHORT);
    }

    private void ChangeScene()
    {
        SceneManager.LoadScene("MainScene");
    }
}
