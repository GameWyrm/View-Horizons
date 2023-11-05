using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// A script that forces layout groups to always recalculate their bounds every frame that they're active
/// </summary>
public class AlwaysCalculateHeight : MonoBehaviour
{
    private RectTransform rt;

    private void Start()
    {
        rt = GetComponent<RectTransform>();
    }
    private void Update()
    {
        float height = 0;
        foreach (Transform child in transform)
        {
            height += child.GetComponent<RectTransform>().sizeDelta.y;
            height += 10; // padding
        }

        rt.sizeDelta = new Vector2(rt.sizeDelta.x, height);
    }
}
