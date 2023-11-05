using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModListButton : MonoBehaviour
{
    public string ModPath;

    public void OnClick()
    {
        TitleManager manager = GameObject.Find("TitleScreenManager").GetComponent<TitleManager>();

        manager.LoadSystem(ModPath);
    }
}
