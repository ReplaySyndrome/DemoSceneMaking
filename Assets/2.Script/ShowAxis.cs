using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowAxis : MonoBehaviour
{

    public char showaxis;
    private RawImage image;
    private Text text;

    void Awake()
    {
        image = GetComponent<RawImage>();
        text = GetComponentInChildren<Text>();
    }

    void Start()
    {
        text.text = char.ToString(showaxis);
        text.fontSize = 20;
        text.alignment = TextAnchor.MiddleCenter;
        image.color = new Color(0, 0, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        showaxis = char.ToLower(showaxis);
        if (Input.GetKey((KeyCode)showaxis))
        {
            image.color = Color.cyan;
            
        }
        else
        {
            image.color = new Color(0, 0, 0, 0.25f);
        }
    }
}
