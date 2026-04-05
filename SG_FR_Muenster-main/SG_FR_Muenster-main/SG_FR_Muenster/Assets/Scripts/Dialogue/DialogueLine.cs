using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable] // zeigt es im Inspector an
public class DialogueLine
{
    [TextArea(3,5)]
    public string text;

    public Sprite image;
    public Sprite portrait;
}
