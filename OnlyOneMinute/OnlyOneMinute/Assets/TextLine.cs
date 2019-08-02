using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New TextLine", menuName = "Text Line", order = 0)]
public class TextLine : ScriptableObject {
    public bool readOnly;
    public string key;
    public TextLine[] children;
    public TextLine parent;
    public string preview;

}
