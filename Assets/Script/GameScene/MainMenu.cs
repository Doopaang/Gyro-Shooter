using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.IO;

public class MainMenu : MonoBehaviour
{
    public Slider bgmSlider;
    private float bgmDefault;

    public Slider sfxSlider;
    private float sfxDefault;

    public Slider colorSlider;
    private int colorDefault;

    [SerializeField]
    private List<Image> UIImage;

    void Start()
    {
        bgmDefault = bgmSlider.value;
        sfxDefault = sfxSlider.value;
        colorDefault = (int)colorSlider.value;
    }

    private Color SetColor(int index)
    {
        return Color.black;
    }

    public void BgmSlider()
    {

    }

    public void SfxSlider()
    {

    }

    public void ColorSlider()
    {
        foreach (Image obj in UIImage)
        {
            obj.color = SetColor((int)colorSlider.value);
        }
    }

    public void Clear()
    {
        bgmSlider.value = bgmDefault;
        sfxSlider.value = sfxDefault;
        colorSlider.value = colorDefault;
    }

    public void Close()
    {
        FileStream f = new FileStream(Application.dataPath + "Option.txt", FileMode.Append, FileAccess.Write);
        StreamWriter writer = new StreamWriter(f, System.Text.Encoding.Unicode);
        //writer.WriteLine(strData);
        writer.Close();

        gameObject.SetActive(false);
    }
}
