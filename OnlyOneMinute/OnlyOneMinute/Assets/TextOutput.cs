using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextOutput : MonoBehaviour {

    public TextMeshProUGUI textBox;
    
    public void setText(string text) {
        textBox.text = text;
    }
    
    

}
