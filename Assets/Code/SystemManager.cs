using NewHorizons.External.Configs;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using Newtonsoft.Json;
using TMPro;
using System;
using UnityEngine.UI;

public class SystemManager : Singleton<SystemManager>
{
    [HideInInspector]
    public string ModPath;
    [Tooltip("A default mod to load a system from, within your AppData/Roaming/OuterWildsModManager/OWML/Mods folder")]
    public string DefaultMod;
    public List<StarSystemConfig> Systems;
    public StarSystemConfig CurrentSystem;

    public Action LoadSystemInfo;

    [SerializeField] private GameObject systemButton;
    [SerializeField] private GameObject systemList;

    private void Start()
    {
        GameObject managerObject = GameObject.Find("TitleScreenManager");
        if (managerObject == null)
        {
            ModPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\OuterWildsModManager\\OWML\\Mods\\" + DefaultMod;
        }
        else
        {
            TitleManager titleManager = managerObject.GetComponent<TitleManager>();
            ModPath = titleManager.ModPath;
        }
        Systems = new List<StarSystemConfig>();

        LoadMod(ModPath);
        // Unload title scene now that we've gotten all the data from it that we need
        if (SceneManager.sceneCount > 1)
        {
            SceneManager.UnloadSceneAsync(0);
        }
    }

    private void Update()
    {

    }

    /// <summary>
    /// Loads all the systems in a mod
    /// </summary>
    /// <param name="path"></param>
    public void LoadMod(string path)
    {
        Debug.Log($"Loading mod {path}");
        if (Directory.Exists(path + "\\systems"))
        {
            foreach(string file in Directory.GetFiles(path + "\\systems"))
            {
                if (file.EndsWith(".json"))
                {
                    Debug.Log($"Opening file at {file}");
                    GameObject go = Instantiate(systemButton, systemList.transform);
                    string configText = File.ReadAllText(file);
                    StarSystemConfig config = JsonConvert.DeserializeObject<StarSystemConfig>(configText);
                    Systems.Add(config);
                    string systemName = file.Replace(Directory.GetParent(file).FullName + "\\", "");
                    systemName = systemName.Replace(".json", "");
                    go.transform.Find("SystemLabel/SystemName").GetComponent<TMP_Text>().text = systemName;
                    go.GetComponent<SystemButton>().Config = config;
                }
            }
        }
    }

}
