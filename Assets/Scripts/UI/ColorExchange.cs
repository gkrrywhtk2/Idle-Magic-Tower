using UnityEngine;
using UnityEngine.UI;

public class ColorExchange : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public Image image;
    public Color trueColor;
    public Color falseColor;

    public void SetColor(bool value)
    {
        image.color = value ? trueColor: falseColor;
    }
}
