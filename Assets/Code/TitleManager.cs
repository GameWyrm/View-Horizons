using NewHorizons.External.Configs;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;

public class TitleManager : MonoBehaviour
{
    public StarSystemConfig ActiveSystem;
    public List<PlanetConfig> Planets;
    public List<NewHorizonsMod> Mods;
    public GameObject MainPanel;
    public GameObject FilePanel;
    public GameObject ModButton;
    public GameObject ModBrowser;

    private void Awake()
    {
        DontDestroyOnLoad(this);
    }

    public void LoadNewSystem()
    {
        MainPanel.SetActive(false);
        FilePanel.SetActive(true);

        Mods = FileExplorer.GetMods();

        List<GameObject> children = new List<GameObject>();
        foreach (Transform child in ModBrowser.transform)
        {
            children.Add(child.gameObject);
        }
        children.ForEach(child => Destroy(child));

        foreach (NewHorizonsMod mod in Mods)
        {
            GameObject newButton = Instantiate(ModButton, ModBrowser.transform);
            newButton.name = mod.name + " Button";
            newButton.GetComponentsInChildren<TMP_Text>()[0].text = mod.shortName;
            newButton.GetComponentsInChildren<TMP_Text>()[1].text = mod.path;
        }
    }
}
