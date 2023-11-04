using NewHorizons.External.Configs;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Main : MonoBehaviour
{
    public StarSystemConfig ActiveSystem;
    public List<PlanetConfig> Planets;
    public List<NewHorizonsMod> Mods;

    private void Awake()
    {
        DontDestroyOnLoad(this);
    }

    public void LoadNewSystem()
    {
        Mods = FileExplorer.GetMods();
    }
}
