using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeOutControls : MonoBehaviour
{
    float a = 1f;
    public Image[] buttons;
    public Text[] texts;
    
	
	
	// Update is called once per frame
	void Update ()
    {
        

        a -= Time.deltaTime * 0.2f;
        for(int i= 0; i < buttons.Length; i++)
        {
            Color c = buttons[i].color;
            c.a = a;
            buttons[i].color = c;
            Color c2 = texts[i].color;
            c2.a = a;
            texts[i].color = c2;
        }
        if(a <= 0)
        {
            FindObjectOfType<BlockSpawner>().isStarted = true;
        }
	}
}
