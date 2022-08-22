using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public Text NameText;
    public Text DialogueText;
    public GameObject StartConversation;
    public GameObject NextConversation;
    private Queue<string> sentences;
    public GameObject CharacterImage;

    // Start is called before the first frame update
    void Start()
    {
        sentences = new Queue<string>();
        if (StartConversation != null)
        {
            StartConversation.GetComponent<DialogueTrigger>().TriggerDialogue();
        }
    }

    public void StartDialogue(Dialogue dialogue)
    {
        if(dialogue.NextConversation!=null)
        {
            NextConversation = dialogue.NextConversation;
        }
        else
        {
            NextConversation = null;
        }
        
        if(dialogue.CharacterImage!=null)
        {
            CharacterImage.GetComponent<Image>().sprite = dialogue.CharacterImage;
        }
        NameText.text = dialogue.name;
        DialogueText.fontSize = dialogue.fontSize;
        sentences.Clear();

        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }
        DisplayNextSentence();
    }
    
    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }
        string sentence = sentences.Dequeue();
        DialogueText.text = sentence;
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }

    IEnumerator TypeSentence(string sentence)
    {
        DialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            DialogueText.text += letter;
            yield return new WaitForSeconds(0.05f);
        }
        yield return null;
    }
    
    public void EndDialogue()
    {
        if (NextConversation != null)
        {
            NextConversation.GetComponent<DialogueTrigger>().TriggerDialogue();
        }
        else GameObject.Find("Canvas").SetActive(false);
    }
}
