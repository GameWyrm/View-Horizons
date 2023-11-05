using NewHorizons.External.Configs;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleManager : MonoBehaviour
{
    public StarSystemConfig ActiveSystem;
    public List<PlanetConfig> Planets;
    public List<NewHorizonsMod> Mods;
    public GameObject MainPanel;
    public GameObject FilePanel;
    public GameObject ModButton;
    public GameObject ModBrowser;

    public string ModPath;

    private void Awake()
    {
        DontDestroyOnLoad(this);
    }

    public void LoadModList()
    {
        MainPanel.SetActive(false);
        FilePanel.SetActive(true);

        Mods = FileExplorer.GetMods();
        Mods = Mods.OrderBy(mod => mod.shortName).ToList();

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
            if (mod.path.Contains("AppData"))
            {
                newButton.GetComponentsInChildren<TMP_Text>()[1].text = mod.name;
            }
            else
            {
                newButton.GetComponentsInChildren<TMP_Text>()[1].text = mod.path;
            }
            newButton.GetComponent<ModListButton>().ModPath = mod.path;
        }
    }

    public void LoadSystem(string path)
    {
        Debug.Log($"Loading mod at {path}");
        ModPath = path;
        // Load editor scene
        SceneManager.LoadSceneAsync(1, LoadSceneMode.Additive);
    }
}
