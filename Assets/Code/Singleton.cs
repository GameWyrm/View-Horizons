using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T _instance;
    private static bool isDestroyed;

    // Returns the instance
    public static T Instance
    {
        get
        {
            // If destroyed, warn & return null
            if (isDestroyed)
            {
                Debug.LogWarning("[Singleton] Instance of type " + typeof(T) + " was destroyed. Returning null.");
                return null;
            }

            if (_instance == null)
            {
                // Find instance if it exists
                _instance = (T)FindObjectOfType(typeof(T));

                if (_instance == null)
                {
                    // Create instance
                    GameObject newInstance = new GameObject();
                    newInstance.name = typeof(T).ToString() + " (Singleton)";
                    _instance = newInstance.AddComponent<T>();

                    // Make persistent
                    DontDestroyOnLoad(newInstance);
                }
            }

            return _instance;
        }
    }

    private void OnApplicationQuit()
    {
        isDestroyed = true;
    }

    private void OnDestroy()
    {
        isDestroyed = true;
    }
}
