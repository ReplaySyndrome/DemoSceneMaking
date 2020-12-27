using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowAxisList : MonoBehaviour
{
    private Text showtext;


    private List<KeyValuePair<float, char>> inputAxisList;

    void Awake()
    {
        showtext = GetComponent<Text>();
    }
    void Start()
    {
        
    }
    // Update is called once per frame
    void Update()
    {
        inputAxisList = GameObject.FindGameObjectWithTag("Player").GetComponent<CommandSystem>().GetInputAxisList;
        string showstring = "";
        foreach (var i in inputAxisList)
        {
            showstring += i.Value;
        }

        showtext.text = showstring;
    }


}
