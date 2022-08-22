using UnityEngine;
using UnityEngine.UI;
[System.Serializable]
public class Dialogue
{
    public string name = "???";
    public int fontSize = 26;
    public Sprite CharacterImage;
    public GameObject BackgroundImage;
    public GameObject NextConversation;
    [TextArea(3, 10)]
    public string[] sentences;
}
