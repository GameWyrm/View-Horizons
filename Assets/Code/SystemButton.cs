using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
using NewHorizons.External.Configs;
using System;

public class SystemButton : MonoBehaviour
{
    public StarSystemConfig Config;

    [SerializeField] private TMP_Text text;
    [SerializeField] private Image bg;
    [SerializeField] private Color selectedColor;
    [SerializeField] private Color unselectedColor;

    private SystemManager main;

    private void Start()
    {
        main = SystemManager.Instance;
    }

    public void OnClick()
    {
        bg.DOColor(selectedColor, 0.2f);
        text.DOColor(unselectedColor, 0.2f);
        main.CurrentSystem = Config;
        main.LoadSystemInfo.Invoke();
    }
}
