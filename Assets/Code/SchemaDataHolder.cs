using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SchemaDataHolder : MonoBehaviour
{
    [SerializeField]
    private GameObject content;
    [SerializeField]
    private GameObject arrow;
    private bool expanded = false;
    private float height;
    private RectTransform rt;
    private CanvasGroup group;
    private SystemManager main;

    private void Start()
    {
        rt = GetComponent<RectTransform>();
        height = rt.sizeDelta.y;
        arrow.transform.localEulerAngles = new Vector3(0, 0, -90);
        rt.sizeDelta = new Vector2(rt.sizeDelta.x, 30);
        content.transform.localScale = new Vector3(1, 0, 1);
        content.SetActive(false);
        group = GetComponentInChildren<CanvasGroup>(true);
        main = SystemManager.Instance;
    }

    public void ToggleContent()
    {
        if (main.CurrentSystem == null)
        {
            Debug.LogWarning("No system selected, pick one before you can load system info");
            return;
        }
        if (expanded)
        {
            Collapse();
        }
        else
        {
            Expand();
        }
        expanded = !expanded;
    }

    public void Expand()
    {
        float width = rt.sizeDelta.x;
        content.SetActive(true);
        content.transform.DOScaleY(1, 0.2f);
        gameObject.GetComponent<RectTransform>().DOSizeDelta(new Vector2(width, height), 0.2f).OnComplete(() => group.interactable = true);
        arrow.transform.DORotate(new Vector3(0, 0, -180), 0.2f);
    }

    public void Collapse()
    {
        float width = rt.sizeDelta.x;
        group.interactable = false;
        gameObject.GetComponent<RectTransform>().DOSizeDelta(new Vector2(width, 30), 0.2f).OnComplete(() => content.SetActive(false));
        content.transform.DOScaleY(0, 0.2f);
        arrow.transform.DORotate(new Vector3(0, 0, -90), 0.2f);
    }
}
