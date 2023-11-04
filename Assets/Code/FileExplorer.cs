using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class FileExplorer
{
    public static List<NewHorizonsMod> GetMods()
    {
        List<NewHorizonsMod> modlist;
        string[] dirs = Directory.GetDirectories(@"%APPDATA%\OuterWildsModManager\OWML\Mods");
        modlist = new List<NewHorizonsMod>();

        foreach (string dir in dirs)
        {
            if (Directory.Exists(dir + @"\planets")) 
            {
                NewHorizonsMod mod = new NewHorizonsMod();
                mod.path = dir;
                mod.name = dir.Replace(Directory.GetParent(dir).FullName, "");
                string[] modSubstrings = mod.name.Split('.');
                mod.shortName = modSubstrings[modSubstrings.Length - 1];
                modlist.Add(mod);
                Debug.Log($"Found mod {mod.shortName} at {mod.path}");
            }
            else
            {
                Debug.Log($"Skipping mod {dir} as no planets folder was found");
            }
        }

        return modlist;
    }
}

public struct NewHorizonsMod
{
    public string shortName;
    public string name;
    public string path;

    public NewHorizonsMod(string name, string longName, string path)
    {
        this.shortName = name;
        this.name = longName;
        this.path = path;
    }
}
