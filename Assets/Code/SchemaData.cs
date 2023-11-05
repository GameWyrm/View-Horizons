using NewHorizons.External.Configs;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SchemaData : MonoBehaviour
{
    [TextArea(3,10)]
    public string tooltip;

    private TMP_Text tooltipObject;
    [SerializeField, Tooltip("The type of object that is being read from the config file. Objects need their own handling.")]
    private SchemaType type;
    [SerializeField, Tooltip("The field in the config you are editing")]
    private string fieldName;

    private SystemManager main;
    private bool myBool = false;
    private float myNumber = 0;
    private string myString = "";

    private void Awake()
    {
        tooltipObject = GameObject.FindGameObjectWithTag("Tooltip").GetComponent<TMP_Text>();
        main = SystemManager.Instance;
    }

    private void Start()
    {
        main.LoadSystemInfo += LoadValue;
    }

    public void OnHover()
    {
        tooltipObject.text = tooltip;
    }

    public void LoadValue()
    {
        if (type == SchemaType.boolean)
        {
            myBool = (bool)main.CurrentSystem.GetType().GetField(fieldName).GetValue(main.CurrentSystem);
            GetComponent<Toggle>().isOn = myBool;
        }
        else if (type == SchemaType.number)
        {
            myNumber = (float)main.CurrentSystem.GetType().GetField(fieldName).GetValue(main.CurrentSystem);
            GetComponentInChildren<TMP_InputField>().text = myNumber.ToString();
        }
        else if (type == SchemaType.text || type == SchemaType.fact)
        {
            myString = (string)main.CurrentSystem.GetType().GetField(fieldName).GetValue(main.CurrentSystem);
            GetComponentInChildren<TMP_InputField>().text = myString;
        }
    }

    public void ChangeValue()
    {
        if (type == SchemaType.boolean)
        {
            myBool = GetComponent<Toggle>().isOn;
            main.CurrentSystem.GetType().GetField(fieldName).SetValue(main.CurrentSystem, myBool);
        }
        else if (type == SchemaType.number)
        {
            if (float.TryParse(GetComponentInChildren<TMP_InputField>().text, out myNumber))
            {
                main.CurrentSystem.GetType().GetField(fieldName).SetValue(main.CurrentSystem, myNumber);
            }
            else Debug.LogError($"Incorrect data type given for {gameObject.name}");
        }
        else if (type == SchemaType.text || type == SchemaType.fact)
        {
            myString = GetComponentInChildren<TMP_InputField>().text;
            main.CurrentSystem.GetType().GetField(fieldName).SetValue(main.CurrentSystem, myString);
        }
    }

    private enum SchemaType
    {
        boolean,
        number,
        text,
        fact,
        obj
    }
}
