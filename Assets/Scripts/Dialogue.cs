using UnityEngine;

[System.Serializable]
public class Dialogue
{
    public string characterName;
    public Sprite portrait; // The face of the character talking
    [TextArea(3, 10)]
    public string[] sentences; // The lines of text
}