using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIMagicBar : MonoBehaviour
{
    public static UIMagicBar instance { get; private set; }

    public Image Magic;
    float originalSize;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        originalSize = Magic.rectTransform.rect.width;
    }
    public void SetValue(float value)
    {
        Magic.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, originalSize * value);
    }
}
