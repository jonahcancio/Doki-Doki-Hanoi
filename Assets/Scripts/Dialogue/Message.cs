using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Message {
    public Sprite emotion;
    [TextArea (3, 10)]
    public string text;
}